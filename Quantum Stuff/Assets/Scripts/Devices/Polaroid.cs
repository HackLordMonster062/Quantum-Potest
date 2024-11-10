using UnityEngine;

public class Polaroid : Anchor {
    [SerializeField] PolaroidReceiver receiver;

    int _filterWidth;

    void Start() {
        receiver.OnExcite += UpdateFilter;
    }

    void UpdateFilter(int energy) {
		_filterWidth = energy;
	}

	public override void Activate() {
        if (_particle == null || !_particle.TryGetComponent(out Spectron spectron)) return;

        spectron.FilterColors(_filterWidth);
    }
}
