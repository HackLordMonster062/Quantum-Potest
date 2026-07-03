using System;
using UnityEngine;

public class RailData : ActivatableData {
	public Vector3[] Path { get; private set; }
	public string DeviceID { get; private set; }

	public RailData(string prefabId, Vector3 position, Vector3 rotation, string id, string triggerId, Vector3[] path, string deviceId) : base(prefabId, position, rotation, id, triggerId) {
		Path = path;
		DeviceID = deviceId;
	}
}
