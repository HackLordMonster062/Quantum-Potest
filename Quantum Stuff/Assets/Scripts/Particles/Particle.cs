using UnityEngine;

public abstract class Particle : Excitable {
	public ParticleBehavior Behavior { get; private set; }

    protected Anchor _anchor = null;

    protected override void Awake() {
        base.Awake();
        Behavior = GetComponent<ParticleBehavior>();
    }

    public virtual bool TryCapture(Anchor capturer) {
        if (Behavior.IsPickedUp || _anchor != null) return false;

        _anchor = capturer;

        return true;
    }
}
