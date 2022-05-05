using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestCollision : MonoBehaviour
{
    [SerializeField] GameObject other;
    [SerializeField] GameObject other1;
    LayerMask mark;
    BoxCollider2D _collider;
    Rigidbody2D _rgb;
    ContactFilter2D _filter;
    // Start is called before the first frame update
    void Start()
    {
        _collider = GetComponent<BoxCollider2D>();
        _rgb = GetComponent<Rigidbody2D>();
        _filter = new ContactFilter2D();
        _filter.useTriggers = true;
        _filter.useLayerMask = true;
        _filter.SetLayerMask(LayerMask.GetMask("A"));
        //_filter.SetLayerMask(Physics2D.GetLayerCollisionMask(LayerMask.NameToLayer("A")));
        if (_filter.IsFilteringLayerMask(other1.gameObject))
        {
            print("Is filter");
        }
        //_filter.NoFilter();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            List<Collider2D> result = new List<Collider2D>();
            _collider.OverlapCollider(_filter, result);
            print("Result count: " + result.Count);
            //List<Collider2D> result = new List<Collider2D>();
            //ContactFilter2D filter = new ContactFilter2D();
            //filter.useTriggers = false;
            //filter.useLayerMask = true;
            //LayerMask mask = new LayerMask();
            //filter.SetLayerMask(mask);
            //int c = _collider.OverlapCollider(filter, result);
            //print("C: " + c);
            
            //if (_collider.IsTouchingLayers(LayerMask.GetMask("A")))    
            //{
            //    print("Touching layer A");
            //}
            ////_collider.OverlapCollider(filter, result);
            //if (result.Count > 0)
            //{
            //    print("Count > 0: " + result[0].gameObject.name);
            //} else
            //{
            //    print("Count ======== 0");
            //}
        }
    }
}
