using UnityEngine;

public class PhysicsManager : Singleton<PhysicsManager> {
	[SerializeField] float gravity;
	[SerializeField] float relaxationTime;
	[SerializeField] LayerMask triggerIgnoreMask;

	public float Gravity { get { return gravity; } }
	public float RelaxtationTime { get { return relaxationTime; } }
}
