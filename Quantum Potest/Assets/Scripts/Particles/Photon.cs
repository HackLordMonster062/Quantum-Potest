using UnityEngine;

[RequireComponent (typeof(Rigidbody))]
public class Photon : MonoBehaviour {
	[SerializeField] int energy;

	Rigidbody _rb;

	private void OnEnable() {
		_rb = GetComponent<Rigidbody>();
	}

	private void Update() {
		_rb.velocity = GameManager.instance.SpeedOfLight * transform.forward;
	}

	private void OnCollisionEnter(Collision collision) {
		if (collision.collider.TryGetComponent(out Excitable other)) {
			other.Excite(energy);
		}

		if (collision.collider.TryGetComponent(out Reflective wall)) {
			Reflect(collision.contacts[0]);

			return;
		}

		Destroy(gameObject);
	}

	void Reflect(ContactPoint point) {
		Physics.Raycast(transform.position, transform.forward, out RaycastHit hit, 1);

		Vector3 normal = point.normal;

		transform.forward -= 2 * Vector3.Dot(normal, transform.forward) * normal;

		if (hit.collider != null) {
			transform.position = hit.point + transform.forward * transform.localScale.magnitude / Mathf.Sqrt(3);
		}
	}
}
