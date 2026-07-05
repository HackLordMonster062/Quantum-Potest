using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Anchor))]
public class Polaroid : MonoBehaviour, ISerializableElement {
	[SerializeField] ActivatorAnchor activatorAnchor;

	Anchor _anchor;

	int _filterFrequency = 0;

	bool _isFiltering = false;
	float _timeToFilter;

	private void Awake() {
		_anchor = GetComponent<Anchor>();

		activatorAnchor.OnActivate += UpdateFilter;
		activatorAnchor.OnDeactivate += InitializeFilter;
	}

	private void OnDestroy() {
		activatorAnchor.OnActivate -= UpdateFilter;
		activatorAnchor.OnDeactivate -= InitializeFilter;
	}

	private void Update() {
		if (!_isFiltering) return;
		
		_timeToFilter -= Time.deltaTime;

		if (_timeToFilter <= 0) FinalizeFilter();
	}

	void UpdateFilter(int energy) {
		if (energy > _filterFrequency)
			_filterFrequency = energy;

		if (energy > 0)
			InterruptFilter();
	}

	void InitializeFilter() {
		_timeToFilter = PhysicsManager.instance.RelaxtationTime;

		_isFiltering = true;
	}

	void InterruptFilter() {
		_isFiltering = false;
	}

	void FinalizeFilter() {
		if (_anchor.Particle == null || !_anchor.Particle.TryGetComponent(out Spectron spectron)) return;

		spectron.FilterColors(_filterFrequency);

		_filterFrequency = 0;

		_isFiltering = false;
	}

	public ElementData Serialize() {
		return new DeviceData("Polaroid", transform.localPosition, transform.eulerAngles, gameObject.GetEntityId().ToString());
	}

	public void Deserialize(ElementData data) {
		transform.localPosition = data.Position;
		transform.eulerAngles = data.Rotation;
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
