using UnityEngine;

public class PickUp : MonoBehaviour {
    [SerializeField] float castWidth;
    [SerializeField] float reach;
    [SerializeField] Transform holdingPoint;

    Transform _camera;

    bool _holding = false;
    ILiftable _item;
    Transform _itemTranform;

    void Start() {
        _camera = Camera.main.transform;
    }

    void Update() {
        if (!Input.GetKeyDown(KeyCode.E)) return;

        if (_holding) {
            _item.Drop();
            _item = null;

			_itemTranform.SetParent(null);
            _itemTranform = null;

			_holding = !_holding;
		} else if (Physics.SphereCast(_camera.position, castWidth, _camera.forward, out RaycastHit info, reach) && 
                   info.collider.TryGetComponent(out _item)) {
            _item.PickUp();

            _itemTranform = info.transform;

            _itemTranform.SetParent(holdingPoint);
            _itemTranform.position = holdingPoint.position;

			_holding = !_holding;
		}
    }
}
