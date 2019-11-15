using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Character : MonoBehaviour
{

    Rigidbody2D rb;
    public Rigidbody2D rb2;
    public float speed;
    public float jumpForce;

    public bool isGrounded;
    public LayerMask isGroundLayer;
    public Transform groundCheck;
    public float groundCheckRadius;

    public Projectile projectilePrefab;
    public Transform projectileSpawnPoint;
    public float projectileSpeed;

    public bool isFacingLeft;

    public float jumpForceBoost;
    public float jumpForceBoostTime;

    public float playerHeatlh;
    public GameObject target;

    public bool isHit;
    public LayerMask isHitLayer;
    public Transform hitCheck;
    public float hitCheckRadius;

    public bool isShooting;
    public LayerMask isShootingLayer;
    public Transform shootCheck;
    public float shootCheckRadius;

    public bool isDead;
    public LayerMask isDeathLayer;
    public Transform deathCheck;
    public float deathCheckRadius;

    Animator anim;

    AudioSource audioSource;
    public AudioClip sfxJump;
    public AudioClip sfxShoot;
    public AudioClip sfxLand;
    public AudioClip sfxTakedmg;
    public AudioClip sfxDeath;

    public bool isClimbing;
    public float climbingSpeed;

    public Text scoreText;

    // Use this for initialization
    void Start()
    {
        // Grab Compenents
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        if (!rb)
        {
            Debug.Log("No Ridigbody2D found.");
        }
        else
        {
            rb.constraints = RigidbodyConstraints2D.FreezeRotation;
            rb.mass = 2.0f;
        }

        if (speed <= 0)
        {
            speed = 1.0f;
            Debug.Log("Speed not set. Defaulting to: " + speed);
        }

        if (jumpForce <= 0)
        {
            jumpForce = 6.0f;
            Debug.Log("JumpForce not set. Defaulting to: " + jumpForce);
        }

        if (!deathCheck)
        {
            Debug.Log("No DeathCheck found");
        }

        if (deathCheckRadius <= 0)
        {
            deathCheckRadius = 0.1f;
            Debug.Log("deathCheckRadius not set. Defaulting to " + deathCheckRadius);
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

        if (!groundCheck)
        {
            Debug.Log("No Groundcheck found");
        }

        if (groundCheckRadius <= 0)
        {
            groundCheckRadius = 0.1f;
            Debug.Log("GroundCheckRadius not set. Defaulting to " + groundCheckRadius);
        }

        if (!hitCheck)
        {
            Debug.Log("No Hitcheck found");

        }

        if (hitCheckRadius <= 0)
        {
            hitCheckRadius = 0.3f;
            Debug.Log("HitCheckRadius not set. Defaulting to " + hitCheckRadius);
        }

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
            projectileSpeed = 3.0f;
            Debug.Log("ProjectileSpeed not set. Defaulting to:" + projectileSpeed);
        }

        if (!anim)
        {
            Debug.Log("No animator found on: " + name);
        }

        if (jumpForceBoost <= 0)
        {
            jumpForceBoost = 4.0f;
            Debug.Log("jumpForceBoost not found, defaulting to: " + jumpForceBoost);
        }

        if (jumpForceBoostTime <= 0)
        {
            jumpForceBoostTime = 2.0f;
            Debug.Log("jumpForceBoost not found, defaulting to: " + jumpForceBoostTime);
        }

        if (playerHeatlh <= 0)
        {
            playerHeatlh = 100.0f;
            Debug.Log("Player health not set. Defaulting to: " + playerHeatlh);

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
    void Update()
    {

        float moveValue = Input.GetAxis("Horizontal");


        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, isGroundLayer);
        isHit = Physics2D.OverlapCircle(hitCheck.position, hitCheckRadius, isHitLayer);
        isShooting = Physics2D.OverlapCircle(shootCheck.position, shootCheckRadius, isShootingLayer);
        isDead = Physics2D.OverlapCircle(deathCheck.position, deathCheckRadius, isDeathLayer);




        anim.SetFloat("MoveSpeed", Mathf.Abs(moveValue));
        anim.SetFloat("Health", playerHeatlh);
        anim.SetBool("Ground", isGrounded);
        anim.SetBool("Hit", isHit);
        anim.SetBool("Shoot", isShooting);
        anim.SetBool("Death", isDead);
        anim.SetBool("Climb", isClimbing);

        if (isDead || playerHeatlh == 0)
        {

            jumpForce = 0.0f;
            speed = 0.0f;

            Die();
            Debug.Log("You died");
        }

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            PlaySound(sfxJump);
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }


        rb.velocity = new Vector2(moveValue * speed, rb.velocity.y);


        if (Input.GetButtonDown("Fire1"))
        {
            PlaySound(sfxShoot);
            Debug.Log("Pew Pew");
            Fire();
        }

        if (moveValue < 0 && !isFacingLeft)
        {
            Flip();
        }
        else if (moveValue > 0 && isFacingLeft)
        {
            Flip();
        }




        float climbingSpeed = Input.GetAxis("Vertical");
        if (isClimbing == true)
        {
            if (climbingSpeed > 0)
            {
                rb.gravityScale = 0;

                rb.velocity = new Vector2(rb.velocity.x, climbingSpeed * speed);
                Debug.Log("Climbing");
            }
            else if (climbingSpeed < 0)
            {
                rb.gravityScale = 0;
                rb.velocity = new Vector2(rb.velocity.x, climbingSpeed * speed);
                Debug.Log("ClimbingDown");
            }
            else
                Debug.Log("Idle on Ladder");
        }




    }

    void Fire()
    {
        Debug.Log("Pew Pew");
        if (isFacingLeft)
        {
            Projectile p = Instantiate(projectilePrefab, projectileSpawnPoint.position, projectileSpawnPoint.rotation);
            p.GetComponent<Rigidbody2D>().AddForce(Vector2.left * projectileSpeed, ForceMode2D.Impulse);
        }
        else if (!isFacingLeft)
        {
            Projectile p = Instantiate(projectilePrefab, projectileSpawnPoint.position, projectileSpawnPoint.rotation);
            p.GetComponent<Rigidbody2D>().AddForce(Vector2.right * projectileSpeed, ForceMode2D.Impulse);
        }



    }


    public void Flip()
    {
        isFacingLeft = !isFacingLeft;

        Vector3 scaleFactor = transform.localScale;

        scaleFactor.x *= -1;

        transform.localScale = scaleFactor;

    }




    void OnTriggerEnter2D(Collider2D c)
    {
        Debug.Log("Trigger Detected: " + c.gameObject.tag);

        if (c.gameObject.tag == "Collectible")
        {
            Collectible col = c.GetComponent<Collectible>();
            if (col)
            {
                Debug.Log("Collected " + col.points + "points ");

            }
            Destroy(c.gameObject);
        }

        if (c.gameObject.tag == "PowerUp_Jump")
        {
            jumpForce += jumpForceBoost;
            StartCoroutine("StopJumpBoost");
            Destroy(c.gameObject);

        }

        if (c.gameObject.tag == "KillZone")
        {
            playerHeatlh = 0.0f;
            Debug.Log("You died Restart the game.");

        }

        if (c.gameObject.tag == "Ladder")
        {
            isClimbing = true;
        }

        if(c.gameObject.tag == "Win")
        {
            SceneManager.LoadScene("Win");
        }



    }

    private void OnTriggerExit2D(Collider2D c)
    {
        if (c.gameObject.tag == "Ladder")
        {
            isClimbing = false;
            rb.gravityScale = 1.0f;
        }


    }



    IEnumerator StopJumpBoost()
    {
        yield return new WaitForSeconds(jumpForceBoostTime);
        jumpForce -= jumpForceBoost;
    }


    void OnCollisionEnter2D(Collision2D c2)
    {
        if (c2.gameObject.tag == "Projectile_Enemy")
        {
            Debug.Log("Decrement by: " + 1);
            playerHeatlh--;
            PlaySound(sfxTakedmg);
        }
       
    }



    public void PlaySound(AudioClip clip, float volume = 1.0f)
    {
        audioSource.clip = clip;
        audioSource.volume = volume;
        audioSource.Play();
    }

    void Die()
    {
        Destroy(gameObject, 2);
        SceneManager.LoadScene("MegaMan");
    }


}