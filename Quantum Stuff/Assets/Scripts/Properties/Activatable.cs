using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class Activatable : MonoBehaviour {
	[SerializeField] Trigger trigger;

	public event Action<int> OnActivate;
	public event Action OnDeactivate;

	private void OnEnable() {
		if (trigger != null) {
			trigger.OnTrigger += Activate;
			trigger.OnUntrigger += Deactivate;
		}
	}

	private void OnDisable() {
		if (trigger != null) {
			trigger.OnTrigger -= Activate;
			trigger.OnUntrigger -= Deactivate;
		}
	}

	public virtual void Activate(int energy = 0) { OnActivate?.Invoke(energy); }
	public virtual void Deactivate() { OnDeactivate?.Invoke(); }

	public void SetTrigger(Trigger newTrigger) {
		if (trigger != null) {
			trigger.OnTrigger -= Activate;
			trigger.OnUntrigger -= Deactivate;
		}
		trigger = newTrigger;
		if (trigger != null) {
			trigger.OnTrigger += Activate;
			trigger.OnUntrigger += Deactivate;
		}
	}
}
