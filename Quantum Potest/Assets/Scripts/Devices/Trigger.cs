using System;
using UnityEngine;

public class Trigger : MonoBehaviour {
	public event Action<int> OnTrigger;
	public event Action OnUntrigger;

	public void Activate(int energy) {
		OnTrigger?.Invoke(energy);
	}

	public void Deactivate() {
		OnUntrigger?.Invoke();
	}
}
