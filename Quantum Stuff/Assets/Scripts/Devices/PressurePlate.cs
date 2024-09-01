using UnityEngine;

public class PressurePlate : Trigger {
	private void OnTriggerEnter(Collider other) {
		Activate();
	}

	private void OnTriggerExit(Collider other) {
		Deactivate();
	}
}
