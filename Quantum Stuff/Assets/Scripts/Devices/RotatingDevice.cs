using UnityEngine;
using static UnityEngine.ParticleSystem;

public class RotatingDevice : Anchor {

	public override void Activate() {
		if (_isEnabled && _particle != null && _particle.TryGetComponent(out IRotateable particle)) {
			particle.Rotate();
		}
	}
}
