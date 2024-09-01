using UnityEngine;

public class SwitchDoor : MonoBehaviour {
    [SerializeField] PressurePlate trigger;

    void Start() {
        trigger.OnTrigger += Open;
        trigger.OnUntrigger += Close;
    }

    void Close() {
        gameObject.SetActive(true);
    }

    void Open() {
        gameObject.SetActive(false);
    }
}
