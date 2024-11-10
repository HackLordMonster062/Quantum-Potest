using System.Collections.Generic;
using UnityEngine;

public class PotentialWell : MonoBehaviour {
    [SerializeField] float captureDistance;
    [SerializeField] float maxDistance;
    [SerializeField] float forceMultiplier;
    [SerializeField] float effectiveMass;

    HashSet<ParticleBehavior> _captured;

    bool _enabled = true;

    void Awake() {
        _captured = new ();
    }

    void Update() {
        if (!_enabled) return;

        Collider[] colliders = Physics.OverlapSphere(transform.position, captureDistance);

        foreach (Collider collider in colliders) {
            if (collider.TryGetComponent<ParticleBehavior>(out var particle) && particle.Mass < effectiveMass)
                _captured.Add(particle);
        }

        foreach (ParticleBehavior particle in _captured) {
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

    public void Enable() {
        _enabled = true;
    }

    public void Disable() {
        _enabled = false;

        _captured.Clear();
    }
}
