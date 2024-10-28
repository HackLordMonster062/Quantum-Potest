using UnityEngine;

public class ActivatorAnchor : Anchor {
	protected override void Pickup() {
		base.Pickup();
		_particle.OnPickedUp -= Pickup;
		_particle = null;
		_isEnabled = true;
	}

	public override void Activate() {
		if (_isEnabled && _particle != null && _particle.TryGetComponent(out Excitable excitable)) {
			excitable.Excite(1);
		}
	}
}
