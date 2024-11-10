using UnityEngine;

public class SpinDevice : Anchor {

	public override void Activate() {
		if (_isEnabled && _particle != null && _particle.TryGetComponent(out IRotateable particle)) {
			particle.FlipSpin();
		}
	}
}
