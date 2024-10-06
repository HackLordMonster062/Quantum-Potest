using UnityEngine;

[RequireComponent(typeof(PotentialWell))]
public class MassiveParticle : Excitable {
    PotentialWell _potentialWell;

    protected override void Awake() {
        base.Awake();

        _potentialWell = GetComponent<PotentialWell>();
    }

	public override void Excite(int energy) {
		base.Excite(energy);

        Energy = 1;

        _potentialWell.Disable();
	}

	protected override void Deplete() {
		base.Deplete();

        _potentialWell.Enable();
	}
}
