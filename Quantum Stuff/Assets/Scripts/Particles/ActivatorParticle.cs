using UnityEngine;

[RequireComponent(typeof(CapturableParticle))]
public class ActivatorParticle : Particle {
	CapturableParticle _capturable;

	ActivatorAnchor _anchor;

	protected override void Awake() {
		base.Awake();
		_capturable = GetComponent<CapturableParticle>();
		_capturable.OnCapture += OnCapture;
		_capturable.OnRelease += OnRelease;
	}

	public override void Excite(int energy, bool invoke = true) {
		_renderer.material.SetFloat("_EnergyChangeTime", Time.time);
		base.Excite(energy, invoke);

		if (_anchor != null) {
			_anchor.Activate(Energy);
		}
	}

	protected override void Decay() {
		_renderer.material.SetFloat("_EnergyChangeTime", Time.time);
		base.Decay();
	}

	protected override void Deplete() {
		_renderer.material.SetFloat("_EnergyChangeTime", Time.time);
		base.Deplete();

		if (_anchor != null) {
			_anchor.Deactivate();
		}
	}

	void OnCapture(Capturable _, CapturerStrengh __) {
		if (_capturable.Capturer != null && _capturable.Capturer.TryGetComponent(out ActivatorAnchor activator)) {
			_anchor = activator;

			_renderer.material.SetFloat("_CapturedChangeTime", Time.time);
			_renderer.material.SetInt("_Is_Captured", 1);
		}
	}

	void OnRelease(Capturable _) {
		if (_anchor != null) {
			if (Energy > 0)
				_anchor.Deactivate();
			_anchor = null;

			_renderer.material.SetFloat("_CapturedChangeTime", Time.time);
			_renderer.material.SetInt("_Is_Captured", 0);
		}
	}
}
