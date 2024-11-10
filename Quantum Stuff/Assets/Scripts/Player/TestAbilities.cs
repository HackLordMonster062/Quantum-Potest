using UnityEngine;

public class TestAbilities : MonoBehaviour {
	[SerializeField] float castWidth;
	[SerializeField] float reach;

	Transform _camera;

	void Awake() {
		_camera = Camera.main.transform;
	}

	void Update() {
		if (Input.GetKeyDown(KeyCode.G))
			if (Physics.Raycast(_camera.position, _camera.forward, out RaycastHit info, reach, -1, QueryTriggerInteraction.Ignore)) {
				if (info.collider.TryGetComponent(out Excitable item)) {
					item.Excite(1);
					return;
				}
			}

		if (Input.GetKeyDown(KeyCode.Q))
			if (Physics.Raycast(_camera.position, _camera.forward, out RaycastHit info, reach, -1, QueryTriggerInteraction.Ignore)) {
				if (info.collider.TryGetComponent(out Rotateable item)) {
					item.Rotate();
					return;
				}
			}

		if (Input.GetKeyDown(KeyCode.E))
			if (Physics.Raycast(_camera.position, _camera.forward, out RaycastHit info, reach, -1, QueryTriggerInteraction.Ignore)) {
				if (info.collider.TryGetComponent(out ParticleBehavior item)) {
					Destroy(item.gameObject);
					return;
				}
			}

		if (Input.GetKeyDown(KeyCode.Alpha1))
			Instantiate(PrefabManager.instance.Particles.Emitter, _camera.position + reach * _camera.forward, Quaternion.identity);
		if (Input.GetKeyDown(KeyCode.Alpha2))
			Instantiate(PrefabManager.instance.Particles.ColoredKey, _camera.position + reach * _camera.forward, Quaternion.identity);
		if (Input.GetKeyDown(KeyCode.Alpha3))
			Instantiate(PrefabManager.instance.Particles.MassiveParticle, _camera.position + reach * _camera.forward, Quaternion.identity);
		if (Input.GetKeyDown(KeyCode.Alpha4))
			Instantiate(PrefabManager.instance.Particles.ActivatorParticle, _camera.position + reach * _camera.forward, Quaternion.identity);
	}
}
