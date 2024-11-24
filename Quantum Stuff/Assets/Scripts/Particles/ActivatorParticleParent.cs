public class ActivatorParticleParent : Particle {
	ActivatorParticleChild _activatorScript;

	void Start() {
		_activatorScript = GetComponentInChildren<ActivatorParticleChild>();
	}

	public override bool TryCapture(Anchor capturer) {
		if (base.TryCapture(capturer)) {
			_activatorScript.SetAnchor(capturer);
			return true;
		}

		return false;
	}

	public override void Excite(int energy, bool invoke = true) {
		base.Excite(0, invoke);

		_activatorScript.Excite();
	}
}
