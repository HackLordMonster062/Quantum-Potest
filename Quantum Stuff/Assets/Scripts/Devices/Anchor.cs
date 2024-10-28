using UnityEngine;

public class Anchor : Activatable {
    [SerializeField] float holdingHeight;
    [SerializeField] float pullingForce;
    [SerializeField] float dampingForce;

    protected ParticleBehavior _particle;

    protected bool _isEnabled = true;

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
                _particle.OnPickedUp += Pickup;

                if (other.GetComponentInChildren<ActivatorParticle>() != null)
                    _isEnabled = false;
            }
        }
	}

    protected virtual void Pickup() {
        
    }

	public override void Activate() {
		
	}
}
