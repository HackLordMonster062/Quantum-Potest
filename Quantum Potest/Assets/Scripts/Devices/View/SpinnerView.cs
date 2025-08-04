using System;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class SpinnerView : MonoBehaviour {

	public event Action OnSpinFlipEnd;

	Animator _animator;

	private void Awake() {
		_animator = GetComponent<Animator>();
	}

	public void Flip() {
		_animator.SetTrigger("Flip");
	}

	void OnSpinFlipFinished() {
		OnSpinFlipEnd?.Invoke();
	}
}
