using System;
using UnityEngine;

public class DoorHandle : MonoBehaviour {
    [SerializeField] bool isExit;

    public event Action<Vector3> OnMoved;

    Level _containingLevel;

	private void Start() {
        _containingLevel = GetComponentInParent<Level>();
	}

	public void Move(Vector3 direction, float amount) {
        Vector3 initial = transform.position;

        transform.position += direction.normalized * amount;
        transform.position.Modify(z: isExit ? _containingLevel.Size.z : 0);

        OnMoved?.Invoke(transform.position - initial);
    }
}
