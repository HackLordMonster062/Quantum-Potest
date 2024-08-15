using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class ColorParticle : MonoBehaviour, ILiftable {
	[SerializeField] int frequency;
	public int Frequency { get { return frequency; } }

	Rigidbody _rb;
	Collider _collider;
	MeshRenderer _renderer;

	private void Awake() {
		_rb = GetComponent<Rigidbody>();
		_collider = GetComponent<Collider>();

		_renderer = GetComponent<MeshRenderer>();

		_renderer.material.color = Color.HSVToRGB(1f / frequency, 1, 1);
	}

	public void PickUp() {
		_rb.isKinematic = true;
		_collider.enabled = false;
	}

	public void Drop() {
		_rb.isKinematic = false;
		_collider.enabled = true;
	}
}
