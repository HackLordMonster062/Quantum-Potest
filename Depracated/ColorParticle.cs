using System.Collections.Generic;
using UnityEngine;

public class ColorParticle : Excitable {
	[SerializeField] List<int> frequencies;
	[SerializeField] float minRadius;
	[SerializeField] float radiusLeaps;
	[SerializeField] LayerMask coloredObjectsLayer;

	bool _hasCollapsed = false;
	int _currColor;
	int _currColorIndex;
	float _timer;

	float _checkingRadius;

	private void Start() {
		_currColorIndex = 0;
		SetColor(frequencies[_currColorIndex]);
	}

	protected override void Update() {
		base.Update();

		UpdateColor();

		_checkingRadius = minRadius + radiusLeaps * Energy;

		Collider[] colliders = Physics.OverlapSphere(transform.position, _checkingRadius, coloredObjectsLayer);

		foreach (Collider collider in colliders) {
			if (collider.TryGetComponent(out FrequencyDoor door) && HasFrequency(door.Frequency)) { // TODO: Generalize
				SetColor(door.Frequency);
				_hasCollapsed = true;

				door.Annihilate();
			}
		}
	}

	public bool HasFrequency(int frequency) {
		if (_hasCollapsed) {
			return frequency == _currColor;
		}

		if (frequencies.Contains(frequency)) {
			_hasCollapsed = true;
			SetColor(frequency);

			return true;
		}

		return false;
	}

	private void SetColor(int frequency) {
		_currColor = frequency;
		_renderer.material.color = VisualManager.instance.FrequencyToColor(frequency);
		_baseColor = VisualManager.instance.FrequencyToColor(frequency) / 2;
	}

	private void UpdateColor() {
		if (_hasCollapsed) return;

		if (_timer <= 0) {
			_currColorIndex = (_currColorIndex + 1) % frequencies.Count;

			SetColor(frequencies[_currColorIndex]);

			_timer = VisualManager.instance.ColorSwitchTime;
		}

		_timer -= Time.deltaTime;
	}
}
