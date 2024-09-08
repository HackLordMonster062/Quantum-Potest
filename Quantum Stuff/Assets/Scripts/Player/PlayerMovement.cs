using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour {
	[Header("Values")]
	[SerializeField] float walkSpeed;
	[SerializeField] float sensitivity;
	[SerializeField] float lowBound;
	[SerializeField] float highBound;

	[Header("Physics")]
	[SerializeField] float mass;

	Rigidbody _rb;

	// Runtime
	Vector3 _direction;

	Transform _cameraRef;

	float _xInput, _yInput;
	float _xRotation, _yRotation;

	private void Awake() {
		_rb = GetComponent<Rigidbody>();
		_cameraRef = Camera.main.transform;

		_xRotation = 90;
	}

	void FixedUpdate() {
		_direction = transform.forward * Input.GetAxis("Vertical") + transform.right * Input.GetAxis("Horizontal");
		_direction = Vector3.ClampMagnitude(_direction, 1);

		_rb.velocity = (_direction * walkSpeed).Modify(y: _rb.velocity.y);
	}

	void Update() {
		_xInput = Input.GetAxis("Mouse X") * sensitivity;
		_yInput = -Input.GetAxis("Mouse Y") * sensitivity;

		_xRotation += _xInput;
		_yRotation = Mathf.Clamp(_yRotation + _yInput, lowBound, highBound);

		transform.rotation = Quaternion.Euler(0f, _xRotation, 0);
		_cameraRef.localRotation = Quaternion.Euler(_yRotation, 0, 0);
	}
}