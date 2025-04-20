using System;
using UnityEngine;

public abstract class Capturable : MonoBehaviour {
    [SerializeField] float mass;
    public float Mass => mass;

    [SerializeField] protected float dampingAmount;

	protected Vector3 _target;
    public Vector3 Target => _target;
	protected float _force;

	public Capturer Capturer { get; private set; }

	public event Action<Capturable, CapturerStrengh> OnCapture;
	public event Action<Capturable> OnRelease;
	public event Action<Vector3, float> OnMove;

    public bool TryCapture(Capturer capturer, CapturerStrengh strength) {
        if (Capturer == null || capturer.Strength > Capturer.Strength) {
			Capturer = capturer;

			OnCapture?.Invoke(this, strength);
            return true;
        }

        return false;
    }

    public virtual void Release() {
        OnRelease?.Invoke(this);
		Capturer = null;
    }

    // Returns distance to target (could be manipulated for bigger objects)
    public virtual Vector3 MoveTo(Vector3 target, float force) {
        OnMove?.Invoke(target, force);
        _target = target;
        _force = force;
        return target - transform.position;
    }

    public virtual Vector3 MoveBy(Vector3 amount, float force) {
        return MoveTo(_target + amount, force);
    }
}
