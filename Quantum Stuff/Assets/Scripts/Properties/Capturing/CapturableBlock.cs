using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class CapturableBlock : Capturable {
	[SerializeField] Vector3 movementAxis;

	Rigidbody _rb;

	Vector3 _target;
	float _force;

	Vector3 _appliedForce;
	Vector3 _velocity;

	void Awake() {
		_rb = GetComponent<Rigidbody>();
	}

	void FixedUpdate() {
		if (Capturer != null) {
			Vector3 damping = _force * dampingAmount * Time.fixedDeltaTime * -_velocity;

			Vector3 target = _target - transform.position;
			target = Vector3.Project(target, movementAxis);

			AddForce(_force * Time.fixedDeltaTime * target + damping);
		}

		_rb.MovePosition(_rb.position + _velocity * Time.fixedDeltaTime);
	}

	void AddForce(Vector3 force) {
		_appliedForce += force / _rb.mass;

		_velocity += _appliedForce;
	}

	public override Vector3 MoveTo(Vector3 target, float force) {
		_target = target;
		_force = force;

		return transform.position - target;
	}
}
