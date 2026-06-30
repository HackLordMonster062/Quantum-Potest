using UnityEngine;

public interface ISerializableElement<T> where T : ElementData {
	T Serialize();
	void Deserialize(T data);
}
