using System;
using UnityEngine;

public class Trigger : MonoBehaviour {
	public event Action OnTrigger;
	public event Action OnUntrigger;

	protected void Activate() {
		OnTrigger?.Invoke();
	}

	protected void Deactivate() {
		OnUntrigger?.Invoke();
	}
}
