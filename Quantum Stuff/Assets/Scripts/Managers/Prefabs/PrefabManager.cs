using UnityEngine;

public class PrefabManager : Singleton<PrefabManager> {
	[SerializeField] ParticlePM particles;
	public ParticlePM Particles { get { return particles; } }
}
