using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Rigidbody2D rbPlayer;
    [SerializeField] float speed = 5f;

    [SerializeField] float jumpForce = 15f;
    [SerializeField] bool isJump;
    [SerializeField] bool inFloor = true;
    [SerializeField] Transform groundCheck;
    [SerializeField] LayerMask groundLayer;
    [SerializeField] public AudioClip pulo;
    [SerializeField] public AudioClip gameover;
     
    private AudioSource tocadorAudio;
    private GameManager gameManager;

    Animator animPlayer;

    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
    }

    private void Awake()
    {
        animPlayer = GetComponent<Animator>();
        rbPlayer = GetComponent<Rigidbody2D>();
        tocadorAudio = GetComponent<AudioSource>();

        if (tocadorAudio == null)
            tocadorAudio = gameObject.AddComponent<AudioSource>();
    }

    private void Update()
    {
        inFloor = Physics2D.Linecast(transform.position, groundCheck.position, groundLayer);
        Debug.DrawLine(transform.position, groundCheck.position, Color.blue);

        animPlayer.SetBool("Jump", !inFloor);

        if (Input.GetButtonDown("Jump") && inFloor)
            isJump = true;
        else if (Input.GetButtonUp("Jump") && rbPlayer.linearVelocity.y > 0)
            rbPlayer.linearVelocity = new Vector2(rbPlayer.linearVelocity.x, rbPlayer.linearVelocity.y * 0.5f);
    }   

    private void FixedUpdate()
    {
        Move();
        JumpPlayer();
    }

    void Move()
    {
        float xMove = Input.GetAxis("Horizontal");
        rbPlayer.linearVelocity = new Vector2(xMove * speed,rbPlayer.linearVelocity.y);

        animPlayer.SetFloat("Speed", Mathf.Abs(xMove));

        if(xMove > 0)
        {
            transform.eulerAngles = new Vector2(0,0);
        }
        else if(xMove < 0)
        {
            transform.eulerAngles = new Vector2(0,180);
        }
    }
    void JumpPlayer()
    {
        if (isJump)
        {
            rbPlayer.linearVelocity = Vector2.up * jumpForce;
            isJump = false;
            tocadorAudio.PlayOneShot(pulo);
        }
    }
     private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Obstacles"))
        {
            gameManager.GameOver();
            tocadorAudio.PlayOneShot(gameover);
        }
        if (collision.CompareTag("Scoring"))
        {
            gameManager.Scoring();
        }
    }
}
