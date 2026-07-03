using System;
using UnityEngine;

[Serializable]
public class DeviceData : ElementData {
	public string ID { get; private set; }

	public DeviceData(string prefabId, Vector3 position, Vector3 rotation, string id) {
		PrefabID = prefabId;
		Position = position;
		Rotation = rotation;
		ID = id;
	}
}
