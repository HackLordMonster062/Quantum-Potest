using UnityEngine;

public class Tunnelable : MonoBehaviour {
	[SerializeField] float minimumEnergy;

	private void OnCollisionEnter(Collision collision) {
		Excitable other = collision.collider.GetComponent<Excitable>();

		if (other != null && other.Energy >= minimumEnergy) {
			Vector3 relativePosition = other.transform.position - transform.position;

			Vector3 newPosition = Vector3.Reflect(relativePosition, transform.right) + transform.position;

			other.transform.position = newPosition;
		}
	}
}
