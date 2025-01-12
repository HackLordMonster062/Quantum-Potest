using System;
using UnityEngine;

public abstract class Capturable : MonoBehaviour {
    [SerializeField] float mass;
    public float Mass => mass;

    [SerializeField] protected float dampingAmount;

	public Capturer Capturer { get; private set; }

	public event Action<Capturable> OnCapture;

    public bool TryCapture(Capturer capturer) {
        if (Capturer == null || capturer.Strength > Capturer.Strength) {
			OnCapture?.Invoke(this);

			Capturer = capturer;
            return true;
        }

        return false;
    }

    public virtual void Release() {
		Capturer = null;
    }

    // Returns distance to target (could be manipulated for bigger objects)
    public abstract Vector3 MoveTo(Vector3 target, float force);
}
