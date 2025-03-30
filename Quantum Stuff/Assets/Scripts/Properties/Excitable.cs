using System;
using UnityEngine;

public class Excitable : MonoBehaviour {
	public event Action<int> OnExcite;

	public int Energy { get; protected set; }

	float _glowingTimer;

	protected MeshRenderer _renderer;
	protected float _baseIntensity;

	protected bool depleted = true;

	protected virtual void Awake() {
		_renderer = GetComponent<MeshRenderer>();
		_baseIntensity = _renderer.material.GetFloat("_Emission_value");
	}

	protected virtual void Update() {
		_renderer.material.SetFloat("_Emission_value", _baseIntensity * (Energy + 1));

		if (depleted) return;

		_glowingTimer -= Time.deltaTime;

		if (_glowingTimer < 0) {
			Energy--;
			Decay();

			if (Energy <= 0) {
				Energy = 0;

				Deplete();
			}
		}
    }

	public virtual void Excite(int energy, bool invoke=true) {
		if (Energy == 0)
			_glowingTimer = PhysicsManager.instance.RelaxtationTime;

		Energy += energy;
		depleted = false;

		if (invoke) OnExcite?.Invoke(Energy);
	}

	protected virtual void Decay() {
		_glowingTimer = PhysicsManager.instance.RelaxtationTime;
	}

	protected virtual void Deplete() {
		depleted = true;
	}
}
