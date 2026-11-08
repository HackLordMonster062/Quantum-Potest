using UnityEngine;

public abstract class Particle : Excitable {
	public ParticleBehavior Behavior { get; private set; }

    protected override void Awake() {
        base.Awake();
        Behavior = GetComponent<ParticleBehavior>();
    }
}
