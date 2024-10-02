using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class ParticleBehavior : MonoBehaviour {
	[SerializeField] float mass;
	public float Mass { get { return mass; } }

	[Space]
	[SerializeField] float maxDistance;
	[SerializeField] float repulsionStrength;
	[SerializeField] LayerMask repulsionMask = ~0;

	Vector3[] directions;

	public Rigidbody Rb { get; private set; }
	Collider _collider;

	float _force;

    void Awake() {
        Rb = GetComponent<Rigidbody>();
        Rb.useGravity = false;

		_collider = GetComponent<Collider>();

		directions = GenerateSymmetricalRingDirections(3, 4);
    }

    void FixedUpdate() {
		Vector3 repulsionForce = Vector3.zero;

		foreach (Vector3 direction in directions) {
			if (Physics.Raycast(transform.position, direction, out RaycastHit hit, maxDistance, repulsionMask, QueryTriggerInteraction.Ignore)) {
				_force = 1;

				if (hit.collider.TryGetComponent<ParticleBehavior>(out var other)) _force = Mathf.Clamp(other.mass / mass, 0, 1);

				Vector3 dir = transform.position - hit.point;

				repulsionForce += dir.normalized * (repulsionStrength / hit.distance) * _force;
			}
		}

		Rb.AddForce(repulsionForce, ForceMode.Acceleration);

		Rb.AddForce(Vector3.down * PhysicsManager.instance.Gravity);
	}

	Vector3[] GenerateSymmetricalRingDirections(int ringCount, int pointsPerRing) {
		List<Vector3> directions = new List<Vector3>();

		for (int i = 0; i < ringCount; i++) {
			float theta = Mathf.PI * (i + 0.5f) / ringCount - Mathf.PI / 2;

			float y = Mathf.Sin(theta);
			float r = Mathf.Cos(theta);

			for (int j = 0; j < pointsPerRing; j++) {
				float phi = 2 * Mathf.PI * j / pointsPerRing;

				float x = r * Mathf.Cos(phi);
				float z = r * Mathf.Sin(phi);

				directions.Add(new Vector3(x, y, z));
			}
		}

		directions.Add(Vector3.down);

		return directions.ToArray();
	}

	public void PickUp() {
		Rb.isKinematic = true;
		_collider.enabled = false;
	}

	public void Drop() {
		Rb.isKinematic = false;
		_collider.enabled = true;
	}
}
