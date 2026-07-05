using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CapturableParticle))]
public class ActivatorParticle : Particle, ISerializableElement {
	CapturableParticle _capturable;

	ActivatorAnchor _anchor;

	Queue<(float, int)> _excitations;

	protected override void Awake() {
		base.Awake();
		_capturable = GetComponent<CapturableParticle>();
		_capturable.OnCapture += OnCapture;
		_capturable.OnRelease += OnRelease;

		_excitations = new();
	}

	protected override void UpdateEnergy() {
		while (_excitations.Count > 0 && Time.time - _excitations.Peek().Item1 > PhysicsManager.instance.RelaxtationTime - .2f) {
			_excitations.Dequeue();
			Decay();
		}
	}

	public override void Excite(int energy, bool invoke = true) {
		SetFloat("_EnergyChangeTime", Time.time);

		_excitations.Enqueue((Time.time, energy));
		base.Excite(energy, invoke);

		UpdateEnergy();

		_anchor?.Activate(Energy);
	}

	protected override void Decay() {
		SetFloat("_EnergyChangeTime", Time.time);
		base.Decay();
	}

	protected override void Deplete() {
		SetFloat("_EnergyChangeTime", Time.time);
		base.Deplete();

		if (_anchor != null) {
			_anchor.Deactivate();
		}
	}

	void OnCapture(Capturable _, CapturerStrengh __) {
		if (_capturable.Capturer != null) {
			if (_capturable.Capturer.TryGetComponent(out ActivatorAnchor activator)) {
				_anchor = activator;

				SetFloat("_CapturedChangeTime", Time.time);
				SetInt("_Is_Captured", 1);
			} else {
				_anchor = null;
			}
		}
	}

	void OnRelease(Capturable _) {
		if (_anchor != null) {
			if (Energy > 0)
				_anchor.Deactivate();
			_anchor = null;

			SetFloat("_CapturedChangeTime", Time.time);
			SetInt("_Is_Captured", 0);
		}
	}

	public ElementData Serialize() {
		return new CatalystData(transform.localPosition, transform.eulerAngles, Energy);
	}

	public void Deserialize(ElementData data) {
		CatalystData casted = (CatalystData)data;

		transform.localPosition = casted.Position;
		transform.eulerAngles = casted.Rotation;
		Energy = casted.Energy;
	}
}
