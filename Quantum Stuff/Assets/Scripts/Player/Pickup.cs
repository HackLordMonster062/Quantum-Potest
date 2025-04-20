using UnityEngine;

public class Pickup : Capturer {
	[SerializeField] float castWidth;
	[SerializeField] float reach;
	[SerializeField] float pullingForce;
	[SerializeField] float maxReleaseSpeed;

	Transform _camera;

	Capturable _particle;

    void Awake() {
		_camera = Camera.main.transform;
	}

    void Update() {
        if (Input.GetMouseButtonDown(0)) {
            if (Physics.SphereCast(_camera.position, castWidth, _camera.forward, out RaycastHit info, reach, -1, QueryTriggerInteraction.Ignore)) {
				if (info.collider.TryGetComponent(out Capturable particle) && TryCapture(particle))
					_particle = particle;
			}
        }

		if (_particle == null) return;

		if (Input.GetMouseButtonUp(0)) {
			Release(_particle);

			return;
		}
    }

	private void FixedUpdate() {
		if (_particle == null) return;

		Vector3 target = _camera.position + _camera.forward * reach;

		_particle.MoveTo(target, pullingForce);
	}

	protected override void Release(Capturable capturable) {
		_particle.Release();

		_particle = null;
	}

	protected override void Give(Capturable capturable, CapturerStrengh strengh) {
		base.Give(capturable, strengh);
		_particle = null;
	}
}
