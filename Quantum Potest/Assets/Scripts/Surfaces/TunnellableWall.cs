using UnityEngine;

public class TunnellableWall : MonoBehaviour, ISerializableElement {
	public ElementData Serialize() {
		return new SurfaceData("TunnellableWall", transform.localPosition, transform.eulerAngles, transform.localScale);
	}

	public void Deserialize(ElementData data) {
		transform.localPosition = data.Position;
		transform.eulerAngles = data.Rotation;
		transform.localScale = data.Scale;
	}
}
