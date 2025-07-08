using UnityEngine;

public class PhysicsManager : Singleton<PhysicsManager> {
	[SerializeField] float gravity;
	[SerializeField] float relaxationTime;

	public float Gravity { get { return gravity; } }
	public float RelaxtationTime { get { return relaxationTime; } }
}
