using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateableBase : MonoBehaviour {
	public event Action OnRotate;
	public event Action OnFlipSpin;

	public virtual void Rotate() {
		OnRotate?.Invoke();
	}

	public virtual void FlipSpin() {
		OnFlipSpin?.Invoke();
	}
}
