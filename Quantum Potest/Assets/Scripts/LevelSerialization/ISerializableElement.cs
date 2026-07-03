using UnityEngine;

public interface ISerializableElement {
	ElementData Serialize();
	void Deserialize(ElementData data);
}
