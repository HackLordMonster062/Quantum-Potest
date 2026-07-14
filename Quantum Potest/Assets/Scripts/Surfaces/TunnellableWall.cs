using UnityEngine;

public class TunnellableWall : MonoBehaviour, ISerializableElement {
	public ElementData Serialize() {
		return new SurfaceData("TunnellableWalll", transform.position, transform.eulerAngles, transform.localScale);
	}

	public void Deserialize(ElementData data) {
		transform.position = data.Position;
		transform.eulerAngles = data.Rotation;
		transform.localScale = data.Scale;
	}
}
