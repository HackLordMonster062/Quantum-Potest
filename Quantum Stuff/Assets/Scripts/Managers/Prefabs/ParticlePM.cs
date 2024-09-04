using System;
using UnityEngine;

[Serializable]
public class ParticlePM {
	[SerializeField] GameObject photon;
	public GameObject Photon { get { return photon; } }

	[SerializeField] GameObject emitter;
	public GameObject Emitter { get { return emitter; } }
}
