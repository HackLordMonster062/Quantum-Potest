using UnityEngine;

[RequireComponent(typeof(Anchor))]
[RequireComponent(typeof(Trigger))]
public class ActivatorAnchor : Activatable, ISerializableElement<ActivatableData> {
	protected Trigger _ownTrigger;

	protected virtual void Awake() {
		_ownTrigger = GetComponent<Trigger>();
	}

	public override void Activate(int energy) {
		base.Activate(energy);
		_ownTrigger.Activate(energy);
	}

	public override void Deactivate() {
		base.Deactivate();
		_ownTrigger.Deactivate();
	}

	public ActivatableData Serialize() {
		return new ActivatableData("ActivatorAnchor", transform.position, transform.eulerAngles, gameObject.GetEntityId(), _trigger.gameObject.GetEntityId());
	}

	public void Deserialize(ActivatableData data) {
		transform.position = data.Position;
		transform.eulerAngles = data.Rotation;
	}
}
