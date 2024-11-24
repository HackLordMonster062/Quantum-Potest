using UnityEngine;

public class ActivatorParticle : Particle {
    [SerializeField] float activationRadius;
    [SerializeField] LayerMask devicesMask;

	int _currDevice = 0;

	public override void Excite(int energy, bool invoke = true) {
		base.Excite(0, invoke);

		Collider[] colliders = Physics.OverlapSphere(transform.position, activationRadius, devicesMask, QueryTriggerInteraction.Ignore);

		int index = 0;
		_currDevice %= colliders.Length;

		foreach (Collider collider in colliders) {
			if (index != _currDevice) {
				index++;
			} else if (collider.TryGetComponent(out Activatable device) && !(device is Anchor && (device as Anchor) == _anchor)) {
				device.Activate();
				_currDevice++;
			}
		}
	}
}
