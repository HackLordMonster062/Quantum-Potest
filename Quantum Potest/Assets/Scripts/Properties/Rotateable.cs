using System;
using UnityEngine;

public class Rotateable : RotateableBase {
	public Spin Spin { get; private set; } = Spin.Horizontal;

	Vector3 _horizontalDirection = Vector3.zero;
	Vector3 _verticalDirection = new Vector3(90, 0, 0);

	public override void Rotate() {
		base.Rotate();

		switch (Spin) {
			case Spin.Horizontal:
				_horizontalDirection.y += 90;

				transform.eulerAngles = _horizontalDirection;
				break;
			case Spin.Vertical:
				_verticalDirection *= -1;

				transform.eulerAngles = _verticalDirection;
				break;
		}
	}

	public override void FlipSpin() {
		base.FlipSpin();

		switch (Spin) {
			case Spin.Horizontal:
				Spin = Spin.Vertical;

				transform.eulerAngles = _verticalDirection;
				break;
			case Spin.Vertical:
				Spin = Spin.Horizontal;

				transform.eulerAngles = _horizontalDirection;
				break;
		}
	}

	public void SetSpin(Spin spin) {
		if (spin != Spin) FlipSpin();
	}
}

public enum Spin {
	Vertical,
	Horizontal,
}