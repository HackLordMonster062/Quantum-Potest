using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class SnapPotentialWell : Capturer {
	[SerializeField] float captureDistance;
	[SerializeField] float snappingDistance;
	[SerializeField] float detachDistance;
	[SerializeField] float pullingForce;

	List<(Capturable, Vector3)> _offsets = new();
	bool _enabled = true;

	Collider[] _ownColliders;

	public Rigidbody Rb { get; private set; }

	void Start() {
		Rb = GetComponent<Rigidbody>(); 
		_ownColliders = GetComponents<Collider>();
	}

	void FixedUpdate() {
		if (!_enabled) return;

		Collider[] colliders = Physics.OverlapSphere(transform.position, captureDistance);

		foreach (Collider collider in colliders) {
			if (collider.gameObject != gameObject && 
				!_offsets.Any(pair => pair.Item1.gameObject == collider.gameObject) && 
				collider.TryGetComponent(out Capturable capturable) &&
				TryCapture(capturable)) {

				Vector3 offset = (collider.transform.position - transform.position).normalized * snappingDistance;

				_offsets.Add((capturable, offset));
			}
		}

		foreach (var (capturable, offset) in _offsets.ToArray()) {
			Vector3 distance = capturable.MoveTo(transform.position + offset, pullingForce);

			if (distance.sqrMagnitude > detachDistance * detachDistance)
				Release(capturable);
		}
	}

	public void Enable() {
		_enabled = true;
	}

	public void Disable() {
		_enabled = false;

		foreach (var (capturable, _) in _offsets.ToArray()) {
			Release(capturable);
		}
	}

	protected override void Release(Capturable capturable) {
		base.Release(capturable);

		_offsets.RemoveAll(pair => pair.Item1 == capturable);
	}

	protected override void Give(Capturable capturable, CapturerStrengh strength) {
		base.Give(capturable, strength);

		_offsets.RemoveAll(pair => pair.Item1 == capturable);
	}
}
