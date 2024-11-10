using System.Collections.Generic;
using UnityEngine;

public class Spectron : Excitable, IRotateable {
	[SerializeField] List<int> frequencies;
	[SerializeField] float minRadius;
	[SerializeField] float radiusLeaps;
	[SerializeField] LayerMask coloredObjectsLayer;

	bool _hasCollapsed = false;
	int _currColor;
	int _currColorIndex;
	float _timer;

	float _checkingRadius;

	protected override void Update() {
		base.Update();

		UpdateColor();

		_checkingRadius = minRadius + radiusLeaps * Energy;

		if (!_hasCollapsed) return;

		Collider[] colliders = Physics.OverlapSphere(transform.position, _checkingRadius, coloredObjectsLayer);

		foreach (Collider collider in colliders) {
			if (collider.TryGetComponent(out FrequencyDoor door) && _currColor == door.Frequency) {
				door.Annihilate();
			}
		}
	}

	public void FilterColors(int maxFreq) {
		List<int> filteredList = new();

		foreach (int frequency in frequencies) {
			if (frequency <= maxFreq) 
				filteredList.Add(frequency);
		}

		if (filteredList.Count <= 0) {
			Annihilate();
			return;
		}

		frequencies = filteredList;
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

	private void SetColor(int frequency) {
		_currColor = frequency;
		_renderer.material.color = VisualManager.instance.FrequencyToColor(frequency);
		_baseColor = VisualManager.instance.FrequencyToColor(frequency) / 2;
	}

	void Annihilate() {
		Destroy(gameObject);
	}

	public void Rotate() { }

	public void FlipSpin() {
		for (int i = 0; i < frequencies.Count; i++) {
			frequencies[i] = VisualManager.instance.MaxFrequency - frequencies[i];
		}
	}
}
