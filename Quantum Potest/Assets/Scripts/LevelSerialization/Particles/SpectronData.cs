using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class SpectronData : ParticleData {
	public List<int> Frequencies { get; private set; }

	public SpectronData(Vector3 position, Vector3 rotation, Vector3 scale, int energy, List<int> frequencies) {
		PrefabID = "Spectron";

		Position = position;
		Rotation = rotation;
		Scale = scale;
		Energy = energy;
		Frequencies = frequencies;
	}
}
