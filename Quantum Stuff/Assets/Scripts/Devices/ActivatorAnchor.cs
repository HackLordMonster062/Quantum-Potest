using UnityEngine;

[RequireComponent(typeof(Anchor))]
[RequireComponent(typeof(Trigger))]
public class ActivatorAnchor : Activatable {
	Trigger _trigger;

	private void Awake() {
		_trigger = GetComponent<Trigger>();
	}

	public override void Activate(int energy) {
		base.Activate(energy);
		_trigger.Activate(energy);
	}

	public override void Deactivate() {
		base.Deactivate();
		_trigger.Deactivate();
	}
}
