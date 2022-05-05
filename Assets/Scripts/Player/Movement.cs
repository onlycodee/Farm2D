using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] float moveSpeed = 6.0f;
    Rigidbody2D _rigidBody;
    BoxCollider2D _collider;
    Vector2 movement = Vector2.zero;
    Animator _anim;
    bool canMove = true;

    private void Awake()
    {
        _rigidBody = GetComponent<Rigidbody2D>();
        _collider = GetComponent<BoxCollider2D>();
        _anim = GetComponent<Animator>();
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (!canMove) return;
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
        if (!Mathf.Approximately(movement.x, .0f))
        {
            _anim.SetFloat("moveX", movement.x);
        }
    }

    private void FixedUpdate()
    {
        _rigidBody.MovePosition(_rigidBody.position + movement * Time.deltaTime * moveSpeed);
        movement.x = 0;
        movement.y = 0;
    }


    public void SetMove(bool canMove)
    {
        this.canMove = canMove;
    }
}
