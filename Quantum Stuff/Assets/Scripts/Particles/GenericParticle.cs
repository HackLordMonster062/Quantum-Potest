using UnityEngine;

public class GenericParticle : ChargedParticle, ILiftable {

	Collider _collider;

	private void Awake() {
		_collider = GetComponent<Collider>();
	}

	public void PickUp() {
		Rb.isKinematic = true;
		_collider.enabled = false;
	}

	public void Drop() {
		Rb.isKinematic = false;
		_collider.enabled = true;
	}
}
