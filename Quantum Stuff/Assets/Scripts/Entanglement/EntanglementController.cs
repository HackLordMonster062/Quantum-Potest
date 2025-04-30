using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntanglementController : MonoBehaviour {
    [SerializeField] bool isControl;
    [SerializeField] List<EntanglementController> entangledWith;

    CommandHandler[] handlers;

    void Awake() {
        handlers = GetComponents<CommandHandler>();
    }

    public void BroadcastCommand(Command command) {
        if (!isControl) return;

        foreach (var particle in entangledWith) {
            particle.ReceiveCommand(command);
        }
    }

    public void ReceiveCommand(Command command) {
        foreach (var handler in handlers) {
            handler.ReceiveCommand(command);
        }
    }
}
