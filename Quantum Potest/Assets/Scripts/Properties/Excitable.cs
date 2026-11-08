using System;
using System.Collections.Generic;
using UnityEngine;

public class Excitable : MonoBehaviour {
	public event Action<int> OnExcite;

	public int Energy { get; protected set; }

	protected MeshRenderer _renderer;

	protected bool depleted = true;
	float _glowingTimer;

	protected MaterialPropertyBlock mpb;

	protected virtual void Awake() {
		_renderer = GetComponent<MeshRenderer>();

		mpb = new();
	}

	protected virtual void Update() {
		mpb.SetFloat("_Energy", Energy);

		_renderer.SetPropertyBlock(mpb);

		if (depleted) return;

		UpdateEnergy();
	}

	protected virtual void UpdateEnergy() {
		_glowingTimer -= Time.deltaTime;

		if (_glowingTimer < 0) {
			Decay();
			_glowingTimer = PhysicsManager.instance.RelaxtationTime;
		}
	}

	public virtual void Excite(int energy, bool invoke=true) {
		if (Energy == 0)
			_glowingTimer = PhysicsManager.instance.RelaxtationTime;

		Energy += energy;
		depleted = false;

		if (Energy > PhysicsManager.instance.MaxStableEnergy) {
			Destabalize();
		}

		if (invoke) OnExcite?.Invoke(Energy);
	}

	protected virtual void Decay() {
		Energy--;

		if (Energy <= 0) {
			Energy = 0;

			Deplete();
		}
	}

	protected virtual void Deplete() {
		depleted = true;
	}

	protected void SetFloat(string name, float value) {
		mpb.SetFloat(name, value);

		_renderer.SetPropertyBlock(mpb);
	}

	protected void SetInt(string name, int value) {
		mpb.SetInt(name, value);

		_renderer.SetPropertyBlock(mpb);
	}

	protected virtual void Destabalize() {
		Destroy(gameObject);
	}
}
