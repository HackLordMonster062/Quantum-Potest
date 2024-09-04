using UnityEngine;

public class Excitable : MonoBehaviour {
	public int Energy { get; protected set; }

	float _timer;

	protected bool depleted = true;

	protected virtual void Awake() {

	}

	protected virtual void Update() {
		if (depleted) return;

		_timer -= Time.deltaTime;

		if (_timer < 0) {
			_timer = PhysicsManager.instance.DecayTime;

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

		_timer = PhysicsManager.instance.DecayTime;
	}

	protected virtual void Decay() {
	}

	protected virtual void Deplete() {
		depleted = true;
	}
}
