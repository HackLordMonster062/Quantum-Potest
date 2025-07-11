using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class SnapPotentialWell : Capturer {
	private const int MaxColliders = 32;

	[SerializeField] float captureDistance;
	[SerializeField] float snappingDistance;
	[SerializeField] float detachDistance;

	List<(Capturable, Vector3)> _offsets = new();
	bool _enabled = true;

	Collider[] _colliders;

	public Rigidbody Rb { get; private set; }

	void Start() {
		Rb = GetComponent<Rigidbody>();
		_colliders = new Collider[MaxColliders];
	}

	void FixedUpdate() {
		_offsets.RemoveAll(pair => pair.Item1 == null);

		if (!_enabled) return;

		int colliderCount = Physics.OverlapSphereNonAlloc(transform.position, captureDistance, _colliders);

		if (colliderCount == MaxColliders) {
			Debug.LogWarning("SnapPotentialWell: Too many colliders in range, some may not be captured.");
		}

		for (int i = 0; i < colliderCount; i++) {
			if (_colliders[i].gameObject != gameObject && 
				!_offsets.Any(pair => pair.Item1 != null && pair.Item1.gameObject == _colliders[i].gameObject) && 
				_colliders[i].TryGetComponent(out Capturable capturable) &&
				TryCapture(capturable)) {

				Vector3 offset = SnapToNearestAxis((_colliders[i].transform.position - transform.position).normalized) * snappingDistance;

				_offsets.Add((capturable, offset));
			}
		}

		for (int i = _offsets.Count - 1; i >= 0; i--) {
			var (capturable, offset) = _offsets[i];

			Vector3 distance = capturable.MoveTo(transform.position + offset, PhysicsManager.instance.PullingForce);

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

	Vector3 SnapToNearestAxis(Vector3 dir) {
		dir.Normalize();
		Vector3 best = Vector3.zero;
		float bestDot = -Mathf.Infinity;

		for (int x = -1; x <= 1; x++)
			for (int y = -1; y <= 1; y++)
				for (int z = -1; z <= 1; z++) {
					Vector3 candidate = new(x, y, z);
					if (candidate == Vector3.zero) continue;

					candidate.Normalize();
					float dot = Vector3.Dot(dir, candidate);
					if (dot > bestDot) {
						bestDot = dot;
						best = candidate;
					}
				}

		return best;
	}
}
