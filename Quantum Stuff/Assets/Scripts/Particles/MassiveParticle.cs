using UnityEngine;

[RequireComponent(typeof(PotentialWell))]
public class MassiveParticle : Particle {
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
}
