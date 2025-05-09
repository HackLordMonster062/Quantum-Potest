using Unity.Burst.CompilerServices;
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

				if (info.collider.TryGetComponent(out Activatable device)) {
					device.transform.Rotate(Vector3.up, 90);
					return;
				}
			}

		if (Input.GetKeyDown(KeyCode.E))
			if (Physics.Raycast(_camera.position, _camera.forward, out RaycastHit info, reach, -1, QueryTriggerInteraction.Ignore)) {
				if (info.collider.TryGetComponent(out ParticleBehavior item) || info.collider.TryGetComponent(out Activatable device)) {
					Destroy(info.collider.gameObject);
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

		if (Input.GetKeyDown(KeyCode.Keypad1))
			SpawnDevice(PrefabManager.instance.Devices.Anchor);
		if (Input.GetKeyDown(KeyCode.Keypad2))
			SpawnDevice(PrefabManager.instance.Devices.ActivatorAnchor);
		if (Input.GetKeyDown(KeyCode.Keypad3))
			SpawnDevice(PrefabManager.instance.Devices.Rotator);
		if (Input.GetKeyDown(KeyCode.Keypad4))
			SpawnDevice(PrefabManager.instance.Devices.Spinner);
		if (Input.GetKeyDown(KeyCode.Keypad5))
			SpawnDevice(PrefabManager.instance.Devices.Polaroid);
		if (Input.GetKeyDown(KeyCode.Keypad6))
			SpawnDevice(PrefabManager.instance.Devices.SignalDoor);
		if (Input.GetKeyDown(KeyCode.Keypad7))
			SpawnDevice(PrefabManager.instance.Devices.ColoredDoor);
		if (Input.GetKeyDown(KeyCode.Keypad8))
			SpawnDevice(PrefabManager.instance.Devices.PhotonShooter);
	}

	void SpawnDevice(GameObject device) {
		if (Physics.Raycast(_camera.position, _camera.forward, out RaycastHit info, reach, -1, QueryTriggerInteraction.Ignore)) {
			Instantiate(device, info.point, Quaternion.LookRotation(Vector3.ProjectOnPlane(Camera.main.transform.forward, info.normal).normalized, info.normal));
		}
	}
}
