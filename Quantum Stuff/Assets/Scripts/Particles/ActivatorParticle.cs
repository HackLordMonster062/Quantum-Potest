using System.Collections.Generic;
using UnityEngine;

public class ActivatorParticle : MonoBehaviour {
	List<Activatable> devices;

	void Awake() {
		devices = new List<Activatable>();
	}

	private void OnTriggerEnter(Collider other) {
		if (other.TryGetComponent(out Activatable device)) {
			devices.Add(device);
		}
	}

	private void OnTriggerExit(Collider other) {
		if (other.TryGetComponent(out Activatable device)) {
			devices.Remove(device);
		}
	}

	public void Excite() {
		foreach (Activatable device in devices) {
			device.Activate();
		}
	}
}
