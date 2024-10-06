using UnityEngine;

public class Rotateable : MonoBehaviour {
	Spin _spin = Spin.Horizontal;

	Vector3 _horizontalDirection = Vector3.zero;
	Vector3 _verticalDirection = new Vector3(90, 0, 0);

	public void Rotate() {
		switch (_spin) {
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

	public void FlipSpin() {
		switch (_spin) {
			case Spin.Horizontal:
				_spin = Spin.Vertical;

				transform.eulerAngles = _verticalDirection;
				break;
			case Spin.Vertical:
				_spin = Spin.Horizontal;

				transform.eulerAngles = _horizontalDirection;
				break;
		}
	}
}

enum Spin {
	Vertical,
	Horizontal,
}