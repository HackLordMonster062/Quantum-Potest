using UnityEngine;

public class SwitchDoor : Activatable, ISerializableElement {
    [SerializeField] int minimumEnergy = 1;
    [SerializeField] GameObject physicalDoor;

    bool _isClosed = true;

    public override void Activate(int energy) {
        if (energy < minimumEnergy) return;

        _isClosed = !_isClosed;
        physicalDoor.SetActive(_isClosed);
	}

	public ElementData Serialize() {
		return new ActivatableData("SignalDoor", transform.localPosition, transform.eulerAngles, transform.localScale, gameObject.GetEntityId().ToString(), _trigger == null ? "" : _trigger.gameObject.GetEntityId().ToString());
	}

	public void Deserialize(ElementData data) {
		transform.localPosition = data.Position;
		transform.eulerAngles = data.Rotation;
		transform.localScale = data.Scale;
	}
}
