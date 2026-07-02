using System;
using UnityEngine;

public class RailData : ActivatableData {
	public Vector3[] Path { get; private set; }
	public EntityId DeviceID { get; private set; }

	public RailData(string prefabId, Vector3 position, Vector3 rotation, EntityId id, EntityId triggerId, Vector3[] path, EntityId deviceId) : base(prefabId, position, rotation, id, triggerId) {
		Path = path;
		DeviceID = deviceId;
	}
}
