using UnityEngine;

public class SwitchDoor : Activatable {

    public override void Activate(int _ = 0) {
        gameObject.SetActive(false);
    }

    public override void Deactivate() {
        gameObject.SetActive(true);
    }
}
