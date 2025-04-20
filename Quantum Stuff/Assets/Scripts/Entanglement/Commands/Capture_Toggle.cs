using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Capture_Toggle : Command {
	public bool isCaptured;

	public Capture_Toggle(bool isCaptured) {
		this.isCaptured = isCaptured;
	}
}
