using System;
using UnityEngine;

[Serializable]
public class SurfaceData : ElementData {
	public Vector3 Scale { get; private set; }

	public SurfaceData(string prefabId, Vector3 position, Vector3 rotation, Vector3 scale) {
		PrefabID = prefabId;
		Position = position;
		Rotation = rotation;
		Scale = scale;
	}
}
