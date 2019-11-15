using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {


    public float lifeTime;
    


	// Use this for initialization
	void Start () {

        if (lifeTime <= 0)
        {
            lifeTime = 10.0f;
            Debug.Log("lifeTime not set. Defautling to " + lifeTime);
        }
        Destroy(gameObject, lifeTime);

    }
    void OnCollisionEnter2D(Collision2D c)
    {
        if (c.gameObject.tag != "Player") 
            Destroy(gameObject);
 
    }



	
	// Update is called once per frame
	void Update () {
		
	}
}
