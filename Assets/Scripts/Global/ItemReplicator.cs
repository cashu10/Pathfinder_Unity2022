using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemReplicator : MonoBehaviour
{
    [SerializeField] private GameObject _item;
    [SerializeField] private int _count;
    [SerializeField] private float _spacing;
    [SerializeField] private Vector2 _startingPos;
    [SerializeField] private bool _inX;

    private void Awake() 
    {
        Vector2 change = _inX ? new Vector2(_spacing, 0) : new Vector2(0, _spacing);
        for(int i=0; i<_count; i++)
        {
            var clone = Instantiate(_item,  _startingPos + (change * i), Quaternion.identity);
            clone.transform.SetParent(this.transform, false);
        }
    }
}
