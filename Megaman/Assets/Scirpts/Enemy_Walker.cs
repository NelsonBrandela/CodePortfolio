using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Enemy_Walker : Character
{


    public float eHealth;

    void Start()
    {

        rb2 = GetComponent<Rigidbody2D>();
        if (!rb2)
        {
            Debug.Log("No Ridigbody2D found.");
        }

        if (speed <= 0)
        {
            speed = 1.0f;
            Debug.Log("Speed not set. Defautling to " + speed);
        }

        if (eHealth <= 0)
        {
            eHealth = 4.0f;
            Debug.Log("Enemy Health not set. Defaulting to: " + eHealth);
        }
    }


    void Update()
    {
        if (isFacingLeft)

            rb2.velocity = new Vector2(-speed, rb2.velocity.y);

        else

            rb2.velocity = new Vector2(speed, rb2.velocity.y);

        if (eHealth == 0)
        {
            Destroy(gameObject);
        }
    }

    void OnCollisionEnter2D(Collision2D c)
    {
        if (c.gameObject.tag != "Ground" && c.gameObject.tag != "Player")
        {
            Flip();
        }

        if (c.gameObject.tag == "Projectile")
        {
            eHealth--;

        }

    }

    public int points
    {
        get; set;
    }








}
