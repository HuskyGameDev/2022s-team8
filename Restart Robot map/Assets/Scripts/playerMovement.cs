using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMovement : MonoBehaviour
{
    public Rigidbody2D rb;
    public Vector2 movement;
    public Vector2 mousePos;
    public float moveSpeed = 5f;
    public Camera cam;
    public Animator animator;
    public bool hasGun;
    // Update is called once per frame
    void Update()
    {
       if (hasGun == true)
            animator.SetBool("hasGun", true);
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
        if (movement.x != 0 || movement.y != 0)
            animator.SetBool("moving", true);
        else
            animator.SetBool("moving", false);

        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);


    }

    void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);

        Vector2 lookDir = mousePos - rb.position;
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg -90f;
        rb.rotation = angle;
    }
}
