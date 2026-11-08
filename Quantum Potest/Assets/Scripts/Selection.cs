using UnityEngine;

public class Selection : MonoBehaviour {
	public GameObject device;
	public bool isSelected;

	private void OnDrawGizmos() {
		if (isSelected && TryGetComponent(out BoxCollider collider)) {
			Gizmos.DrawWireCube(collider.bounds.center, collider.bounds.size);
		}
	}
}
