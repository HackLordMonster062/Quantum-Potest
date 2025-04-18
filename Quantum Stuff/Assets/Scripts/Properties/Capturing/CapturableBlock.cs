using UnityEngine;

public class CapturableBlock : Capturable {
	[SerializeField] Transform lowerBounds;
	[SerializeField] Transform upperBounds;
	[SerializeField] bool orientOnTrack;

	Vector3 _target;
	float _force;

	void FixedUpdate() {
		Vector3 axis = upperBounds.position - lowerBounds.position;
		float distance = axis.sqrMagnitude;

		if (orientOnTrack)
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
