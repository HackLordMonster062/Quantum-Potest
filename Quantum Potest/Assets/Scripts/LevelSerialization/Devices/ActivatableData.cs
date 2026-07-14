using System;
using UnityEngine;

[Serializable]
public class ActivatableData : DeviceData {
	public string TriggerID { get; private set; }

	public ActivatableData(string prefabId, Vector3 position, Vector3 rotation, Vector3 scale, string id, string triggerId) : base(prefabId, position, rotation, scale, id) {
		TriggerID = triggerId;
	}
}
