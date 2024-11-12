using UnityEngine;

public class PressurePlate : Trigger {
	int inside = 0;

	private void OnTriggerEnter(Collider other) {
		inside++;
		if (inside == 1)
			Activate();
	}

	private void OnTriggerExit(Collider other) {
		inside--;
		if (inside == 0) 
			Deactivate();
	}
}
