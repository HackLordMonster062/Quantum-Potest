using UnityEngine;

[RequireComponent (typeof(Rigidbody))]
public class Photon : MonoBehaviour {
	[SerializeField] float energy;

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
			Vector3 normal = collision.contacts[0].normal;

			transform.forward -= 2 * Vector3.Dot(normal, transform.forward) * normal;

			return;
		}

		Destroy(gameObject);
	}
}
