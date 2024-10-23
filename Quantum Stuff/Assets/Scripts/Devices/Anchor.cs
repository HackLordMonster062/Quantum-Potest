using UnityEngine;

public class Anchor : Activatable {
    [SerializeField] float holdingHeight;
    [SerializeField] float pullingForce;
    [SerializeField] float dampingForce;

    ParticleBehavior _particle;

    bool _isEnabled = true;

    void FixedUpdate() {
        if (_particle == null) return;

        Vector3 target = transform.position + transform.up * holdingHeight;

        Vector3 damping = -_particle.Rb.velocity * dampingForce;

        _particle.Rb.AddForce((target - _particle.transform.position) * pullingForce + damping);
    }

	private void OnTriggerEnter(Collider other) {
		if (_particle == null) {
            _particle = other.GetComponent<ParticleBehavior>();

            if (_particle != null ) {
                _particle.OnPickedUp += Release;

                if (other.GetComponentInChildren<ActivatorParticle>() != null)
                    _isEnabled = false;
            }
        }
	}

    void Release() {
        _particle.OnPickedUp -= Release;
        _particle = null;
        _isEnabled = true;
    }

    public override void Activate() {
        if (_isEnabled && _particle != null && _particle.TryGetComponent(out Excitable excitable)) {
            excitable.Excite(1);
        }
    }
}
