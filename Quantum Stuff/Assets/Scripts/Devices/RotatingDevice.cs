using UnityEngine;

[RequireComponent(typeof(Anchor))]
public class RotatingDevice : Activatable {
	Anchor _anchor;

	private void Awake() {
		_anchor = GetComponent<Anchor>();
	}

	public override void Activate(int energy = 1) {
		if (_anchor.Particle != null && _anchor.Particle.TryGetComponent(out IRotateable particle)) {
			for (int i = 0; i < energy; i++)
				particle.Rotate();
		}
	}
}
