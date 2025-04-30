using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class CapturableParticle : Capturable {
    Rigidbody _rb;

	void Awake() {
        _rb = GetComponent<Rigidbody>();
	}

	void FixedUpdate() {
        if (Capturer != null) {
			Vector3 damping = _force * dampingAmount * -_rb.velocity;

			_rb.AddForce((_target - transform.position) * _force + damping);
		}
	}
}
