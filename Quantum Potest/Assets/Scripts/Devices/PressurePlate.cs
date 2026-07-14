using UnityEngine;

public class PressurePlate : Trigger, ISerializableElement {
	int inside = 0;

	private void OnTriggerEnter(Collider other) {
		inside++;
		if (inside == 1)
			Activate(1);
	}

	private void OnTriggerExit(Collider other) {
		inside--;
		if (inside == 0) 
			Deactivate();
	}

	public ElementData Serialize() {
		return new DeviceData("Polaroid", transform.localPosition, transform.eulerAngles, transform.localScale, gameObject.GetEntityId().ToString());
	}

	public void Deserialize(ElementData data) {
		transform.localPosition = data.Position;
		transform.eulerAngles = data.Rotation;
		transform.localScale = data.Scale;
	}
}
