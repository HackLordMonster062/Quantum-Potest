using UnityEngine;

public class SpinDevice : Anchor {

	public override void Activate() {
		if (_isEnabled && _particle != null && _particle.TryGetComponent(out Rotateable particle)) {
			particle.FlipSpin();
		}
	}
}
