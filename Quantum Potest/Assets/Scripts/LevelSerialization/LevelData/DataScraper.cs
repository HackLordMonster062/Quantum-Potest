using UnityEngine;
using Newtonsoft.Json;
using System.Collections.Generic;

public class DataScraper : MonoBehaviour {
	

    void Start() {
        JsonSerializerSettings settings = new JsonSerializerSettings {
            TypeNameHandling = TypeNameHandling.Auto,
            Formatting = Formatting.Indented,
            ContractResolver = new UnityFieldsOnlyContractResolver()
        };

        string json = JsonConvert.SerializeObject(GetAllData(), settings);

        print(json);
    }

    void Update() {
        
    }

    List<ElementData> GetAllData() {
        List<ElementData> data = new();

		foreach (ISerializableElement element in transform.GetComponentsInChildren<ISerializableElement>()) {
            data.Add(element.Serialize());
		}

        return data;
    }
}
