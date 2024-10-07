using System.Collections.Generic;
using UnityEngine;

public abstract class Activatable : MonoBehaviour {
	protected List<ActivatorParticle> _activators;

	private void Awake() {
		_activators = new List<ActivatorParticle>();
	}

	public void AddActivator(ActivatorParticle activator) {
		_activators.Add(activator);

		if (_activators.Count == 1)
			Activate();
	}

	public void RemoveActivator(ActivatorParticle activator) {
		_activators.Remove(activator);

		if (_activators.Count == 0)
			Deactivate();
	}

	protected abstract void Activate();
	protected abstract void Deactivate();
}
