using UnityEngine;

public class CapturableBlock : Capturable, ISerializableElement {
	[SerializeField] Transform lowerBounds;
	[SerializeField] Transform upperBounds;
	[SerializeField] bool orientOnTrack;

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

	public void Initialize(Transform lowerBounds, Transform upperBounds, bool orientOnTrack) {
		this.lowerBounds = lowerBounds;
		this.upperBounds = upperBounds;

		this.orientOnTrack = orientOnTrack;
	}

	public ElementData Serialize() {
		return new SlidingWallData("SlidingWall", transform.position, transform.eulerAngles, transform.localScale, lowerBounds.position, upperBounds.position, orientOnTrack);
	}

	public void Deserialize(ElementData data) {
		SlidingWallData casted = (SlidingWallData)data;

		transform.position = casted.Position;
		transform.eulerAngles = casted.Rotation;
		transform.localScale = casted.Scale;

		Transform point1 = new GameObject().transform;
		point1.position = casted.Point1;

		Transform point2 = new GameObject().transform;
		point2.position = casted.Point2;

		Initialize(point1, point2, orientOnTrack);
	}
}
