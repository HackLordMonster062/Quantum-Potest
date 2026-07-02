using UnityEngine;

public class SuspenderAnchor : ActivatorAnchor, ISerializableElement<ActivatableData> {
	Anchor _anchor;

	protected override void Awake() {
		base.Awake();
		_anchor = GetComponent<Anchor>();
	}

	private void OnEnable() {
		if (_anchor != null) {
			_anchor.OnCapture += HandleCapture;
			_anchor.OnRelease += HandleRelease;
		}
	}

	private void OnDisable() {
		if (_anchor != null) {
			_anchor.OnCapture -= HandleCapture;
			_anchor.OnRelease -= HandleRelease;
		}
	}

	void HandleCapture(Capturable _) { // On
		base.Activate(1);
	}

	void HandleRelease(Capturable _) { // Off
		base.Deactivate();
	}

	public override void Activate(int energy) { // Off
		if (_anchor.Particle == null) return;

		base.Deactivate();
	}

	public override void Deactivate() { // On
		if (_anchor.Particle == null) return;

		base.Activate(1);
	}

	public new ActivatableData Serialize() {
		return new ActivatableData("SuspenderAnchor", transform.position, transform.eulerAngles, gameObject.GetEntityId(), _trigger.gameObject.GetEntityId());
	}
}
