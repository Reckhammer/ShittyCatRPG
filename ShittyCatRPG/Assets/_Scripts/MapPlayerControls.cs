using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapPlayerControls : MonoBehaviour
{
    public float speed = 2;
    private Rigidbody2D rigidbody;
    private Vector2 moveDirection;

    private void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        moveDirection = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
    }

    private void FixedUpdate()
    {
        rigidbody.MovePosition((Vector2)transform.position + (moveDirection * speed * Time.deltaTime));
    }
}
