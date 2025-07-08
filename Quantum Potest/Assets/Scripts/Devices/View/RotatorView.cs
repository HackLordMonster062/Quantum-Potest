using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class RotatorView : MonoBehaviour {

	public event Action OnRotationEnd;

	Animator _animator;

	private void Awake() {
		_animator = GetComponent<Animator>();
	}

	public void Rotate() {
		_animator.SetTrigger("Rotate");
	}

	void FinalizeRotation() {
		OnRotationEnd?.Invoke();
	}
}
