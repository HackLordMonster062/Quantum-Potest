using UnityEngine;

public class SpinDevice : MonoBehaviour {

	private void OnTriggerEnter(Collider other) {
		if (other.TryGetComponent(out Rotateable particle)) {
			particle.FlipSpin();
		}
	}
}
