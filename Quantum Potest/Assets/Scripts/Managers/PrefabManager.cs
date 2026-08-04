using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

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
    [field: SerializeField] public GameObject QuantumMirror { get; private set; }
}

[Serializable]
public class SurfacePM {
    [field: SerializeField] public GameObject Wall { get; private set; }
    [field: SerializeField] public GameObject ReflectiveWall { get; private set; }
    [field: SerializeField] public GameObject TunnellableWall { get; private set; }
	[field: SerializeField] public GameObject SlidingWall { get; private set; }
}

public class PrefabManager : Singleton<PrefabManager> {
    [field: SerializeField] public GameObject Level { get; private set; }
    [field: SerializeField] public ParticlePM Particles { get; private set; }
    [field: SerializeField] public DevicePM Devices { get; private set; }
    [field: SerializeField] public SurfacePM Surfaces { get; private set; }

    Dictionary<string, GameObject> particleMap;
    Dictionary<string, GameObject> deviceMap;
    Dictionary<string, GameObject> surfaceMap;

    protected override void Awake() {
        base.Awake();
        BuildMaps();
    }

    void BuildMaps() {
        particleMap = BuildMapFromContainer(Particles);
        deviceMap = BuildMapFromContainer(Devices);
        surfaceMap = BuildMapFromContainer(Surfaces);
    }

    static Dictionary<string, GameObject> BuildMapFromContainer(object container) {
        var map = new Dictionary<string, GameObject>(StringComparer.OrdinalIgnoreCase);
        if (container == null) return map;

        var type = container.GetType();

        foreach (var prop in type.GetProperties(BindingFlags.Instance | BindingFlags.Public)) {
            if (prop.PropertyType != typeof(GameObject)) continue;
            var getter = prop.GetGetMethod(nonPublic: false);
            if (getter == null) continue;
            var value = getter.Invoke(container, null) as GameObject;
            if (value == null) continue;

            var keyAttr = prop.GetCustomAttribute<PrefabKeyAttribute>();
            var key = keyAttr != null && !string.IsNullOrEmpty(keyAttr.Key) ? keyAttr.Key : prop.Name;
            if (!map.ContainsKey(key)) map[key] = value;
        }

        foreach (var field in type.GetFields(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public)) {
            if (field.FieldType != typeof(GameObject)) continue;
            var value = field.GetValue(container) as GameObject;
            if (value == null) continue;

            var keyAttr = field.GetCustomAttribute<PrefabKeyAttribute>();
            var key = keyAttr != null && !string.IsNullOrEmpty(keyAttr.Key) ? keyAttr.Key : field.Name;
            if (!map.ContainsKey(key)) map[key] = value;
        }

        return map;
    }

    public GameObject GetParticle(string id) {
        if (string.IsNullOrEmpty(id)) return null;
        particleMap.TryGetValue(id, out var go);
        return go;
    }

    public bool TryGetParticle(string id, out GameObject prefab) {
		prefab = null;
		return particleMap != null && particleMap.TryGetValue(id, out prefab);
	}

	public GameObject GetDevice(string id) {
        if (string.IsNullOrEmpty(id)) return null;
        deviceMap.TryGetValue(id, out var go);
        return go;
    }

    public bool TryGetDevice(string id, out GameObject prefab) {
		prefab = null;
		return deviceMap != null && deviceMap.TryGetValue(id, out prefab);
	}

	public GameObject GetSurface(string id) {
        if (string.IsNullOrEmpty(id)) return null;
        surfaceMap.TryGetValue(id, out var go);
        return go;
    }

    public bool TryGetSurface(string id, out GameObject prefab) {
		prefab = null;
		return surfaceMap != null && surfaceMap.TryGetValue(id, out prefab);
	}
}

[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
public class PrefabKeyAttribute : Attribute {
    public string Key { get; }
    public PrefabKeyAttribute(string key) => Key = key;
}