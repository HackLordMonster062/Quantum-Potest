using UnityEngine;

public class PhysicsManager : Singleton<PhysicsManager> {
	[SerializeField] float gravity;

	public float Gravity { get { return gravity; } }
}
