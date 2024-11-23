public class ActivatorParticleParent : Particle {
	ActivatorParticle _activatorScript;

	protected override void Awake() {
		base.Awake();

		_activatorScript = GetComponentInChildren<ActivatorParticle>();
	}

	public override void Excite(int energy, bool invoke = true) {
		base.Excite(0, invoke);

		_activatorScript.Excite();
	}
}
