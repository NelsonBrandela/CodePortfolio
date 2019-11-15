using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{

    Vector2 velocity;
    public float speed;
    public float range;
    Rigidbody2D rb;

    // Use this for initialization
    void Start()
    {
        if (speed <= 0)
        {
            speed = 4;
        }

        if (range == 0)
        {
            range = 4;
        }

        rb = GetComponent<Rigidbody2D>();
        if (!rb)
        {
            rb = gameObject.AddComponent<Rigidbody2D>();

        }
        rb.isKinematic = true;
        rb.velocity = Vector2.up * speed;

        transform.position = new Vector3(transform.position.x, transform.position.y - Random.Range(-range, range), transform.position.z);

        Destroy(gameObject, 2.0f);
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnTriggerEnter2D(Collider2D c)
    {
        if (c.gameObject.tag == "ObstacleDestroyer")
        {
            Destroy(gameObject);
        }
        }
}
