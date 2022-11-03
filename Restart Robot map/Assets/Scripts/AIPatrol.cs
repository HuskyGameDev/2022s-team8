/*
    AI for patrolling back and forth inside of the game, goes from wall to wall does not go to fixed points
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIPatrol : MonoBehaviour
{
    public float walkSpeed;
    // Start is called before the first frame update
    [HideInInspector]
    public bool mustPatrol;
    private bool mustFlip;

    //REMINDER CREATE A COLLIDER FOR THE ENEMY SO THAT IT CAN DETECT WHEN RUNNING INTO WALLS
    public RigidBody2D rb;
    public Collider2D bodyCollider;
    public Layer Tilemap_walls;

    void Start()
    {
        mustPatrol = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (mustPatrol)
        {
            patrol();
        }
    }

    private void fixedUpdate()
    {
        if(mustPatrol)
        {
            mustTurn = Physics2d.OverlapCircle(groundCheckPos.position, 0.1f, Tilemap_base);
        }
    }
    void patrol()
    {
        //REMINDER: SET WALL LAYER TO TILEMAP WALLS OTHERWISE IT WILL NOT TURN AROUND
        if(bodyCollider.IsTouchingLayers(Tilemap_walls))
        {
            flip();
        }
        rb.velocity = new Vector2(walkSpeed * Time.fixedDeltaTime, rb.velocity.y);
    }

    void flip()
    {
        mustPatrol = false;
        transform.localScale = new Vector2(transform.localScale.x * -1, transform.localScale.y);
        walkSpeed *= -1;
        mustPatrol = true;
    }
}
