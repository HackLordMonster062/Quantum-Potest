using UnityEngine;

public class Pickup : MonoBehaviour {
	[SerializeField] float castWidth;
	[SerializeField] float reach;
	[SerializeField] float pullingForce;
	[SerializeField] float dampingForce;
	[SerializeField] float maxReleaseSpeed;

	Transform _camera;

	ParticleBehavior _particle;

    void Awake() {
		_camera = Camera.main.transform;
	}

    void Update() {
        if (Input.GetMouseButtonDown(0)) {
            if (Physics.SphereCast(_camera.position, castWidth, _camera.forward, out RaycastHit info, reach, -1, QueryTriggerInteraction.Ignore)) {
				if (info.collider.TryGetComponent(out _particle))
					_particle.PickUp();
			}
        }

		if (_particle == null) return;

		if (Input.GetMouseButtonUp(0)) {
			_particle.Rb.velocity = Vector3.ClampMagnitude(_particle.Rb.velocity, maxReleaseSpeed);
			_particle = null;

			return;
		}
    }

	private void FixedUpdate() {
		if (_particle == null) return;

		Vector3 diff = _camera.position + _camera.forward * reach - _particle.transform.position;
		Vector3 damping = -_particle.Rb.velocity * dampingForce;

		_particle.Rb.AddForce(diff * pullingForce + damping);
	}
}
