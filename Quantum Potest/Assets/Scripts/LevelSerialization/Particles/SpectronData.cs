using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class SpectronData : ParticleData {
	public List<int> Frequencies { get; private set; }

	public SpectronData(Vector3 position, Vector3 rotation, int energy, List<int> frequencies) {
		Frequencies = frequencies;
	}
}
