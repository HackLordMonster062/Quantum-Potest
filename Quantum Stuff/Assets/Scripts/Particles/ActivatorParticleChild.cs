using System.Collections.Generic;
using UnityEngine;

public class ActivatorParticleChild : MonoBehaviour {
	List<Activatable> _devices;

	int _currDevice = 0;
	Capturer _capturer;

	void Awake() {
		_devices = new List<Activatable>();
	}

	public void SetCapturer(Capturer capturer) {
		_capturer = capturer;

		_devices.RemoveAll(device => device.TryGetComponent(out Capturer exCapturer) && exCapturer == capturer);
	}

	private void OnTriggerEnter(Collider other) {
		if (other.TryGetComponent(out Activatable device) && 
			!(other.TryGetComponent(out Capturer capturer) && capturer == _capturer)) {
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
