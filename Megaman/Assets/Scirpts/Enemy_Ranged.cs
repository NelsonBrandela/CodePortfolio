using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy_Ranged : MonoBehaviour {

    public Projectile projectilePrefab;
    public Transform projectileSpawnPoint;
    public float projectileSpeed;

    public bool isFacingLeft;

    public float fireRate;
    float timeSinceLastFire;

    public float eHealth;

    Animator anim;

    public bool isShooting;
    public LayerMask isShootingLayer;
    public Transform shootCheck;
    public float shootCheckRadius;

    AudioSource audioSource;
    public AudioClip sfxShoot;
    public AudioClip sfxTakeDmg;



    // Use this for initialization
    void Start() {

        if (!projectilePrefab)
        {
            Debug.Log("No projectilePrefab found on" + projectilePrefab);
        }

        if (!projectileSpawnPoint)
        {
            Debug.Log("No ProjectileSpawnPoint found on " + projectileSpawnPoint);
        }

        if (projectileSpeed <= 0)
        {
            projectileSpeed = 5.0f;
            Debug.Log("ProjectileSpeed not set. Defaulting to:" + projectileSpeed);
        }

        if (fireRate <= 0)
        {
            fireRate = 2.0f;
            Debug.Log("fireRate not set. Defaulting to: " + fireRate);
        }

        if(eHealth <= 0)
        {
            eHealth = 4.0f;
            Debug.Log("Enemy Health not set. Defaulting to: " + eHealth);
        }

        anim = GetComponent<Animator>();
        if (!anim)
        {
            Debug.Log("No animator found on: " + name);
        }

        if (!shootCheck)
        {
            Debug.Log("No ShootCheck found");
        }

        if (shootCheckRadius <= 0)
        {
            shootCheckRadius = 0.2f;
            Debug.Log("GroundCheckRadius not set. Defaulting to " + shootCheckRadius);
        }


        audioSource = GetComponent<AudioSource>();
        if (!audioSource)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
            audioSource.playOnAwake = false;
            audioSource.loop = false;
            audioSource.volume = 1.0f;

            Debug.Log("No Animator found on " + name);
        }

    }
	
	// Update is called once per frame
	void Update () {

        isShooting = Physics2D.OverlapCircle(shootCheck.position, shootCheckRadius, isShootingLayer);

        anim.SetBool("EShoot", isShooting);
        
       

       

        if (eHealth <= 0)
        {

            
            Destroy(gameObject);
        }
      
    }

    void OnCollisionEnter2D(Collision2D c2)
    {
        if(c2.gameObject.tag == "Projectile")
        {
            eHealth--;
            PlaySound(sfxTakeDmg);
            
        }


    }

    void OnTriggerStay2D(Collider2D c)
    {
        if (c.gameObject.tag == "Player")
        {
            if (Time.time > timeSinceLastFire + fireRate)
            {
                Fire();

                timeSinceLastFire = Time.time;
            }
        }
    }



    void Fire()
    {
        Debug.Log("Pew Pew");

        Projectile p = Instantiate(projectilePrefab, projectileSpawnPoint.position, projectileSpawnPoint.rotation);

        p.tag = "Projectile_Enemy";

        p.GetComponent<SpriteRenderer>().color = Color.blue;
        if (!isFacingLeft)
        {
            p.GetComponent<Rigidbody2D>().AddForce(Vector2.left * projectileSpeed, ForceMode2D.Impulse);
        }
        PlaySound(sfxShoot);
    }

    public void PlaySound(AudioClip clip, float volume = 1.0f)
    {
        audioSource.clip = clip;
        audioSource.volume = volume;
        audioSource.Play();
    }

}
