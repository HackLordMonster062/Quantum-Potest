using UnityEngine;

public class SwitchingMirror : MonoBehaviour, ISerializableElement {
    [SerializeField] Collider surface;

	void OnTriggerEnter(Collider other) {
		if (other.CompareTag("Photon")) {
			surface.enabled = !surface.enabled;
		}
	}

	public ElementData Serialize() {
		return new DeviceData("QuantumMirror", transform.localPosition, transform.eulerAngles, transform.localScale, gameObject.GetEntityId().ToString());
	}

	public void Deserialize(ElementData data) {
		transform.localPosition = data.Position;
		transform.eulerAngles = data.Rotation;
		transform.localScale = data.Scale;
	}
}
