using UnityEngine;

public class GenericParticle : ChargedParticle {

	Collider _collider;

	private void Awake() {
		_collider = GetComponent<Collider>();
	}
}
