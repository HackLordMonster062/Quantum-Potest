using UnityEngine;

[RequireComponent(typeof(Anchor))]
public class RotatingDevice : Activatable, ISerializableElement {
	[SerializeField] RotatorView view;

	Anchor _anchor;

	int _currRotation;

	private void Awake() {
		_anchor = GetComponent<Anchor>();
	}

	public override void Activate(int energy = 1) {
		view.OnRotationEnd += CommitRotation;
		_currRotation = energy;

		for (int i = 0; i < energy; i++)
			view.Rotate();
	}

	void CommitRotation() {
		if (_anchor.Particle != null && _anchor.Particle.TryGetComponent(out RotateableBase particle)) {
			particle.Rotate();
		}

		_currRotation--;
		if (_currRotation <= 0) {
			view.OnRotationEnd -= CommitRotation;
		}
	}

	public ElementData Serialize() {
		return new ActivatableData("Rotator", transform.localPosition, transform.eulerAngles, gameObject.GetEntityId().ToString(), _trigger == null ? "" : _trigger.gameObject.GetEntityId().ToString());
	}

	public void Deserialize(ElementData data) {
		transform.localPosition = data.Position;
		transform.eulerAngles = data.Rotation;
	}
}
