using UnityEngine;

[RequireComponent(typeof(SnapPotentialWell))]
public class MassiveParticle : Particle, ISerializableElement<GravoData> {
    [SerializeField] int activationEnergy;

    SnapPotentialWell _potentialWell;

    protected override void Awake() {
        base.Awake();

        _potentialWell = GetComponent<SnapPotentialWell>();
    }

	public override void Excite(int energy, bool invoke = true) {
        Energy = 1;
		base.Excite(activationEnergy, invoke);

        _potentialWell.Disable();
	}

	protected override void Deplete() {
		base.Deplete();

        _potentialWell.Enable();
	}

	public GravoData Serialize() {
		return new GravoData(transform.position, transform.eulerAngles, Energy);
	}

	public void Deserialize(GravoData data) {
		transform.position = data.Position;
		transform.eulerAngles = data.Rotation;
		Energy = data.Energy;
	}
}
