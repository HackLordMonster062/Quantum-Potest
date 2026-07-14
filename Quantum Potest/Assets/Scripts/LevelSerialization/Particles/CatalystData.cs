using System;
using UnityEngine;

[Serializable]
public class CatalystData : ParticleData {
	public CatalystData(Vector3 position, Vector3 rotation, Vector3 scale, int energy) {
		PrefabID = "Catalyst";

		Position = position;
		Rotation = rotation;
		Scale = scale;
		Energy = energy;
	}
}
