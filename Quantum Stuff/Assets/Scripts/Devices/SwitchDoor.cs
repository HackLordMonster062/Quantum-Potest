using UnityEngine;

public class SwitchDoor : Activatable {

    protected override void Activate() {
        gameObject.SetActive(false);
    }

    protected override void Deactivate() {
        gameObject.SetActive(true);
    }
}
