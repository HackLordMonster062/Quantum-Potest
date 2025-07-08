using UnityEngine;

[RequireComponent(typeof(Anchor))]
public class SpinDevice : Activatable {
	Anchor _anchor;

	private void Awake() {
		_anchor = GetComponent<Anchor>();
	}

	public override void Activate(int energy = 1) {
		if (_anchor.Particle != null && _anchor.Particle.TryGetComponent(out IRotateable particle)) {
			particle.FlipSpin();
		}
	}
}
