using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestMovement : MonoBehaviour
{

    [SerializeField] float moveSpeed = 6.0f;
    Rigidbody2D _rigidBody;
    BoxCollider2D _collider;
    Vector2 movement = Vector2.zero;


    private void Awake()
    {
        _rigidBody = GetComponent<Rigidbody2D>();
        _collider = GetComponent<BoxCollider2D>();
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
        //if (movement.x > 0)
        //{
        //    transform.localScale = new Vector3(1, 1);
        //} else
        //{
        //    transform.localScale = new Vector3(-1, 1);
        //}
    }

    private void FixedUpdate()
    {
        _rigidBody.MovePosition(_rigidBody.position + movement * Time.deltaTime * moveSpeed);
    }

}
