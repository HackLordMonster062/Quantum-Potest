using UnityEngine;

public class DataScraper : MonoBehaviour {
	

    void Start() {
        
    }

    void Update() {
        
    }

    void GetAllData() {
        foreach (ISerializableElement element in transform.GetComponentsInChildren<ISerializableElement>()) {

        }
    }
}
