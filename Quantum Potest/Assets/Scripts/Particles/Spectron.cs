using System;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(RotateableBase))]
public class Spectron : Particle, ISerializableElement<SpectronData> {
	[SerializeField] List<int> frequencies;
	[SerializeField] float minRadius;
	[SerializeField] float radiusLeaps;
	[SerializeField] LayerMask coloredObjectsLayer;

	public event Action OnAnnihilate;

	RotateableBase _rotateable;

	bool _hasCollapsed = false;
	int _currColor;
	int _currColorIndex;
	float _timer;

	float _checkingRadius;

	protected override void Awake() {
		base.Awake();

		_rotateable = GetComponent<RotateableBase>();

		_rotateable.OnFlipSpin += FlipSpin;

		if (frequencies.Count == 1) {
			_hasCollapsed = true;
		}
	}

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

		if (filteredList.Count == 1) {
			_hasCollapsed = true;
		}

		frequencies = filteredList;
	}

	private void UpdateColor() {
		if (_timer <= 0) {
			_currColorIndex = (_currColorIndex + 1) % frequencies.Count;

			SetColor(frequencies[_currColorIndex]);

			_timer = VisualManager.instance.ColorSwitchTime;
		}

		_timer -= Time.deltaTime;
	}

	private void SetColor(int frequency) {
		_currColor = frequency;

		mpb.SetColor("_Base", VisualManager.instance.FrequencyToColor(frequency));

		_renderer.SetPropertyBlock(mpb);
	}

	void Annihilate() {
		OnAnnihilate?.Invoke();
		Destroy(gameObject);
	}

	public void FlipSpin() {
		for (int i = 0; i < frequencies.Count; i++) {
			frequencies[i] = VisualManager.instance.MaxFrequency - frequencies[i] + 1;
		}
	}

	public void SetFrequencies(List<int> newFrequencies) {
		frequencies = newFrequencies;

		if (frequencies.Count > 1) {
			_hasCollapsed = false;
			_currColorIndex = 0;
			_currColor = frequencies[_currColorIndex];
			SetColor(_currColor);
		} else {
			_hasCollapsed = true;
			_currColor = frequencies[0];
			SetColor(_currColor);
		}
	}

	public SpectronData Serialize() {
		return new SpectronData(transform.position, transform.eulerAngles, Energy, frequencies);
	}

	public void Deserialize(SpectronData data) {
		transform.position = data.Position;
		transform.eulerAngles = data.Rotation;
		Energy = data.Energy;
		frequencies = data.Frequencies;
	}
}
