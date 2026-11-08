using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(EntanglementController))]
[RequireComponent(typeof(RotateableBase))]
public class RotationHandler : MonoBehaviour, CommandHandler {
    EntanglementController _controller;

	RotateableBase _rotateable;

	private void Awake() {
		_controller = GetComponent<EntanglementController>();
		_rotateable = GetComponent<RotateableBase>();

		_rotateable.OnRotate += HandleRotate;
		_rotateable.OnFlipSpin += HandleFlipSpin;
	}

	public void ReceiveCommand(Command command) {
		if (command is Rotate) {
			_rotateable.Rotate();
		}
		if (command is Rotate_FlipSpin) {
			_rotateable.FlipSpin();
		}
	}

	void HandleRotate() {
		_controller.BroadcastCommand(new Rotate());
	}

	void HandleFlipSpin() {
		_controller.BroadcastCommand(new Rotate_FlipSpin());
	}
}
