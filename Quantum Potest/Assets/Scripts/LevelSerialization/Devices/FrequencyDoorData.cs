using System;
using UnityEngine;

[Serializable]
public class FrequencyDoorData : DeviceData {
	public int Frequency { get; private set; }

	public FrequencyDoorData(string prefabId, Vector3 position, Vector3 rotation, Vector3 scale, string id, int frequency) : base(prefabId, position, rotation, scale, id) {
		Frequency = frequency;
	}
}