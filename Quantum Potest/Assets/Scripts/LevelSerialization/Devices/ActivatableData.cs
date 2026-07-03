using System;
using UnityEngine;

[Serializable]
public class ActivatableData : DeviceData {
	public string TriggerID { get; private set; }

	public ActivatableData(string prefabId, Vector3 position, Vector3 rotation, string id, string triggerId) : base(prefabId, position, rotation, id) {
		TriggerID = triggerId;
	}
}
