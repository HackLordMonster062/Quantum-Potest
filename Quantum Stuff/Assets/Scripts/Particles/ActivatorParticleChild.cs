using System.Collections.Generic;
using UnityEngine;

public class ActivatorParticleChild : MonoBehaviour {
	List<Activatable> _devices;

	int _currDevice = 0;
	Anchor _anchor;

	void Awake() {
		_devices = new List<Activatable>();
	}

	public void SetAnchor(Anchor anchor) {
		_anchor = anchor;

		_devices.Remove(anchor);
	}

	private void OnTriggerEnter(Collider other) {
		if (other.TryGetComponent(out Activatable device) && !(device is Anchor && (device as Anchor) == _anchor)) {
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
