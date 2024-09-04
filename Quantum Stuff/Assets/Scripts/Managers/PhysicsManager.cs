using UnityEngine;

public class PhysicsManager : Singleton<PhysicsManager> {
	[SerializeField] float gravity;
	[SerializeField] float decayTime;

	public float Gravity { get { return gravity; } }
	public float DecayTime { get { return decayTime; } }
}
