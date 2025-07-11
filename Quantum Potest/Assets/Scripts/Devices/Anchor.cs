using System.Collections.Generic;
using UnityEngine;

public class Anchor : Capturer {
    [SerializeField] float holdingHeight;

    public Capturable Particle { get; private set; }
	public bool IsActive { get; set; } = true;

	List<Capturable> _inTrigger;

	private void Awake() {
        _inTrigger = new();
	}

	void FixedUpdate() {
        if (Particle == null) return;

        Vector3 target = transform.position + transform.up * holdingHeight;

        Particle.MoveTo(target, PhysicsManager.instance.PullingForce);
    }

	private void OnTriggerEnter(Collider other) {
		_inTrigger.RemoveAll(c => c == null || c.gameObject == null);
		if (Particle != null && Particle.gameObject == null)
			Particle = null;

		if (Particle == null) {
            Capturable particle = other.GetComponent<Capturable>();

            if (particle != null) {
                _inTrigger.Add(particle);
            }
        }
	}

	private void OnTriggerStay(Collider other) {
		if (!IsActive) return;

		foreach (Capturable capturable in _inTrigger) {
            if (capturable.gameObject == other.gameObject && TryCapture(capturable))
				Particle = capturable;
		}
	}

	private void OnTriggerExit(Collider other) {
		_inTrigger.RemoveAll(capturable => capturable.gameObject == other.gameObject);
	}

	protected override void Release(Capturable capturable) {
		base.Release(capturable);
		Particle = null;
	}

	public void ForceRelease() {
		if (Particle == null) return;
		Release(Particle);
	}

	protected override void Give(Capturable capturable, CapturerStrengh strength) {
		base.Give(capturable, strength);
		Particle = null;
	}
}
