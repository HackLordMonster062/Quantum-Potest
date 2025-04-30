using UnityEngine;

[RequireComponent(typeof(Anchor))]
public class Polaroid : MonoBehaviour {
	[SerializeField] ActivatorAnchor activatorAnchor;

	Anchor _anchor;

	int _filterFrequency = 0;

	private void Awake() {
		_anchor = GetComponent<Anchor>();

		activatorAnchor.OnActivate += UpdateFilter;
		activatorAnchor.OnDeactivate += FinalizeFilter;
	}

	void UpdateFilter(int energy) {
		if (energy > _filterFrequency)
			_filterFrequency = energy;
	}

	void FinalizeFilter() {
		if (_anchor.Particle == null || !_anchor.Particle.TryGetComponent(out Spectron spectron)) return;

		spectron.FilterColors(_filterFrequency);

		_filterFrequency = 0;
	}



	/*[SerializeField] PolaroidReceiver receiver;

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

	public void Activate() {
        if (_anchor.Particle == null || !_anchor.Particle.TryGetComponent(out Spectron spectron)) return;

        spectron.FilterColors(_filterWidth);
    }*/
}
