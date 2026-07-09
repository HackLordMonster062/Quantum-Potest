using System;
using UnityEngine;

public class DoorHandle : MonoBehaviour {
    [SerializeField] bool isExit;

    public event Action<Vector3> OnMoved;

    public void Move(Vector3 direction, float amount) {
        Vector3 move = direction * amount;
        move.z = 0;

        transform.position += move;

        OnMoved?.Invoke(move);
    }
}
