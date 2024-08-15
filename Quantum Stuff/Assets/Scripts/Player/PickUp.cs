using UnityEngine;

public class PickUp : MonoBehaviour {
    [SerializeField] Transform holdingPoint;

    bool _holding = false;
    ILiftable _item;
    Transform _itemTranform;

    public ILiftable Item { get { return _item; } }

    public void Drop() {
        if (!_holding) return;

		_item.Drop();
		_item = null;

		_itemTranform.SetParent(null);
		_itemTranform = null;

		_holding = false;
	}

    public void PickUpItem(ILiftable item, Transform itemTransform) {
        if (_holding)
            Drop();

        _item = item;

		_item.PickUp();

        _itemTranform = itemTransform;

		_itemTranform.SetParent(holdingPoint);
		_itemTranform.position = holdingPoint.position;

		_holding = true;
	}
}
