using UnityEngine;

public class PlayerMovement : MonoBehaviour {
    public float speed;
    [Range(1, 100)] public float jumpVelocity;
    private Rigidbody2D body;
    private Animator anim;
    private SpriteRenderer sprite;
    public bool onGround, spaceReleased, canMove;

    private float coyoteTime = 0.2f;
    public float coyoteTimeCounter;

    private float jumpBufferTime = 0.2f;
    public float jumpBufferTimeCounter;


    public AudioSource audioS;

    public AudioClip audioC;
    private void Awake() {
        // Grab references for rigidbody, animator, and sprite
        body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
        spaceReleased = true;
        onGround = true;
        canMove = true;
    }

    private void Update() {
        // Test for space release
        if (Input.GetKeyUp(KeyCode.Space)) {
            spaceReleased = true;
        }

        if(Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.UpArrow)) {
            canMove = false;
        }
        if(Input.GetKeyUp(KeyCode.S) || Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.DownArrow) || Input.GetKeyUp(KeyCode.UpArrow)) {
            canMove = true;
        }
    }

    private void FixedUpdate() {
        // Setting horizontal velocity
        float horizontalInput = Input.GetAxis("Horizontal");

        if (canMove) {
            if (onGround) {
                // Faster on ground
                body.velocity = new Vector2(Input.GetAxis("Horizontal") * speed, body.velocity.y);
                // When on ground, reset coyoteTimeCounter
                if (body.velocity.y <= 0f) {
                    coyoteTimeCounter = coyoteTime;
                }
            } else {
                // Slower mid-air
                body.velocity = new Vector2(Input.GetAxis("Horizontal") * speed * 0.55f, body.velocity.y);
                // When mid-air, decrement coyoteTimeCounter
                coyoteTimeCounter -= Time.deltaTime;
            }
        }
        
        // Flip player by direction
        if (horizontalInput > 0.01f) {
            transform.localScale = new Vector3(1, 1, 1);
        } else if (horizontalInput < -0.01f) {
            transform.localScale = new Vector3(-1, 1, 1);
        }

        // Raycast for onGround detection
        RaycastHit2D hitInfo = Physics2D.Raycast(transform.position - new Vector3(0, sprite.bounds.extents.y + 0.01f, 0), Vector3.down, 0.1f);
        
        if (hitInfo) {
            if (hitInfo.collider.CompareTag("Ground")) {
                onGround = true;
            } else {
                onGround = false;
            }
        } else {
            onGround = false;
        }

        // Setting jumpBuffer
        if (Input.GetKey(KeyCode.Space) && spaceReleased) {
            jumpBufferTimeCounter = jumpBufferTime;
        } else {
            jumpBufferTimeCounter -= Time.deltaTime;
        }

        // Jump
        if (coyoteTimeCounter > 0f && jumpBufferTimeCounter > 0f) {
            // Give horizontal velocity
            body.velocity = Vector2.up * jumpVelocity;
            spaceReleased = false;
            JumpSound();
            coyoteTimeCounter = 0f;
            jumpBufferTimeCounter = 0f;
        }

        // Set animator parameters
        // anim.SetBool("turn", horizontalInput != 0);
        anim.SetBool("run", horizontalInput != 0 && canMove);
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.tag.Equals("Spike")) {
            RewindManager.rm.StartAllRewind();
        }
    }

    public void JumpSound(){
        audioS.PlayOneShot(audioC);
    }
}