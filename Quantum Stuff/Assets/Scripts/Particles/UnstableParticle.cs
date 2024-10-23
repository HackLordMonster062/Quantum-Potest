using System.Collections;
using UnityEngine;

public class UnstableParticle : MonoBehaviour {
    [SerializeField] float maxEmission;
    [SerializeField] float emissionUpdateTime;

	Renderer _renderer;
	Color _baseColor;

    float _multiplier;

    static readonly int _emissionID = Shader.PropertyToID("_EmissionColor");

	void Awake() {
        _renderer = GetComponent<Renderer>();
        _baseColor = _renderer.material.GetColor(_emissionID);

        _multiplier = 0;
    }

    void Start() {
        StartCoroutine(EmissionDecay());
    }

    void Decay() {
        Destroy(gameObject);
    }

    IEnumerator EmissionDecay() {
        while (_multiplier < maxEmission) {
            yield return new WaitForSeconds(emissionUpdateTime);

            _multiplier += emissionUpdateTime;

			_renderer.material.SetColor(_emissionID, _baseColor * _multiplier);
		}

        Decay();
    }
}
