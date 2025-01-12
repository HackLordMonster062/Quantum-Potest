using UnityEngine;

[RequireComponent(typeof(Anchor))]
public class ActivatorAnchor : Activatable {
	Anchor _anchor;

	private void Awake() {
		_anchor = GetComponent<Anchor>();
	}

	public override void Activate() {
		if (_anchor.Particle != null && _anchor.Particle.TryGetComponent(out Excitable excitable)) {
			excitable.Excite(1);
		}
	}
}
