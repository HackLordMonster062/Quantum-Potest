using UnityEngine;

public abstract class Anchor : Activatable {
    [SerializeField] float holdingHeight;
    [SerializeField] float pullingForce;
    [SerializeField] float dampingForce;

    protected Particle _particle;

    void FixedUpdate() {
        if (_particle == null || _particle.Behavior.IsPickedUp) return;

        Vector3 target = transform.position + transform.up * holdingHeight;

        Vector3 damping = -_particle.Behavior.Rb.velocity * dampingForce;

        _particle.Behavior.Rb.AddForce((target - _particle.transform.position) * pullingForce + damping);
    }

	private void OnTriggerEnter(Collider other) {
		if (_particle == null) {
            Particle particle = other.GetComponent<Particle>();

            if (particle != null && particle.TryCapture(this)) {
                _particle = particle;
                _particle.Behavior.OnPickedUp += Pickup;
            }
        }
	}

    protected virtual void Pickup() {
		_particle.Behavior.OnPickedUp -= Pickup;
        Release();
	}

    protected void Release() {
        _particle.Release();
		_particle = null;
	}
}
