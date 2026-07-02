using UnityEngine;

[RequireComponent(typeof(Anchor))]
public class DispatchAnchor : Activatable, ISerializableElement<ActivatableData> {

	Anchor _anchor;

	private void Awake() {
		_anchor = GetComponent<Anchor>();
	}

	public override void Activate(int energy) {
		base.Activate(energy);
		_anchor.ForceRelease();

		_anchor.IsActive = false;
	}

	public override void Deactivate() {
		base.Deactivate();
		_anchor.IsActive = true;
	}

	public ActivatableData Serialize() {
		return new ActivatableData("Anchor", transform.position, transform.eulerAngles, gameObject.GetEntityId(), _trigger.gameObject.GetEntityId());
	}

	public void Deserialize(ActivatableData data) {
		transform.position = data.Position;
		transform.eulerAngles = data.Rotation;
	}
}