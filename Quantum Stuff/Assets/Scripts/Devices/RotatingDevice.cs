using UnityEngine;

public class RotatingDevice : MonoBehaviour {

	private void OnTriggerEnter(Collider other) {
		if (other.TryGetComponent(out Rotateable particle)) {
            particle.Rotate();
        }
	}
}
