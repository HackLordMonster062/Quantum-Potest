using System.Collections.Generic;

public class EntanglementManager : Singleton<EntanglementManager> {
    Dictionary<int, List<Excitable>> _groups;
    Dictionary<Excitable, int> _particleIDs;

    int _currID = 0;

    protected override void Awake() {
        base.Awake();

        _groups = new();
        _particleIDs = new();
    }

    public void RegisterPair(Excitable particle) {
        int id;

        if (!_particleIDs.TryGetValue(particle, out id))
            id = _currID++;

        if (!_groups.ContainsKey(id))
            _groups[id] = new List<Excitable>();

        _groups[id].Add(particle);

        particle.OnExcite += UpdateGroup;
    }

    public void UnregisterParticle(Excitable particle) {
        int id;

        if (!_particleIDs.TryGetValue(particle, out id)) return;

        _groups[id].Remove(particle);

        particle.OnExcite -= UpdateGroup;
        
        if (_groups[id].Count <= 1)
            _groups.Remove(id);
    }

    void UpdateGroup(Excitable caller, int value) {
        var group = _groups[_particleIDs[caller]];

        foreach (Excitable particle in group) {
            if (particle != caller)
                particle.Excite(value);
        }
    }
}
