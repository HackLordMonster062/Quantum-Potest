using UnityEngine;

public class PhysicsManager : Singleton<PhysicsManager> {
	[field: SerializeField] public float Gravity { get; private set; }
	[field: SerializeField] public float RelaxtationTime { get; private set; }
	[field: SerializeField] public float PullingForce { get; private set; }
}
