using System.Collections.Generic;
using UnityEngine;

public class PotentialWell : MonoBehaviour {
    [SerializeField] float captureDistance;
    [SerializeField] float maxDistance;
    [SerializeField] float forceMultiplier;

    HashSet<ParticleBehavior> captured;

    void Awake() {
        captured = new ();
    }

    void Update() {
        Collider[] colliders = Physics.OverlapSphere(transform.position, captureDistance);

        foreach (Collider collider in colliders) {
            if (collider.TryGetComponent<ParticleBehavior>(out var particle))
                captured.Add(particle);
        }

        foreach (ParticleBehavior particle in captured) {
            Vector3 delta = particle.transform.position - transform.position;
            float distance = delta.magnitude;

            if (distance > maxDistance) {
                delta = delta.normalized * maxDistance;

                particle.transform.position = transform.position + delta;
            }

            if (forceMultiplier > 0) {
                Vector3 force = -delta.normalized * (Mathf.Cos(distance / maxDistance * Mathf.PI / 2) + .05f) * forceMultiplier;

                particle.Rb.AddForce(force);
            }
        }
    }
}
