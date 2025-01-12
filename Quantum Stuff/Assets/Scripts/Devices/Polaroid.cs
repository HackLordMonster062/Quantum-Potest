using UnityEngine;

[RequireComponent(typeof(Anchor))]
public class Polaroid : Activatable {
    [SerializeField] PolaroidReceiver receiver;

    int _filterWidth;

    Anchor _anchor;

	private void Awake() {
		_anchor = GetComponent<Anchor>();
	}

	void Start() {
        receiver.OnExcite += UpdateFilter;
    }

    void UpdateFilter(int energy) {
		_filterWidth = energy;
	}

	public override void Activate() {
        if (_anchor.Particle == null || !_anchor.Particle.TryGetComponent(out Spectron spectron)) return;

        spectron.FilterColors(_filterWidth);
    }
}
