using System;
using UnityEngine;

public class PrefabManager : Singleton<PrefabManager> {
	[SerializeField] ParticlePM particles;
	public ParticlePM Particles { get { return particles; } }
}

[Serializable]
public class ParticlePM {
	[SerializeField] GameObject photon;
	public GameObject Photon { get { return photon; } }

	[SerializeField] GameObject emitter;
	public GameObject Emitter { get { return emitter; } }
}
