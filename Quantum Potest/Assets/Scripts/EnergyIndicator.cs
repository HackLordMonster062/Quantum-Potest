using UnityEngine;

public class EnergyIndicator : MonoBehaviour {
    [SerializeField] Excitable playerEnergy;
    [SerializeField] float baseSize;

    void Start() {
        
    }

    void Update() {
        transform.localScale = transform.localScale.Modify(y: playerEnergy.Energy * baseSize);
    }
}
