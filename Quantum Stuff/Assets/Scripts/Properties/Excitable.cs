using System;
using System.Collections.Generic;
using UnityEngine;

public class Excitable : MonoBehaviour {
	public event Action<int> OnExcite;

	public int Energy { get; protected set; }

	protected MeshRenderer _renderer;

	protected bool depleted = true;

	Queue<(float, int)> _excitations;

	protected virtual void Awake() {
		_renderer = GetComponent<MeshRenderer>();

		_excitations = new();
	}

	protected virtual void Update() {
		_renderer.material.SetFloat("_Energy", Energy);

		if (depleted) return;

		while (_excitations.Count > 0 && Time.time - _excitations.Peek().Item1 > PhysicsManager.instance.RelaxtationTime) {
			var (_, energy) = _excitations.Dequeue();
			Decay();

			Energy -= energy;

			if (Energy <= 0) {
				Energy = 0;

				Deplete();
			}
		}
    }

	public virtual void Excite(int energy, bool invoke=true) {
		_excitations.Enqueue((Time.time, energy));

		Energy += energy;
		depleted = false;

		if (invoke) OnExcite?.Invoke(Energy);
	}

	protected virtual void Decay() { }

	protected virtual void Deplete() {
		depleted = true;
	}
}
