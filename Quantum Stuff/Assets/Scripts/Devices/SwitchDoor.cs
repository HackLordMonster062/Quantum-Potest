using UnityEngine;

public class SwitchDoor : Activatable {

    public override void Activate() {
        gameObject.SetActive(false);
    }

    protected override void Deactivate() {
        gameObject.SetActive(true);
    }
}
