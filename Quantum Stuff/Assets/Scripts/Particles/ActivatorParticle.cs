using System.Collections.Generic;
using UnityEngine;

public class ActivatorParticle : Excitable {
	[SerializeField] float activationRange;

	List<Activatable> devices;

	protected override void Awake() {
		base.Awake();

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

	public override void Excite(int energy, bool invoke = true) {
		base.Excite(0, invoke);

		foreach (Activatable device in devices) {
			device.Activate();
			print("Activated");
		}
	}
}
