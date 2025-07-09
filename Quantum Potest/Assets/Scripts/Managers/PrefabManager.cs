using System;
using UnityEngine;

public class PrefabManager : Singleton<PrefabManager> {
    [field: SerializeField] public ParticlePM Particles { get; private set; }
    [field: SerializeField] public DevicePM Devices { get; private set; }
}

[Serializable]
public class ParticlePM {
    [field: SerializeField] public GameObject Photon { get; private set; }
    [field: SerializeField] public GameObject Emitter { get; private set; } 
    [field: SerializeField] public GameObject Gravo { get; private set; }
    [field: SerializeField] public GameObject Spectron { get; private set; }
    [field: SerializeField] public GameObject Catalyst { get; private set; }
}

[Serializable]
public class DevicePM {
    [field: SerializeField] public GameObject Anchor { get; private set; }
    [field: SerializeField] public GameObject ActivatorAnchor { get; private set; }
    [field: SerializeField] public GameObject SuspenderAnchor { get; private set; }
    [field: SerializeField] public GameObject Rotator { get; private set; }
    [field: SerializeField] public GameObject Spinner { get; private set; }
    [field: SerializeField] public GameObject PhotonShooter { get; private set; }
    [field: SerializeField] public GameObject ColoredDoor { get; private set; }
    [field: SerializeField] public GameObject Polaroid { get; private set; }
    [field: SerializeField] public GameObject SignalDoor { get; private set; }
    [field: SerializeField] public GameObject Rail { get; private set; }
    [field: SerializeField] public GameObject RailPoint { get; private set; }
    [field: SerializeField] public GameObject SlidingWall { get; private set; }
    [field: SerializeField] public GameObject QuantumMirror { get; private set; }
}