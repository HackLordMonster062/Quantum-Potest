using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface CommandHandler {
	void ReceiveCommand(Command command);
}
