using UnityEngine;

[RequireComponent(typeof(CapturableParticle))]
public class ActivatorParticle : Particle {
    [SerializeField] float activationRadius;
    [SerializeField] LayerMask devicesMask;

	CapturableParticle _capturable;

	public override void Excite(int energy, bool invoke = true) {
		base.Excite(0, invoke);

		if (_capturable.Capturer != null && _capturable.Capturer.TryGetComponent(out Activatable activatable)) {
			activatable.Activate(energy);
		}
	}
}
