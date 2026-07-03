using UnityEngine;

[RequireComponent(typeof(Anchor))]
[RequireComponent(typeof(Trigger))]
public class ActivatorAnchor : Activatable, ISerializableElement {
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

	public ElementData Serialize() {
		return new ActivatableData("ActivatorAnchor", transform.position, transform.eulerAngles, gameObject.GetEntityId().ToString(), _trigger == null ? "" : _trigger.gameObject.GetEntityId().ToString());
	}

	public void Deserialize(ElementData data) {
		transform.position = data.Position;
		transform.eulerAngles = data.Rotation;
	}
}
