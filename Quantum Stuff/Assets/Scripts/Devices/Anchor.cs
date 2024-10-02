using UnityEngine;

public class Anchor : MonoBehaviour {
    [SerializeField] float holdingHeight;
    [SerializeField] float pullingForce;
    [SerializeField] float dampingForce;

    ParticleBehavior _particle; 

    void Start() {
        
    }

    void FixedUpdate() {
        if (_particle == null) return;

        Vector3 target = transform.position + transform.up * holdingHeight;

        Vector3 damping = -_particle.Rb.velocity * dampingForce;

        _particle.Rb.AddForce((target - _particle.transform.position) * pullingForce + damping);
    }

	private void OnTriggerEnter(Collider other) {
		if (_particle == null) {
            _particle = other.GetComponent<ParticleBehavior>();
        }
	}
}
