using System;
using UnityEngine;

public class PrefabManager : Singleton<PrefabManager> {
	[SerializeField] ParticlePM particles;
	public ParticlePM Particles { get { return particles; } }

	[SerializeField] DevicePM devices;
	public DevicePM Devices { get { return devices; } }
}

[Serializable]
public class ParticlePM {
	[SerializeField] GameObject photon;
	public GameObject Photon { get { return photon; } }

	[SerializeField] GameObject emitter;
	public GameObject Emitter { get { return emitter; } }

	[SerializeField] GameObject massiveParticle;
	public GameObject MassiveParticle { get { return massiveParticle; } }

	[SerializeField] GameObject coloredKey;
	public GameObject ColoredKey { get { return coloredKey; } }

	[SerializeField] GameObject activatorParticle;
	public GameObject ActivatorParticle { get { return activatorParticle; } }
}

[Serializable]
public class DevicePM {
	[SerializeField] GameObject anchor;
	public GameObject Anchor { get { return anchor; } }

	[SerializeField] GameObject activatorAnchor;
	public GameObject ActivatorAnchor { get { return activatorAnchor; } }

	[SerializeField] GameObject rotator;
	public GameObject Rotator { get { return rotator; } }

	[SerializeField] GameObject spinner;
	public GameObject Spinner { get { return spinner; } }

	[SerializeField] GameObject photonShooter;
	public GameObject PhotonShooter { get { return photonShooter; } }

	[SerializeField] GameObject coloredDoor;
	public GameObject ColoredDoor { get { return coloredDoor; } }

	[SerializeField] GameObject polaroid;
	public GameObject Polaroid { get { return polaroid; } }

	[SerializeField] GameObject signalDoor;
	public GameObject SignalDoor { get { return signalDoor; } }
}