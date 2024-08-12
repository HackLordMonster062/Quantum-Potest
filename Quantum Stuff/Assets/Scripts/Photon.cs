using UnityEngine;

[RequireComponent (typeof(Rigidbody))]
public class Photon : MonoBehaviour {
	[SerializeField] float energy;

	private void OnCollisionEnter(Collision collision) {
		Excitable other = collision.collider.GetComponent<Excitable>();

		if (other != null) {
			other.Excite(energy);
		}

		Destroy(gameObject);
	}
}
