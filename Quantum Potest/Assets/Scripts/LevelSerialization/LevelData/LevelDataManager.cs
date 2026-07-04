using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LevelDataManager : MonoBehaviour {
    [SerializeField] Material roomMaterial;
    [SerializeField] Vector2 doorSize;
    [SerializeField] Vector3 defaultRoomSize;
    [SerializeField] Vector3 defaultEntrancePoint;
    [SerializeField] Vector3 defaultExitPoint;

    [SerializeField] List<LevelData> levels;
	
	void Start() {
        ConstructLevelList(levels);
    }

    void Update() {
        
    }

    public void ConstructLevelList(List<LevelData> levelList) {
        (GameObject lastRoom, LevelData lastLevel) = (null, null);

        foreach (LevelData data in levelList) {
            GameObject room = ConstructLevel(data);

            if (lastRoom != null) {
                room.transform.position = lastRoom.transform.position + lastLevel.Exit - data.Entrance;
            }

            (lastRoom, lastLevel) = (room, data);
        }
    }

    public GameObject ConstructLevel(LevelData data) {
        GameObject room = CreateRoom(data);

        Dictionary<string, (DeviceData, GameObject)> deviceLookup = new();

        foreach (ElementData element in data.Elements) {
			GameObject physical = Instantiate(PrefabManager.instance.GetDevice(element.PrefabID), element.Position, Quaternion.Euler(element.Rotation), room.transform);

			switch (element) {
                case DeviceData device:
                    deviceLookup[device.ID] = (device, physical);
                    break;
                case ParticleData particle:
                    physical.GetComponent<Excitable>().Excite(particle.Energy, false);
                    break;
                case SurfaceData surface:
                    physical.transform.localScale = surface.Scale;

                    if (surface is SlidingWallData slidingWall) {
                        Transform point1 = Instantiate(PrefabManager.instance.Devices.RailPoint, slidingWall.Point1, Quaternion.identity).transform;
                        Transform point2 = Instantiate(PrefabManager.instance.Devices.RailPoint, slidingWall.Point2, Quaternion.identity).transform;

                        physical.GetComponent<CapturableBlock>().Initialize(point1, point2, slidingWall.Orient);
                    }

                    break;
                default:
                    break;
            }
        }

        foreach (var (id, (eData, obj)) in deviceLookup) {
			switch (eData) {
				case ActivatableData device:
                    if (device.TriggerID != "") {
                        obj.GetComponent<Activatable>().SetTrigger(deviceLookup[device.TriggerID].Item2.GetComponent<Trigger>());
                    }

                    if (device is RailData rail) {
                        Transform[] path = rail.Path.Select(point => Instantiate(PrefabManager.instance.Devices.RailPoint, point, Quaternion.identity).transform).ToArray();

                        obj.GetComponent<Rail>().Initialize(path, deviceLookup[rail.DeviceID].Item2.transform);
                    }

					break;
                case FrequencyDoorData door:
                    obj.GetComponent<FrequencyDoor>().SetFrequency(door.Frequency);
                    break;
				default:
					break;
			}
		}

        return room;
    }

    GameObject CreateRoom(LevelData data) { // TODO: Fix zero-area triangles
        GameObject room = new GameObject();
        MeshRenderer renderer = room.AddComponent<MeshRenderer>();
        renderer.material = roomMaterial;
        MeshFilter filter = room.AddComponent<MeshFilter>();

        Mesh mesh = new();

        Vector3[] vertices = new Vector3[] {
            new Vector3(0, 0, 0),//0
            new Vector3(data.Size.x, 0, 0),//1
            new Vector3(data.Size.x, data.Size.y, 0),//2
            new Vector3(0, data.Size.y, 0),//3
            new Vector3(0, 0, data.Size.z),//4
            new Vector3(data.Size.x, 0, data.Size.z),//5
            new Vector3(data.Size.x, data.Size.y, data.Size.z),//6
            new Vector3(0, data.Size.y, data.Size.z),//7

            new Vector3(data.Entrance.x - doorSize.x/2, data.Entrance.y, data.Entrance.z),//8
            new Vector3(data.Entrance.x + doorSize.x/2, data.Entrance.y, data.Entrance.z),//9
            new Vector3(data.Entrance.x - doorSize.x/2, data.Entrance.y + doorSize.y, data.Entrance.z),//10
            new Vector3(data.Entrance.x + doorSize.x/2, data.Entrance.y + doorSize.y, data.Entrance.z),//11

            new Vector3(data.Exit.x - doorSize.x/2, data.Exit.y, data.Exit.z),//12
            new Vector3(data.Exit.x + doorSize.x/2, data.Exit.y, data.Exit.z),//13
            new Vector3(data.Exit.x - doorSize.x/2, data.Exit.y + doorSize.y, data.Exit.z),//14
            new Vector3(data.Exit.x + doorSize.x/2, data.Exit.y + doorSize.y, data.Exit.z),//15
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
