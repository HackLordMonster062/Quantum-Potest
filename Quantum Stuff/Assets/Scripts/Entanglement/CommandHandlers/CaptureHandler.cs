using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Capturable))]
public class CaptureHandler : Capturer, CommandHandler {
	Capturable _capturable;

	EntanglementController _controller;

	private void Awake() {
		_capturable = GetComponent<Capturable>();
		_controller = GetComponent<EntanglementController>();

		_capturable.OnCapture += HandleCapture;
		_capturable.OnRelease += HandleRelease;
		_capturable.OnMove += HandleMove;
	}

	public void ReceiveCommand(Command command) {
		if (command is Capture_Toggle toggle) {
			if (toggle.isCaptured)
				TryCapture(_capturable);
			else
				Release(_capturable);
		}
		if (command is Capture_TargetMove targetChange) {
			_capturable.MoveBy(targetChange.amount, targetChange.force);
		}
	}

	void HandleCapture(Capturable capturable, CapturerStrengh strength) {
		if (strength == CapturerStrengh.Player) {
			_controller.BroadcastCommand(new Capture_Toggle(false));
			return;
		}

		_controller.BroadcastCommand(new Capture_Toggle(true));

		_capturable.MoveTo(_capturable.transform.position, 1);
	}

	void HandleRelease(Capturable capturable) {
		_controller.BroadcastCommand(new Capture_Toggle(false));
	}

	void HandleMove(Vector3 target, float force) {
		_controller.BroadcastCommand(new Capture_TargetMove(target - _capturable.Target, force));
	}
}
