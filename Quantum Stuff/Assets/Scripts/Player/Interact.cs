using UnityEngine;

[RequireComponent (typeof(PickUp))]
public class Interact : MonoBehaviour {
	[SerializeField] float castWidth;
	[SerializeField] float reach;

	PickUp _pickup;

	Transform _camera;

	void Awake() {
		_camera = Camera.main.transform;
		_pickup = GetComponent<PickUp>();
	}

	void Update() {
		if (!Input.GetKeyDown(KeyCode.E)) return;

		if (Physics.SphereCast(_camera.position, castWidth, _camera.forward, out RaycastHit info, reach)) {
			if (info.collider.TryGetComponent(out ILiftable item)) {
				_pickup.PickUpItem(item, info.transform);
				return;
			}

			if (info.collider.TryGetComponent(out IInteractable interactable)) {
				interactable.Interact(_pickup.Item);
				return;
			}
		}

		_pickup.Drop();
	}
}
