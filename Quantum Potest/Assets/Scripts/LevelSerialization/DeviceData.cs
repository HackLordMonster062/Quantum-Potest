using System;
using UnityEngine;

[Serializable]
public class DeviceData : ElementData {
	public EntityId ID { get; private set; }

	public DeviceData(string prefabId, Vector3 position, Vector3 rotation, EntityId id) {
		PrefabID = prefabId;
		Position = position;
		Rotation = rotation;
		ID = id;
	}
}
