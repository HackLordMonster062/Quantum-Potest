public class ActivatorParticleParent : Particle {
	ActivatorParticleChild _activatorScript;

	protected override void Awake() {
		base.Awake();

		_activatorScript = GetComponentInChildren<ActivatorParticleChild>();
	}

	public override void Excite(int energy, bool invoke = true) {
		base.Excite(0, invoke);

		_activatorScript.Excite();
	}
}
