using System;
using UnityEngine;

[Serializable]
public class GravoData : ParticleData {
	public GravoData(Vector3 position, Vector3 rotation, Vector3 scale, int energy) {
		PrefabID = "Gravo";

		Position = position;
		Rotation = rotation;
		Scale = scale;
		Energy = energy;
	}
}
