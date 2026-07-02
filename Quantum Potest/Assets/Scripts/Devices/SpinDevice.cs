using UnityEngine;

[RequireComponent(typeof(Anchor))]
public class SpinDevice : Activatable, ISerializableElement<ActivatableData> {
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

	public ActivatableData Serialize() {
		return new ActivatableData("Spinner", transform.position, transform.eulerAngles, gameObject.GetEntityId(), _trigger.gameObject.GetEntityId());
	}

	public void Deserialize(ActivatableData data) {
		transform.position = data.Position;
		transform.eulerAngles = data.Rotation;
	}
}
