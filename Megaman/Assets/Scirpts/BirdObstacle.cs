using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdObstacle : MonoBehaviour {

    Vector2 velocity;
    public float speed;
    public float range;
    Rigidbody2D rb;

    // Use this for initialization
    void Start()
    {
        if (speed <= 0.0f)
        {
            speed = 4.0f;
        }

        if (range == 0.0f)
        {
            range = 1.0f;
        }

        rb = GetComponent<Rigidbody2D>();
        if (!rb)
        {
            rb = gameObject.AddComponent<Rigidbody2D>();

        }
        
        rb.velocity = Vector2.left * speed;

        transform.position = new Vector3(transform.position.x, transform.position.y - Random.Range(0, range), transform.position.z);

        Destroy(gameObject, 1.0f);
    }

    // Update is called once per frame
    void Update()
    {

    }
   
}

