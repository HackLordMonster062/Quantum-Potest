using System.Collections.Generic;
using UnityEngine;

public class ActivatorParticleChild : MonoBehaviour {
	List<Activatable> _devices;

	int _currDevice = 0;

	void Awake() {
		_devices = new List<Activatable>();
	}

	private void OnTriggerEnter(Collider other) {
		if (other.TryGetComponent(out Activatable device)) {
			_devices.Add(device);
		}
	}

	private void OnTriggerExit(Collider other) {
		if (other.TryGetComponent(out Activatable device)) {
			_devices.Remove(device);
		}
	}

	public void Excite() {
		_currDevice = (_currDevice + 1) % _devices.Count;

		_devices[_currDevice].Activate();
	}
}
