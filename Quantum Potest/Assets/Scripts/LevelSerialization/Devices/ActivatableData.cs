using System;
using UnityEngine;

[Serializable]
public class ActivatableData : DeviceData {
	public EntityId TriggerID { get; private set; }

	public ActivatableData(string prefabId, Vector3 position, Vector3 rotation, EntityId id, EntityId triggerId) : base(prefabId, position, rotation, id) {
		TriggerID = triggerId;
	}
}
