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
    public static float moveSpeed = 5f;
    public Camera cam;
    public Animator animator;
    public bool hasGun;

    [HideInInspector]
    public bool isDead;
    private float secondsSinceDeath;
    public int curHealth;
    public int maxHealth;

    public delegate void SetHealth(int amount);
    public static event SetHealth UpdateHealthEvent;

    //Called on startup
    private void Start()
    {
        maxHealth = UpgradesManagerScript.Instance.getMaxHealth();
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
        FindObjectOfType<MainMenuAudio>().Play("PlayerDamage");
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
