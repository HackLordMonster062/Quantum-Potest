using System;
using UnityEngine;

[Serializable]
public class CatalystData : ParticleData {
	public CatalystData(Vector3 position, Vector3 rotation, int energy) {
		Position = position;
		Rotation = rotation;
		Energy = energy;
	}
}
