using UnityEngine;

public class EmitterData : ParticleData {
	public Spin Spin { get; private set; }

	public EmitterData(Vector3 position, Vector3 rotation, int energy, Spin spin) {
		Position = position;
		Rotation = rotation;
		Energy = energy;
		Spin = spin;
	}
}
