using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class TestTouching : MonoBehaviour
{
    [SerializeField] BoxCollider2D otherCollider;
    BoxCollider2D _collider;

    private void Awake()
    {
        _collider = GetComponent<BoxCollider2D>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (_collider.IsTouching(otherCollider))
        {
            Debug.Log("Touching");
        }
    }
}
