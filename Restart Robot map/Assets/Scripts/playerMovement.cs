using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//Animation code based on https://www.youtube.com/watch?v=hkaysu1Z-N8

public class playerMovement : MonoBehaviour
{
    public SettingsManager settings;
    public Rigidbody2D rb;
    public Vector2 movement;
    public Vector2 mousePos;
    public float moveSpeed = 5f;
    public Camera cam;
    public Animator animator;
    public bool hasGun;

    [HideInInspector]
    public bool isDead;
    private float secondsSinceDeath;
    public int curHealth;
    [SerializeField]
    public int maxHealth = 100;

    public delegate void SetHealth(int amount);
    public static event SetHealth UpdateHealthEvent;

    //Called on startup
    private void Start()
    {
        curHealth = maxHealth;
        secondsSinceDeath = 0;
        UpdateHealthEvent(curHealth);
    }

    // Update is called once per frame
    void Update()
    {
        if (isDead) {
			secondsSinceDeath = secondsSinceDeath + 1*Time.deltaTime;
            if (secondsSinceDeath >= 3.0) {
                FindObjectOfType<MainMenuAudio>().Stop("MainGameBGM");
                FindObjectOfType<MainMenuAudio>().Play("UpgradeBGM");
                SceneManager.LoadScene("UpgradeMenu");
            }
            return;
        }
        
        if (hasGun == true)
            animator.SetBool("hasGun", true);
        if (Input.GetKey(settings.getKey("Left")))
        {
            movement.x = -1;
        }
        else if (Input.GetKey(settings.getKey("Right")))
        {
            movement.x = 1;
        }
        else
            movement.x = 0;
        if (Input.GetKey(settings.getKey("Down")))
        {
            movement.y = -1;
        }
        else if (Input.GetKey(settings.getKey("Up")))
        {
            movement.y = 1;
        }
        else
            movement.y = 0;
        if (movement.x != 0 || movement.y != 0)
            animator.SetBool("moving", true);
        else
            animator.SetBool("moving", false);

        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);

        //UPDATE HEALTH
        //DEBUG CODE
        /**
        if(Input.GetKeyDown(KeyCode.T))
        {
            DamagePlayer(10);
        }
        if(Input.GetKeyDown(KeyCode.Y))
        {
            DamagePlayer(1);
        }
        if(Input.GetKeyDown(KeyCode.U))
        {
            DamagePlayer(99);
        }
        if(Input.GetKeyDown(KeyCode.I))
        {
            DamagePlayer((int)Random.Range(1,10));
        }
        if(Input.GetKeyDown(KeyCode.H) && curHealth != maxHealth)
        {
            HealPlayer(10);
        }
        if(Input.GetKeyDown(KeyCode.J) && curHealth != maxHealth)
        {
            HealPlayer(1);
        }
        if(Input.GetKeyDown(KeyCode.K) && curHealth != maxHealth)
        {
            HealPlayer(100);
        }
        */
    }

    void FixedUpdate()
    {
        if (isDead)
			return;
        
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);

        Vector2 lookDir = mousePos - rb.position;
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg -90f;
        rb.rotation = angle;
    }

    public void Quit()
    {
        if (Input.GetKey("escape"))
        {
            Application.Quit();
        }
    }

    public void DamagePlayer(int amount)
    {
        curHealth -= amount;
        UpdateHealthEvent(curHealth);
        if (curHealth <= 0)
        {
            isDead = true;
            FindObjectOfType<MainMenuAudio>().Play("PlayerDeathWithExplosion");//PLAY DEATH SFX
            animator.SetBool("isDead", true);
        }
    }


    void Collision(Collision other)
    {
        if(other.gameObject.tag == ("Bullet"))
        {
            DamagePlayer(5);
        }
    }

    public void HealPlayer(int amount)
    {
        curHealth += amount;
        if (curHealth > maxHealth)
            curHealth = maxHealth;
        UpdateHealthEvent(curHealth);
    }

}
