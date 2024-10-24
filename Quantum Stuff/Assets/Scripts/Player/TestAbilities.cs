using UnityEngine;

public class TestAbilities : MonoBehaviour {
	[SerializeField] float castWidth;
	[SerializeField] float reach;

	Transform _camera;

	void Awake() {
		_camera = Camera.main.transform;
	}

	void Update() {
		if (!Input.GetKeyDown(KeyCode.G)) return;

		//if (Physics.SphereCast(_camera.position, castWidth, _camera.forward, out RaycastHit info, reach, -1, QueryTriggerInteraction.Ignore)) {
		if (Physics.Raycast(_camera.position, _camera.forward, out RaycastHit info, reach, -1, QueryTriggerInteraction.Ignore)) {
			if (info.collider.TryGetComponent(out Excitable item)) {
				item.Excite(1);
				return;
			}
		}
    }
}
