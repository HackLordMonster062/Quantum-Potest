using UnityEngine;

public class SwitchDoor : Activatable {
    [SerializeField] int minimumEnergy = 1;

    public override void Activate(int energy) {
        if (energy < minimumEnergy) return;

        gameObject.SetActive(false);
    }

    public override void Deactivate() {
        gameObject.SetActive(true);
    }
}
