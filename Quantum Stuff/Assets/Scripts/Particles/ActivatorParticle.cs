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

			if (!depleted) {
				device.AddActivator(this);
			}
		}
	}

	private void OnTriggerExit(Collider other) {
		if (other.TryGetComponent(out Activatable device)) {
			print("Removed: " + other);
			devices.Remove(device);

			device.RemoveActivator(this);
		}
	}

	public override void Excite(int energy) {
		base.Excite(energy);

		foreach (Activatable device in devices) {
			print("Activated");
			device.AddActivator(this);
		}
	}

	protected override void Deplete() {
		base.Deplete();

		foreach (Activatable device in devices) {
			device.RemoveActivator(this);
		}
	}
}
