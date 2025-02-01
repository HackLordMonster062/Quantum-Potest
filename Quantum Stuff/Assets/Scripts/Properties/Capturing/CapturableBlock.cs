using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class CapturableBlock : Capturable {
	[SerializeField] Transform lowerBounds;
	[SerializeField] Transform upperBounds;

	Rigidbody _rb;

	Vector3 _target;
	float _force;

	void Awake() {
		_rb = GetComponent<Rigidbody>();
	}

	void FixedUpdate() {
		Vector3 axis = upperBounds.position - lowerBounds.position;
		float distance = axis.sqrMagnitude;
		transform.right = axis;

		if (Capturer != null) {
			Vector3 target = _target - transform.position;

			transform.position = Vector3.Lerp(transform.position, transform.position + target, Time.fixedDeltaTime * _force / Mass / Mass);
		}

		float projection = Vector3.Dot(transform.position - lowerBounds.position, axis);
		projection = Mathf.Clamp(projection, 0, distance);

		Vector3 projectedMovement = projection / distance * axis;

		transform.position = projectedMovement + lowerBounds.position;
	}

	public override Vector3 MoveTo(Vector3 target, float force) {
		_target = target;
		_force = force;

		return transform.position - target;
	}
}
