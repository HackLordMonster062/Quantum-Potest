using UnityEngine;

[RequireComponent (typeof(Rigidbody))]
public class ChargedParticle : MonoBehaviour {
    [SerializeField] int charge;
    [SerializeField] float repelForce;

    public int Charge { get { return charge; } }

    public Rigidbody Rb { get; protected set; }

    void Start() {
        Rb = GetComponent<Rigidbody>();
    }

    void Update() {
        
    }

	private void OnCollisionEnter(Collision collision) {
		if (collision.gameObject.TryGetComponent(out ChargedParticle other)) {
            if (other.Charge * charge < 0) {
                Destroy(other.gameObject);
                Destroy(gameObject);
            } else {
                Vector3 normal = collision.contacts[0].normal;

                Rb.AddForce(normal * repelForce);
                other.Rb.AddForce(-normal * repelForce);
            }
        }
	}
}
