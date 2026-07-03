using UnityEngine;

[RequireComponent(typeof(Anchor))]
public class SpinDevice : Activatable, ISerializableElement {
	[SerializeField] SpinnerView view;

	Anchor _anchor;

	int _currRotation;

	private void Awake() {
		_anchor = GetComponent<Anchor>();
	}

	public override void Activate(int energy = 1) {
		view.OnSpinFlipEnd += CommitRotation;
		_currRotation = energy;

		for (int i = 0; i < energy; i++)
			view.Flip();
	}

	void CommitRotation() {
		if (_anchor.Particle != null && _anchor.Particle.TryGetComponent(out RotateableBase particle)) {
			particle.FlipSpin();
		}

		_currRotation--;
		if (_currRotation <= 0) {
			view.OnSpinFlipEnd -= CommitRotation;
		}
	}

	public ElementData Serialize() {
		return new ActivatableData("Spinner", transform.position, transform.eulerAngles, gameObject.GetEntityId().ToString(), _trigger == null ? "" : _trigger.gameObject.GetEntityId().ToString());
	}

	public void Deserialize(ElementData data) {
		transform.position = data.Position;
		transform.eulerAngles = data.Rotation;
	}
}
