using UnityEngine;

[RequireComponent(typeof(SnapPotentialWell))]
public class MassiveParticle : Particle, ISerializableElement {
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

	public ElementData Serialize() {
		return new GravoData(transform.localPosition, transform.eulerAngles, transform.localScale, Energy);
	}

	public void Deserialize(ElementData data) {
		GravoData casted = (GravoData)data;

		transform.localPosition = casted.Position;
		transform.eulerAngles = casted.Rotation;
		transform.localScale = casted.Scale;
		Energy = casted.Energy;
	}
}
