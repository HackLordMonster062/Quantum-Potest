using UnityEngine;

public class SwitchDoor : Activatable, ISerializableElement<ActivatableData> {
    [SerializeField] int minimumEnergy = 1;
    [SerializeField] GameObject physicalDoor;

    bool _isClosed = true;

    public override void Activate(int energy) {
        if (energy < minimumEnergy) return;

        _isClosed = !_isClosed;
        physicalDoor.SetActive(_isClosed);
	}

	public ActivatableData Serialize() {
		return new ActivatableData("SignalDoor", transform.position, transform.eulerAngles, gameObject.GetEntityId(), _trigger.gameObject.GetEntityId());
	}

	public void Deserialize(ActivatableData data) {
		transform.position = data.Position;
		transform.eulerAngles = data.Rotation;
	}
}
