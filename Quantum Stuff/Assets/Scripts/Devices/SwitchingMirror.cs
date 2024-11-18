using UnityEngine;

public class SwitchingMirror : MonoBehaviour {
    [SerializeField] Collider surface;

	void OnTriggerEnter(Collider other) {
		if (other.CompareTag("Photon")) {
			surface.enabled = !surface.enabled;
		}
	}
}
