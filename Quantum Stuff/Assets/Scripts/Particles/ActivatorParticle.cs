using UnityEngine;

[RequireComponent(typeof(CapturableParticle))]
public class ActivatorParticle : Particle {
	CapturableParticle _capturable;

	protected override void Awake() {
		base.Awake();
		_capturable = GetComponent<CapturableParticle>();
	}

	public override void Excite(int energy, bool invoke = true) {
		base.Excite(energy, invoke);

		if (_capturable.Capturer != null && _capturable.Capturer.TryGetComponent(out Activatable activatable)) {
			activatable.Activate(Energy);
		}
	}

	protected override void Deplete() {
		base.Deplete();

		if (_capturable.Capturer != null && _capturable.Capturer.TryGetComponent(out Activatable activatable)) {
			activatable.Deactivate();
		}
	}
}
