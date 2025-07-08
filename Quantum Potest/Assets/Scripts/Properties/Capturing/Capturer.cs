using System;
using UnityEngine;

public abstract class Capturer : MonoBehaviour {
    [SerializeField] float maxMass;
    [SerializeField] CapturerStrengh strength;
    public CapturerStrengh Strength => strength;

    public Action<Capturable> OnCapture;
    public Action<Capturable> OnRelease;

	protected virtual bool TryCapture(Capturable capturable) {
        if (capturable.Mass <= maxMass && capturable.TryCapture(this, strength)) {
            capturable.OnCapture += Give;

            OnCapture?.Invoke(capturable);

			return true;
        }

        return false;
    }

    protected virtual void Release(Capturable capturable) {
		capturable.Release();
		capturable.OnCapture -= Give;
		OnRelease?.Invoke(capturable);
	}

    protected virtual void Give(Capturable capturable, CapturerStrengh _) {
		capturable.OnCapture -= Give;
	}
}

public enum CapturerStrengh {
    ActionAnchor = 1,
    Player = 2,
    MassiveParticle = 3,
    LockAnchor = 4,
    Entangled = 5
}