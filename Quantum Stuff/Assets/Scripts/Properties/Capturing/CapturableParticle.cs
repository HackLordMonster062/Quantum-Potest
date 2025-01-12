using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class CapturableParticle : Capturable {
    Rigidbody _rb;

    Vector3 _target;
    float _force;

	void Awake() {
        _rb = GetComponent<Rigidbody>();
	}
	void FixedUpdate() {
        if (Capturer != null) {
			Vector3 damping = _force * dampingAmount * -_rb.velocity;

			_rb.AddForce((_target - transform.position) * _force + damping);
		}
	}
	public override Vector3 MoveTo(Vector3 target, float force) {
		_target = target;
		_force = force;

        return transform.position - target;
	}
}
