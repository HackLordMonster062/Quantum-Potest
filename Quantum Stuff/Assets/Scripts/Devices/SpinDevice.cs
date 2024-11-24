using UnityEngine;

public class SpinDevice : Anchor {

	public override void Activate() {
		if (_particle != null && _particle.TryGetComponent(out IRotateable particle)) {
			particle.FlipSpin();
		}
	}
}
