using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TestAbilities : MonoBehaviour {
#if UNITY_EDITOR
	[SerializeField] float castWidth;
	[SerializeField] float reach;
	[SerializeField] float scalingFactor;
	[SerializeField] LayerMask selection;

	[SerializeField] Selection _selection;
	[SerializeField] Selection _prevSelection;

	Transform _camera;

	// Rail
	Transform _railDevice;
	[SerializeField] List<Transform> _railPath;

	// Frequency Selection
	bool frequencyMode = false;
	List<int> selectedFrequencies = new();
	int selectedFrequency = -1;

	void Awake() {
		_camera = Camera.main.transform;
	}

	void Update() {
		if (Input.GetKeyDown(KeyCode.H)) {
			frequencyMode = !frequencyMode;
			Debug.Log(frequencyMode ? "Entering Frequency Selection Mode" : "Exiting Frequency Selection Mode");
		}

		if (frequencyMode) {
			for (int i = 1; i <= 6; i++) {
				if (Input.GetKeyDown(i.ToString())) {
					if (selectedFrequencies.Contains(i)) selectedFrequencies.Remove(i);
					else selectedFrequencies.Add(i);

					selectedFrequency = i;

					Debug.Log($"Frequencies: [{string.Join(", ", selectedFrequencies)}]");
				}
			}
			
			if (Input.GetKeyDown(KeyCode.Escape)) {
				selectedFrequencies.Clear();
			}

			return;
		}

		if (Input.GetKeyDown(KeyCode.G))
			if (Physics.Raycast(_camera.position, _camera.forward, out RaycastHit info, reach, ~selection, QueryTriggerInteraction.Ignore)) {
				if (info.collider.TryGetComponent(out Excitable item)) {
					item.Excite(1);
					return;
				}
			}

		if (Input.GetKeyDown(KeyCode.R)) {
			if (Physics.Raycast(_camera.position, _camera.forward, out RaycastHit info, reach, ~selection, QueryTriggerInteraction.Ignore)) {
				if (info.collider.TryGetComponent(out Rotateable item)) {
					item.Rotate();
					return;
				}
			}

			if (_selection != null && _selection.device != null) {
				_selection.device.transform.Rotate(Vector3.up, Input.GetKey(KeyCode.LeftShift) ? 45 : 90);
				return;
			}
		}

		if (Input.GetKeyDown(KeyCode.KeypadPlus)) {
			if (_selection != null && _selection.device != null) {
				ChangeSize(Camera.main.transform.forward, scalingFactor);
				return;
			}
		}

		if (Input.GetKeyDown(KeyCode.KeypadMinus)) {
			if (_selection != null && _selection.device != null) {
				ChangeSize(Camera.main.transform.forward, -scalingFactor);
				return;
			}
		}

		if (Input.GetKeyDown(KeyCode.Q)) {
			if (Physics.Raycast(_camera.position, _camera.forward, out RaycastHit info, reach, ~selection, QueryTriggerInteraction.Ignore)) {
				if (info.collider.TryGetComponent(out ParticleBehavior item)) {
					Destroy(info.collider.gameObject);
					return;
				}
			}

			if (_selection != null && _selection.device != null) {
				Destroy(_selection.device);
				_selection = null;
				return;
			}
		}

		if (Input.GetKeyDown(KeyCode.C)) {
			if (_selection != null && _prevSelection != null && _selection.device.TryGetComponent(out Activatable device) && _prevSelection.device.TryGetComponent(out Trigger trigger)) {
				device.SetTrigger(trigger);
				print("Connected " + device.name + " to " + trigger.name);
			}
		}

		if (Input.GetKeyDown(KeyCode.F))
			if (Physics.Raycast(_camera.position, _camera.forward, out RaycastHit info, reach, selection, QueryTriggerInteraction.Ignore)) {
				if (info.collider.TryGetComponent(out Selection device)) {
					if (_selection != null) {
						_selection.isSelected = false;
						_prevSelection = _selection;
					}

					_selection = device;
					_selection.isSelected = true;
					return;
				}
			} else if (_selection != null) {
				_selection.isSelected = false;
				_selection = null;
			}

		if (Input.GetKeyDown(KeyCode.Alpha1))
			Instantiate(PrefabManager.instance.Particles.Emitter, _camera.position + reach * _camera.forward, Quaternion.identity, LevelDataManager.instance.CurrentLevel.transform);
		if (Input.GetKeyDown(KeyCode.Alpha2) && selectedFrequencies.Count > 0) {
			Spectron spectron = Instantiate(PrefabManager.instance.Particles.Spectron, _camera.position + reach * _camera.forward, Quaternion.identity, LevelDataManager.instance.CurrentLevel.transform).GetComponent<Spectron>();

			spectron.SetFrequencies(selectedFrequencies.ToList());
		}
		if (Input.GetKeyDown(KeyCode.Alpha3))
			Instantiate(PrefabManager.instance.Particles.Gravo, _camera.position + reach * _camera.forward, Quaternion.identity, LevelDataManager.instance.CurrentLevel.transform);
		if (Input.GetKeyDown(KeyCode.Alpha4))
			Instantiate(PrefabManager.instance.Particles.Catalyst, _camera.position + reach * _camera.forward, Quaternion.identity, LevelDataManager.instance.CurrentLevel.transform);

		if (Input.GetKeyDown(KeyCode.Keypad0)) {
			if (Input.GetKey(KeyCode.LeftShift)) {
				GameObject rail = SpawnDevice(PrefabManager.instance.Devices.Rail, false);

				if (rail != null) {
					Rail railComp = rail.GetComponent<Rail>();
					FinalizeRail(railComp);
				}
			} else if (Input.GetKey(KeyCode.LeftAlt)) {
				GameObject wall = SpawnDevice(PrefabManager.instance.Devices.SlidingWall, false);

				if (wall != null) {
					CapturableBlock wallComp = wall.GetComponent<CapturableBlock>();
					FinalizeSlidingWall(wallComp);
				}
			} else if (Input.GetKey(KeyCode.LeftControl)) {
				CancelRail();
			} else {
				AddPointToRail(transform.position);
			}
		}

		if (Input.GetKeyDown(KeyCode.Keypad1))
			SpawnDevice(PrefabManager.instance.Devices.Anchor);
		if (Input.GetKeyDown(KeyCode.Keypad2))
			SpawnDevice(PrefabManager.instance.Devices.ActivatorAnchor);
		if (Input.GetKeyDown(KeyCode.Keypad3))
			SpawnDevice(PrefabManager.instance.Devices.SuspenderAnchor);
		if (Input.GetKeyDown(KeyCode.Keypad4))
			SpawnDevice(PrefabManager.instance.Devices.Rotator);
		if (Input.GetKeyDown(KeyCode.Keypad5))
			SpawnDevice(PrefabManager.instance.Devices.Spinner);
		if (Input.GetKeyDown(KeyCode.Keypad6))
			SpawnDevice(PrefabManager.instance.Devices.Polaroid);
		if (Input.GetKeyDown(KeyCode.Keypad7))
			SpawnDevice(PrefabManager.instance.Devices.SignalDoor);
		if (Input.GetKeyDown(KeyCode.Keypad8))
			SpawnDevice(PrefabManager.instance.Devices.ColoredDoor);
		if (Input.GetKeyDown(KeyCode.Keypad9))
			SpawnDevice(PrefabManager.instance.Devices.PhotonShooter, false);
		if (Input.GetKeyDown(KeyCode.KeypadMultiply))
			SpawnDevice(PrefabManager.instance.Devices.QuantumMirror);
		if (Input.GetKeyDown(KeyCode.KeypadDivide)) {
			if (Input.GetKey(KeyCode.LeftShift))
				SpawnDevice(PrefabManager.instance.Surfaces.ReflectiveWall, false);
			else
				SpawnDevice(PrefabManager.instance.Surfaces.Wall, false);
		}
	}

	GameObject SpawnDevice(GameObject device, bool mountable = true) {
		if (Physics.Raycast(_camera.position, _camera.forward, out RaycastHit info, reach, ~selection, QueryTriggerInteraction.Ignore)) {
			GameObject dev = Instantiate(device, info.point, Quaternion.LookRotation(SnapToNearestAxis(Vector3.ProjectOnPlane(Camera.main.transform.forward, info.normal)).normalized, info.normal), LevelDataManager.instance.CurrentLevel.transform);

			if (mountable && Input.GetKey(KeyCode.LeftShift)) {
				StartRail(dev.transform);
			}

			if (dev.TryGetComponent(out FrequencyDoor door)) {
				door.SetFrequency(selectedFrequency);
			}

			return dev;
		}

		return null;
	}

	void StartRail(Transform device) {
		_railDevice = device;
		_railPath = new();
	}

	void CancelRail() {
		Destroy(_railDevice.gameObject);
		_railDevice = null;

		foreach (Transform t in _railPath) {
			Destroy(t.gameObject);
		}

		_railPath = null;
	}

	void AddPointToRail(Vector3 point) {
		if (_railPath == null) return;

		GameObject pointObj = SpawnDevice(PrefabManager.instance.Devices.RailPoint, false);
		_railPath.Add(pointObj.transform);
	}

	void FinalizeRail(Rail rail) {
		if (_railDevice == null) return;

		rail.Initialize(_railPath.ToArray(), _railDevice); 

		_railDevice = null;
		_railPath = new();
	}

	void FinalizeSlidingWall(CapturableBlock wall) {
		if (_railPath == null) return;

		if (_railPath.Count < 2) {
			print("Requires 2 points");
			return;
		}

		wall.Initialize(_railPath[0], _railPath[1], true);

		_railDevice = null;
		_railPath = new();
	}

	Vector3 SnapToNearestAxis(Vector3 dir) {
		dir.Normalize();
		Vector3 best = Vector3.zero;
		float bestDot = -Mathf.Infinity;

		for (int x = -1; x <= 1; x++)
			for (int y = -1; y <= 1; y++)
				for (int z = -1; z <= 1; z++) {
					Vector3 candidate = new Vector3(x, y, z);
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

	void ChangeSize(Vector3 direction, float amount) {
		Vector3 snapped = SnapToNearestAxis(direction);

		_selection.device.transform.position += .5f * amount * snapped;

		Vector3 localDir = SnapToNearestAxis(_selection.device.transform.InverseTransformDirection(direction));
		_selection.device.transform.localScale += amount * MaskNonZero(localDir);
	}

	Vector3 MaskNonZero(Vector3 input, float epsilon = 0.0001f) {
		return new Vector3(
			Mathf.Abs(input.x) > epsilon ? 1 : 0,
			Mathf.Abs(input.y) > epsilon ? 1 : 0,
			Mathf.Abs(input.z) > epsilon ? 1 : 0
		);
	}
#endif
}
