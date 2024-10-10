using System.Collections.Generic;
using static UnityEngine.ParticleSystem;

public class EntanglementManager : Singleton<EntanglementManager> {
    Dictionary<int, List<Excitable>> _groups;
    Dictionary<Excitable, int> _particleIDs;

    int _currID = 0;

    protected override void Awake() {
        base.Awake();

        _groups = new();
        _particleIDs = new();
    }

    public void RegisterPair(Excitable particle1, Excitable particle2) {
        bool doesExist1 = _particleIDs.TryGetValue(particle1, out int id1);
        bool doesExist2 = _particleIDs.TryGetValue(particle2, out int id2);

        if (doesExist1 && doesExist2)
            MergeGroups(id1, id2);
        else if (doesExist1)
            RegisterParticle(particle2, id1);
        else if (doesExist2)
            RegisterParticle(particle1, id2);
        else {
            int id = _currID++;

			_groups[id] = new List<Excitable>();

			RegisterParticle(particle1, id);
			RegisterParticle(particle2, id);
		}
    }

    public void UnregisterParticle(Excitable particle) {
        int id;

        if (!_particleIDs.TryGetValue(particle, out id)) return;

        _groups[id].Remove(particle);

        particle.OnExcite -= UpdateGroup;
        
        if (_groups[id].Count <= 1)
            _groups.Remove(id);
    }

    void MergeGroups(int id1, int id2) {
        var secList = _groups[id2];

        foreach (var particle in secList) {
            _groups[id1].Add(particle);
            _particleIDs[particle] = id1;
        }

        _groups.Remove(id2);
    }

    void RegisterParticle(Excitable particle, int id) {
        _groups[id].Add(particle);
        _particleIDs[particle] = id;
		particle.OnExcite += UpdateGroup;
	}

    void UpdateGroup(Excitable caller, int value) {
        var group = _groups[_particleIDs[caller]];

        foreach (Excitable particle in group) {
            if (particle != caller)
                particle.Excite(value);
        }
    }
}
