using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class ColorParticle : MonoBehaviour {
	[SerializeField] List<int> frequencies;
	[SerializeField] float checkingRadius;
	[SerializeField] LayerMask coloredObjectsLayer;

	Rigidbody _rb;
	Collider _collider;
	MeshRenderer _renderer;

	bool _hasCollapsed = false;
	int _currColor;
	int _currColorIndex;
	float _timer;

	private void Awake() {
		_rb = GetComponent<Rigidbody>();
		_collider = GetComponent<Collider>();

		_renderer = GetComponent<MeshRenderer>();
	}

	private void Start() {
		_currColorIndex = 0;
		SetColor(frequencies[_currColorIndex]);
	}

	public void Update() {
		UpdateColor();

		Collider[] colliders = Physics.OverlapSphere(transform.position, checkingRadius, coloredObjectsLayer);

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
