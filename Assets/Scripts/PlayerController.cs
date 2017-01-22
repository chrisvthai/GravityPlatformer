using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class PlayerController : MonoBehaviour
{

    [HideInInspector]
    public bool facingRight = true;
    [HideInInspector]
    public bool jump = false;

    public float groundSpeedScale;
    public float moveForce = 150;
    public float maxSpeed = 5f;
    public float jumpForce = 500f;
    public Transform groundCheck;
    public GameObject DeadUI;
    public int nextscene;
    public AudioClip[] jumpClips;
    public AudioClip[] ouchClips;

    private bool grounded = false;
    private float groundVelocity;
    public Vector3 lastCheckpoint; //For keeping track of checkpoints
    public Vector2 checkpointGravity;
    

    private Animator anim;
    private GameObject player;
    private Rigidbody2D rb2d;
    private bool door; //Is the player at a door?
    



    //Everything related to gravity changes
    private Vector2 up = new Vector2(0, 1);
    private Vector2 down = new Vector2(0, -1);
    private Vector2 left = new Vector2(-1, 0);
    private Vector2 right = new Vector2(1, 0);
    public Vector2 gravityDir;

    // Use this for initialization
    void Start()
    {
        anim = GetComponent<Animator>();
        rb2d = GetComponent<Rigidbody2D>();
        DeadUI.SetActive(false);
        gravityDir = down;
        lastCheckpoint = rb2d.position;

        jumpClips = new AudioClip[] {(AudioClip)Resources.Load("Audio/Player/Jumps/Player-jump1"),
                                      (AudioClip)Resources.Load("Audio/Player/Jumps/Player-jump2"),
                                        (AudioClip)Resources.Load("Audio/Player/Jumps/Player-jump3")};

        ouchClips = new AudioClip[] {(AudioClip)Resources.Load("Audio/Player/Ouch/Player-ouch1"),
                                      (AudioClip)Resources.Load("Audio/Player/Ouch/Player-ouch2"),
                                        (AudioClip)Resources.Load("Audio/Player/Ouch/Player-ouch3"),
                                          (AudioClip)Resources.Load("Audio/Player/Ouch/Player-ouch4")};

    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            gravityDir = up;
        }
        else if (Input.GetKeyDown(KeyCode.K))
        {
            gravityDir = down;
        }
        else if (Input.GetKeyDown(KeyCode.J))
        {
            gravityDir = left;
        }
        else if (Input.GetKeyDown(KeyCode.L))
        {
            gravityDir = right;
        }


        if (gravityDir == up) {
            transform.Rotate(new Vector3(0, 0, 180));
            // groundCheck.position = new Vector3(0, 0.9890461f, 0); 
        }
        else if (gravityDir == down)
        {
            transform.Rotate(new Vector3(0, 0, 0));
            // groundCheck.position = new Vector3(0, -0.9890461f, 0);
        }
        else if (gravityDir == left)
        {
            transform.Rotate(new Vector3(0, 0, -90));
            //groundCheck.position = new Vector3(-0.9890461f, 0, 0);
        }
        else if (gravityDir == right)
        {
            transform.Rotate(new Vector3(0, 0, 90));
            //groundCheck.position = new Vector3(0.9890461f, 0, 0);
        }



    }

    void FixedUpdate()
    {
        Physics2D.gravity = 9.81f * gravityDir;

        /*****Determine if the player is touching the ground*****/
        //I have to provide some offset to the linecast, or else we could potentially double jump (some problems with my platform colliders

        grounded = Physics2D.Linecast(transform.position, groundCheck.position, 1 << LayerMask.NameToLayer("Ground"));
        
        //obstacleHit = Physics2D.Linecast(transform.position, groundCheck.position, 1 << LayerMask.NameToLayer("Laser Beams"));

        //grounded = Physics2D.Raycast(transform.position, -gravityDir, 0.9890461f);
        if (Input.GetKeyDown(KeyCode.Space) && grounded)
        {
            jump = true;
        }

        /*Below helps determine speed of the player*/
        float h;
        if (gravityDir == down || gravityDir == up)
        {
            h = Input.GetAxis("Horizontal");
            anim.SetFloat("Speed", Mathf.Abs(h));
        } else
        {
            h = Input.GetAxis("Vertical");
            anim.SetFloat("Speed", Mathf.Abs(h));
        }

        /*******Ground v/s air movement*********/
        if (grounded) { 
            if (gravityDir == down || gravityDir == up)
                rb2d.velocity = new Vector2(h * maxSpeed * groundSpeedScale, rb2d.velocity.y);
            else rb2d.velocity = new Vector2(rb2d.velocity.x, h * maxSpeed * groundSpeedScale);
        }
        else //in the air
        {
            if (gravityDir == down || gravityDir == up)
            {
                /*if (Mathf.Abs(h * rb2d.velocity.x) < groundVelocity)
                    rb2d.AddForce(Vector2.right * h * moveForce);

                if (Mathf.Abs(rb2d.velocity.x) >= groundVelocity)
                    rb2d.velocity = new Vector2(Mathf.Sign(rb2d.velocity.x) * groundVelocity, rb2d.velocity.y);
                */

                //If movement would speed up player past ground speed, ignore it (h is in direction of player's velocity and the velocity is at ground speed)
                if (!(h * rb2d.velocity.x > 0 && Mathf.Abs(rb2d.velocity.x) >= groundVelocity ))
                {
                    rb2d.AddForce(Vector2.right * h * moveForce);
                }
                
               

            } else
            {
                /*if (h * rb2d.velocity.y < groundVelocity)
                    rb2d.AddForce(Vector2.up * h * moveForce);

                if (Mathf.Abs(rb2d.velocity.y) >= groundVelocity)
                    rb2d.velocity = new Vector2(rb2d.velocity.x, Mathf.Sign(rb2d.velocity.y) * groundVelocity);
                */
                if (!(h * rb2d.velocity.y > 0 && Mathf.Abs(rb2d.velocity.y) >= groundVelocity))
                {
                    rb2d.AddForce(Vector2.up * h * moveForce);
                }
            }
        }

        /******* Flipping the Character *********/
        if (gravityDir == down || gravityDir == right)
        {
            if (h > 0 && !facingRight)
                Flip();
            else if (h < 0 && facingRight)
                Flip();
        } else if (gravityDir == up || gravityDir == left)
        {
            if (h > 0 && facingRight)
                Flip();
            else if (h < 0 && !facingRight)
                Flip();
        }



        /******* Jumping ******/
        if (jump)
        {
            anim.SetTrigger("Jump");

            //What was the initial ground velocity?
            if (gravityDir == up || gravityDir == down)
                groundVelocity = Mathf.Abs(rb2d.velocity.x);
            else groundVelocity = Mathf.Abs(rb2d.velocity.y);

            int i = Random.Range(0, jumpClips.Length);
            AudioSource.PlayClipAtPoint(jumpClips[i], transform.position);

            rb2d.AddForce(jumpForce * -gravityDir);
            jump = false;
        }

        

     
    }

    void Flip()
    {
        facingRight = !facingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;

    }


    void OnTriggerEnter2D (Collider2D other)
    {
        if (other.gameObject.CompareTag("Laser Beam"))
        {
            //anim.SetTrigger("Die");
            DeadUI.SetActive(true);

           this.enabled = false;
            int i = Random.Range(0, ouchClips.Length);
            AudioSource.PlayClipAtPoint(ouchClips[i], transform.position);
            //rb2d.isKinematic = true;

        } else if (other.gameObject.CompareTag("Door"))
        {
            if (Input.GetKeyDown(KeyCode.E))
                SceneManager.LoadScene(nextscene);
        } else if (other.gameObject.CompareTag("Checkpoint"))
        {
            lastCheckpoint = rb2d.position;
            checkpointGravity = gravityDir;
        }
    }

    void OnTriggerStay2D (Collider2D other)
    {
        if (other.gameObject.CompareTag("Door"))
        {
            if (Input.GetKeyDown(KeyCode.E))
                SceneManager.LoadScene(nextscene);
        }
    }
    
    
   
}
 
    