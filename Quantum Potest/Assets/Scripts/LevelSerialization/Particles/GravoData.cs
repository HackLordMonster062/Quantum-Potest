using UnityEngine;

public class GravoData : ParticleData {
	public GravoData(Vector3 position, Vector3 rotation, int energy) {
		Position = position;
		Rotation = rotation;
		Energy = energy;
	}
}
