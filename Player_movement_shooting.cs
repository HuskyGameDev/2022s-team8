using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed= 5f;

    public Rigidbody2D rb;
    public Camera cam;

    Vector2 movement;

    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
    }

    void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * moveSpeed* Time.fixedDeltaTime);


        Vector2 lookDir = mousePos- rb.position;
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 90f;
        rb.rotation = angle;
    }
}
public class Shooting : MonoBehaviour{

    public Transform firePoint;
    public GameObject bulletPrefab

    public float bulletForce = 20f;

    void Update(){
        if(Input.GetButtonDown("Fire1")){
            Shoot();
        }
    }
    void Shoot(){// shooting code, but needs some tweaking.  
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.AddForce(firePoint.up * bulletForce, ForceMode2D.Impulse);
    }


}

public class Bullet : Mono Behaviour{

    public GameObject hitEffect;

    void OnCollisionEnter2D(Collsion2D collision){
        GameObject effect = Instantiate(hitEffect, transform.position, Quaternion.identity);
        Destroy(effect, 5f);
        Destroy(GameObject);
    }
}