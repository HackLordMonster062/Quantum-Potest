using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public record Capture_TargetMove : Command {
	public Vector3 amount;
	public float force;

	public Capture_TargetMove(Vector3 newPosition, float force) {
		this.amount = newPosition;
		this.force = force;
	}
}
