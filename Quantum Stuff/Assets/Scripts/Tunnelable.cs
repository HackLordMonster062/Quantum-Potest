using UnityEngine;

public class Tunnelable : MonoBehaviour {


	private void OnCollisionEnter(Collision collision) {
		Excitable other = collision.collider.GetComponent<Excitable>();

		if (other != null) {
			print(other.gameObject);
		}
	}
}
