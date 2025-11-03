using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Rigidbody2D rbPlayer;
    [SerializeField] float speed = 5f;

    private void Awake()
    {
        rbPlayer = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        Move();
    }

    void Move()
    {
        float xMove = Input.GetAxis("Horizontal");
        rbPlayer.linearVelocity = new Vector2(xMove * speed,rbPlayer.linearVelocity.y);

        if(xMove > 0)
        {
            transform.eulerAngles = new Vector2(0,0);
        }
        else if(xMove < 0)
        {
            transform.eulerAngles = new Vector2(0,180);
        }
    }
}
