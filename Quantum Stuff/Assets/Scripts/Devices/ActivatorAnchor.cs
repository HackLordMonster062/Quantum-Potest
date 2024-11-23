using UnityEngine;

public class ActivatorAnchor : Anchor {

	public override void Activate() {
		if (_particle != null && _particle.TryGetComponent(out Excitable excitable)) {
			excitable.Excite(1);
		}
	}
}
