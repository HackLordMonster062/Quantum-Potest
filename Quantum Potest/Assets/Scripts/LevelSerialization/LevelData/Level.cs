using UnityEngine;
using Newtonsoft.Json;
using System.Collections.Generic;
using System;
using Unity.VisualScripting;

[RequireComponent(typeof(BoxCollider))]
public class Level : MonoBehaviour {
    [SerializeField] Transform entranceHandle;
    [SerializeField] Transform exitHandle;

	public event Action<Level> OnPlayerEnter;
	public event Action<Level, Vector3, bool> OnHandleMoved; // TODO: Call

	Vector3 _size;

    BoxCollider _trigger;

    void Awake() {
        _trigger = GetComponent<BoxCollider>();
        _trigger.isTrigger = true;
    }

	private void OnTriggerEnter(Collider other) {
		if (other.CompareTag("Player")) {
			OnPlayerEnter?.Invoke(this);
		}
	}

	public LevelData GetLevelData() {
		return new LevelData(
			gameObject.name,
			_size,
			entranceHandle.localPosition,
			exitHandle.localPosition,
			GetAllData()
		);
	}

    List<ElementData> GetAllData() {
        List<ElementData> data = new();

		foreach (ISerializableElement element in transform.GetComponentsInChildren<ISerializableElement>()) {
			data.Add(element.Serialize());
		}

        return data;
    }

    public void DataUpdate(Vector3 entrance, Vector3 exit, Vector3 size) {
		entranceHandle.transform.position = entrance;
		exitHandle.transform.position = exit;

		_size = size;

		_trigger.size = size;
		_trigger.center = size / 2;

		GameObject room = CreateRoom(entrance, exit, size);
		room.transform.parent = transform;
    }

	GameObject CreateRoom(Vector3 entrance, Vector3 exit, Vector3 size) { // TODO: Fix zero-area triangles
		GameObject room = new GameObject();

		MeshFilter filter = room.AddComponent<MeshFilter>();
		MeshRenderer renderer = room.AddComponent<MeshRenderer>();
		renderer.material = LevelDataManager.instance.roomMaterial;

		Mesh mesh = new();

		Vector2 doorSize = LevelDataManager.instance.doorSize;

		Vector3[] vertices = new Vector3[] {
			new(0, 0, 0),//0
            new(size.x, 0, 0),//1
            new(size.x, size.y, 0),//2
            new(0, size.y, 0),//3
            new(0, 0, size.z),//4
            new(size.x, 0, size.z),//5
            new(size.x, size.y, size.z),//6
            new(0, size.y, size.z),//7

            new(entrance.x - doorSize.x/2, entrance.y, entrance.z),//8
            new(entrance.x + doorSize.x/2, entrance.y, entrance.z),//9
            new(entrance.x - doorSize.x/2, entrance.y + doorSize.y, entrance.z),//10
            new(entrance.x + doorSize.x/2, entrance.y + doorSize.y, entrance.z),//11

            new(exit.x - doorSize.x/2, exit.y, exit.z),//12
            new(exit.x + doorSize.x/2, exit.y, exit.z),//13
            new(exit.x - doorSize.x/2, exit.y + doorSize.y, exit.z),//14
            new(exit.x + doorSize.x/2, exit.y + doorSize.y, exit.z),//15
        };

		int[] triangles = new int[] {
			0, 3, 4,
			4, 3, 7, // Left wall
            1, 5, 6,
			6, 2, 1, // Right wall
            3, 2, 7,
			7, 2, 6, // Ceiling
            0, 4, 1,
			1, 4, 5, // Floor
            0, 8, 3,
			3, 8, 10,
			10, 11, 3,
			3, 11, 2,
			2, 11, 1,
			1, 11, 9,
			9, 8, 1,
			1, 8, 0, // Front wall
            4, 12, 5,
			5, 12, 13,
			13, 15, 5,
			5, 15, 6,
			6, 15, 7,
			7, 15, 14,
			14, 12, 7,
			7, 12, 4
		};

		mesh.vertices = vertices;
		mesh.triangles = triangles;

		mesh.RecalculateNormals();
		mesh.RecalculateBounds();

		filter.mesh = MakeFlatShaded(mesh);

		room.AddComponent<MeshCollider>().sharedMesh = mesh;

		return room;
	}

	public static Mesh MakeFlatShaded(Mesh sourceMesh) {
		Vector3[] oldVertices = sourceMesh.vertices;
		int[] oldTriangles = sourceMesh.triangles;

		Vector3[] newVertices = new Vector3[oldTriangles.Length];
		int[] newTriangles = new int[oldTriangles.Length];

		for (int i = 0; i < oldTriangles.Length; i++) {
			int originalIndex = oldTriangles[i];

			newVertices[i] = oldVertices[originalIndex];
			newTriangles[i] = i;
		}

		Mesh flatMesh = new() {
			name = sourceMesh.name + "_Flat",
			vertices = newVertices,
			triangles = newTriangles,
		};

		flatMesh.RecalculateNormals();
		flatMesh.RecalculateBounds();

		return flatMesh;
	}
}
