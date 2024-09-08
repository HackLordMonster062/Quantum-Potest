using UnityEngine;

public class Anchor : MonoBehaviour {
    [SerializeField] float holdingHeight;

    ParticleBehavior _particle; 

    void Start() {
        
    }

    void Update() {
        if (_particle == null) return;

        Vector3 target = transform.position + transform.up * holdingHeight;

        _particle.Rb.AddForce(target - _particle.transform.position);
    }

	private void OnTriggerEnter(Collider other) {
		if (_particle == null) {
            _particle = other.GetComponent<ParticleBehavior>();
        }
	}
}
