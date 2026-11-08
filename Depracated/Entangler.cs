using System.Collections.Generic;
using UnityEngine;

public class Entangler : MonoBehaviour {
    bool _isEnabled = true;

	List<Excitable> particles;

	private void Awake() {
		particles = new List<Excitable>();
	}

	private void OnTriggerEnter(Collider other) {
		if (!_isEnabled || !other.TryGetComponent(out Excitable particle)) return;

		particles.Add(particle);

		print("added");

		if (particles.Count >= 2) {
			EntanglementManager.instance.RegisterPair(particles[0], particles[1]);

			_isEnabled = false;
		}
	}

	private void OnTriggerExit(Collider other) {
		if (!other.TryGetComponent(out Excitable particle)) return;

		particles.Remove(particle);

		if (particles.Count < 2) {
			_isEnabled = true;
		}
	}
}
