using UnityEngine;

public class PressurePlate : Trigger, ISerializableElement<DeviceData> {
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

	public DeviceData Serialize() {
		return new DeviceData("Polaroid", transform.position, transform.eulerAngles, gameObject.GetEntityId());
	}

	public void Deserialize(DeviceData data) {
		transform.position = data.Position;
		transform.eulerAngles = data.Rotation;
	}
}
