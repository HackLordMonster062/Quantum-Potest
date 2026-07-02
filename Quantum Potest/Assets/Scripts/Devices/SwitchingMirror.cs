using UnityEngine;

public class SwitchingMirror : MonoBehaviour, ISerializableElement<DeviceData> {
    [SerializeField] Collider surface;

	void OnTriggerEnter(Collider other) {
		if (other.CompareTag("Photon")) {
			surface.enabled = !surface.enabled;
		}
	}

	public DeviceData Serialize() {
		return new DeviceData("QuantumMirror", transform.position, transform.eulerAngles, gameObject.GetEntityId());
	}

	public void Deserialize(DeviceData data) {
		transform.position = data.Position;
		transform.eulerAngles = data.Rotation;
	}
}
