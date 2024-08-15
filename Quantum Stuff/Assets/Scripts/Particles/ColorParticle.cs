using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class ColorParticle : MonoBehaviour, ILiftable {
	[SerializeField] List<int> frequencies;

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
		if (_hasCollapsed) return;

		if (_timer <= 0) {
			_currColorIndex = (_currColorIndex + 1) % frequencies.Count;

			SetColor(frequencies[_currColorIndex]);

			_timer = VisualManager.instance.ColorSwitchTime;
		}

		_timer -= Time.deltaTime;
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

	public void PickUp() {
		_rb.isKinematic = true;
		_collider.enabled = false;
	}

	public void Drop() {
		_rb.isKinematic = false;
		_collider.enabled = true;
	}
}
