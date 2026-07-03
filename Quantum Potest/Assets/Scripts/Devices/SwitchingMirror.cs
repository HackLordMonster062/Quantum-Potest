using UnityEngine;

public class SwitchingMirror : MonoBehaviour, ISerializableElement {
    [SerializeField] Collider surface;

	void OnTriggerEnter(Collider other) {
		if (other.CompareTag("Photon")) {
			surface.enabled = !surface.enabled;
		}
	}

	public ElementData Serialize() {
		return new DeviceData("QuantumMirror", transform.position, transform.eulerAngles, gameObject.GetEntityId().ToString());
	}

	public void Deserialize(ElementData data) {
		transform.position = data.Position;
		transform.eulerAngles = data.Rotation;
	}
}
