using System.Collections.Generic;
using UnityEngine;

public abstract class Activatable : MonoBehaviour {
	float _timer = 0;

	protected virtual void Update() {
		_timer -= Time.deltaTime;

		if (_timer < 0) {
			_timer = PhysicsManager.instance.RelaxtationTime;

			Deactivate();
		}
	}

	public abstract void Activate();
	protected virtual void Deactivate() { }
}
