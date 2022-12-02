using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrapCollider : MonoBehaviour
{
    
       private void OnTriggerEnter2D(Collider2D collision)
       {
        if (collision.tag == "Player")
           {
            FindObjectOfType<MainMenuAudio>().Play("ScrapPickup");
			FindObjectOfType<HUDManager>().addScrap(1);
            Destroy(gameObject);
           }
       }
}
