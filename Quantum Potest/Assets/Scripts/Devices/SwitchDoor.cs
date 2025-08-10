using UnityEngine;

public class SwitchDoor : Activatable {
    [SerializeField] int minimumEnergy = 1;
    [SerializeField] GameObject physicalDoor;

    bool _isClosed = true;

    public override void Activate(int energy) {
        if (energy < minimumEnergy) return;

        _isClosed = !_isClosed;
        physicalDoor.SetActive(_isClosed);
    }
}
