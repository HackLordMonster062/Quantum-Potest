using DG.Tweening;
using System.Linq;
using UnityEngine;

public class Rail : Activatable, ISerializableElement {
    [SerializeField] Transform device;
    [SerializeField] Transform[] pathPoints;
    [SerializeField] float speed;

    int _currPoint = 0;
    bool _forward = false;
    Tween _currTween;

	private void Start() {
		//device.position = pathPoints[0].position;
	}

	public override void Activate(int _ = 0) {
        if (_currTween == null || !_currTween.IsActive())
            StartTravel();
	}

	void StartTravel() {
        _forward = !_forward;

        _currPoint = _forward ? 0 : pathPoints.Length - 1;

        MoveToNextPoint();
    }

    void MoveToNextPoint() {
        Vector3 target = pathPoints[_currPoint].position;

		float duration = Vector3.Distance(device.position, target) / speed;

        _currTween = device.DOMove(target, duration)
            .SetEase(Ease.Linear)
            .OnComplete(OnReached);
    }

    void OnReached() {
        _currPoint += _forward ? 1 : -1;

        if (_currPoint < pathPoints.Length && _currPoint >= 0) {
            MoveToNextPoint();
		}
    }

    public void Initialize(Transform[] newPath, Transform newDevice) {
        device = newDevice;
		pathPoints = newPath;

        _currPoint = 0;
        device.position = pathPoints[0].position;

        if (_currTween != null && _currTween.IsActive()) {
            _currTween.Kill();
            _currTween = null;
        }
    }

	public ElementData Serialize() {
        return new RailData(
            "Rail",
            transform.localPosition,
            transform.eulerAngles,
            transform.localScale,
            gameObject.GetEntityId().ToString(),
            _trigger == null ? "" : _trigger.gameObject.GetEntityId().ToString(),
            pathPoints.Select(point => point.position).ToArray(),
            device.GetEntityId().ToString()
        );
	}

	public void Deserialize(ElementData data) {
		transform.localPosition = data.Position;
		transform.eulerAngles = data.Rotation;
        transform.localScale = data.Scale;
	}
}
