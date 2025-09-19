using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class SnapPotentialWell : Capturer {
	private const int MaxColliders = 32;
	private const int CardinalDirections = 3 * 3 * 3 - 1; // 3D directions excluding zero vector

	private static Vector3[] snapDirections;

	[SerializeField] float captureDistance;
	[SerializeField] float snappingDistance;
	[SerializeField] float detachDistance;

	Capturable[] _slots;
	bool _enabled = true;

	Collider[] _colliders;

	GravoView _gravoView;

	public Rigidbody Rb { get; private set; }

	void Start() {
		Rb = GetComponent<Rigidbody>();
		_gravoView = GetComponentInParent<GravoView>();
		_colliders = new Collider[MaxColliders];

		PopulateDirections();

		_slots = new Capturable[CardinalDirections];

		Enable();
	}

	void FixedUpdate() {
		if (!_enabled) return;

		int colliderCount = Physics.OverlapSphereNonAlloc(transform.position, captureDistance, _colliders);

		if (colliderCount == MaxColliders) {
			Debug.LogWarning("SnapPotentialWell: Too many colliders in range, some may not be captured.");
		}

		for (int i = 0; i < colliderCount; i++) {
			int offset = GetNearsetSnapAxis(_colliders[i].transform.position - transform.position);

			if (_colliders[i].gameObject != gameObject &&
				!_slots.Any(capturable => capturable != null && capturable.gameObject == _colliders[i].gameObject) &&
				_slots[offset] == null &&
				_colliders[i].TryGetComponent(out Capturable capturable) &&
				TryCapture(capturable)) {

				_slots[offset] = capturable;
			}
		}

		for (int i = 0; i < _slots.Length; i++) {
			if (_slots[i] == null) continue;

			Vector3 distance = _slots[i].MoveTo(transform.position + snapDirections[i] * captureDistance, PhysicsManager.instance.PullingForce);

			if (distance.sqrMagnitude > detachDistance * detachDistance)
				Release(_slots[i]);
		}

		if (_gravoView != null) {
			_gravoView.slots = _slots.Select((c, index) => c != null ? new GravoView.SlotData { position = c.transform.position - transform.position, isTaken = 1 } : new GravoView.SlotData { position = snapDirections[index] * snappingDistance, isTaken = 0 }).ToArray();
		}
	}

	public void Enable() {
		_enabled = true;
		_gravoView.SetEnabled(true);
	}

	public void Disable() {
		_enabled = false;
		_gravoView.SetEnabled(false);

		foreach (var capturable in _slots) {
			Release(capturable);
		}
	}

	protected override void Release(Capturable capturable) {
		if (capturable == null) return;

		base.Release(capturable);

		ReleaseCapturable(capturable);
	}

	protected override void Give(Capturable capturable, CapturerStrengh strength) {
		base.Give(capturable, strength);

		ReleaseCapturable(capturable);
	}

	private void ReleaseCapturable(Capturable capturable) {
		if (_slots == null) return;

		for (int i = 0; i < _slots.Length; i++) {
			if (_slots[i] == capturable)
				_slots[i] = null;
		}
	}

	int GetNearsetSnapAxis(Vector3 dir) {
		dir.Normalize();
		int best = -1;
		float bestDot = -Mathf.Infinity;

		int index = 0;

		for (int x = -1; x <= 1; x++)
			for (int y = -1; y <= 1; y++)
				for (int z = -1; z <= 1; z++) {
					Vector3 candidate = new(x, y, z);
					if (candidate == Vector3.zero) continue;

					candidate.Normalize();
					float dot = Vector3.Dot(dir, candidate);
					if (dot > bestDot) {
						bestDot = dot;
						best = index;
					}

					index++;
				}

		return best;
	}

	static void PopulateDirections() {
		if (snapDirections != null) return;

		snapDirections = new Vector3[CardinalDirections];

		int index = 0;

		for (int x = -1; x <= 1; x++)
			for (int y = -1; y <= 1; y++)
				for (int z = -1; z <= 1; z++) {
					Vector3 candidate = new(x, y, z);
					if (candidate == Vector3.zero) continue;

					snapDirections[index++] = candidate.normalized;
				}
	}
}
