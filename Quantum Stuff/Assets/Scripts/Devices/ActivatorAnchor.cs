using UnityEngine;

[RequireComponent(typeof(Anchor))]
[RequireComponent(typeof(Trigger))]
public class ActivatorAnchor : Activatable {
	Trigger _trigger;

	private void Awake() {
		_trigger = GetComponent<Trigger>();
	}

	public override void Activate() {
		_trigger.Activate(0);
	}

	public override void Activate(int energy) {
		_trigger.Activate(energy);
	}

	protected override void Deactivate() {
		_trigger.Deactivate();
	}
}
