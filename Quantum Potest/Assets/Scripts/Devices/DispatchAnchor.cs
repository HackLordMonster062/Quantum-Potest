using UnityEngine;

[RequireComponent(typeof(Anchor))]
public class DispatchAnchor : Activatable, ISerializableElement {

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

	public ElementData Serialize() {
		return new ActivatableData("Anchor", transform.localPosition, transform.eulerAngles, transform.localScale, gameObject.GetEntityId().ToString(), _trigger == null ? "" : _trigger.gameObject.GetEntityId().ToString());
	}

	public void Deserialize(ElementData data) {
		transform.localPosition = data.Position;
		transform.eulerAngles = data.Rotation;
		transform.localScale = data.Scale;
	}
}