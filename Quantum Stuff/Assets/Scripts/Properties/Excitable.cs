using System;
using UnityEngine;

public class Excitable : MonoBehaviour {
	public event Action<Excitable, int> OnExcite;

	public int Energy { get; protected set; }

	float _glowingTimer;

	protected MeshRenderer _renderer;
	protected Color _baseColor;

	protected bool depleted = true;

	protected virtual void Awake() {
		_renderer = GetComponent<MeshRenderer>();
		_baseColor = _renderer.material.GetColor("_EmissionColor");
	}

	protected virtual void Update() {
		_renderer.material.SetColor("_EmissionColor", _baseColor * (Energy + 1));

		if (depleted) return;

		_glowingTimer -= Time.deltaTime;

		if (_glowingTimer < 0) {
			_glowingTimer = PhysicsManager.instance.RelaxtationTime;

			Energy--;
			Decay();

			if (Energy <= 0) {
				Energy = 0;

				Deplete();
			}
		}
    }

	public virtual void Excite(int energy) {
		Energy += energy;
		depleted = false;

		_glowingTimer = PhysicsManager.instance.RelaxtationTime;

		OnExcite?.Invoke(this, energy);
	}

	protected virtual void Decay() {
	}

	protected virtual void Deplete() {
		depleted = true;
	}
}
