using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class ParticleMovement : MonoBehaviour, ILiftable {
	[SerializeField] float maxDistance;
	[SerializeField] float repulsionStrength;

	Vector3[] directions;

	Rigidbody _rb;
	Collider _collider;

    void Awake() {
        _rb = GetComponent<Rigidbody>();
        _rb.useGravity = false;

		_collider = GetComponent<Collider>();

		directions = GenerateSymmetricalRingDirections(3, 4);
    }

    void FixedUpdate() {
		Vector3 repulsionForce = Vector3.zero;

		foreach (Vector3 direction in directions) {
			if (Physics.Raycast(transform.position, direction, out RaycastHit hit, maxDistance)) {
				Vector3 dir = transform.position - hit.point;

				repulsionForce += dir.normalized * (repulsionStrength / hit.distance);
			}
		}

		_rb.AddForce(repulsionForce, ForceMode.Acceleration);

		_rb.AddForce(Vector3.down * PhysicsManager.instance.Gravity);
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
		_rb.isKinematic = true;
		_collider.enabled = false;
	}

	public void Drop() {
		_rb.isKinematic = false;
		_collider.enabled = true;
	}
}
