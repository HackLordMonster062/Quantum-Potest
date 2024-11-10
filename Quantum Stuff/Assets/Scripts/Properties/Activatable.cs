using System.Collections.Generic;
using UnityEngine;

public abstract class Activatable : MonoBehaviour {
	[SerializeField] Trigger trigger;

	float _timer = 0;

	private void Start() {
		if (trigger != null) {
			trigger.OnTrigger += Activate;
			trigger.OnUntrigger += Deactivate;
		}
	}

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
