using System;
using UnityEngine;

[Serializable]
public class GravoData : ParticleData {
	public GravoData(Vector3 position, Vector3 rotation, int energy) {
		PrefabID = "Gravo";

		Position = position;
		Rotation = rotation;
		Energy = energy;
	}
}
