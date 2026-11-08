using System;
using UnityEngine;
using UnityEngine.UIElements;

[Serializable]
public class EmitterData : ParticleData {
	public Spin Spin { get; private set; }

	public EmitterData(Vector3 position, Vector3 rotation, Vector3 scale, int energy, Spin spin) {
		PrefabID = "Emitter";

		Position = position;
		Rotation = rotation;
		Scale = scale;
		Energy = energy;
		Spin = spin;
	}
}
