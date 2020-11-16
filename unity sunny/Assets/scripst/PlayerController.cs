using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Rigidbody2D rb;
    public float speed;
    public float jumpforce;

    private void Start()
    {
        
    }

    private void FixedUpdate()
    {
        Movement();
    }

    void Movement()
    {
        float horizontalmove =Input.GetAxis("Horizontal");
        float facedircetion = Input.GetAxisRaw("Horizontion");

        if (horizontalmove != 0)
        { 
        rb.velocity = new Vector2(horizontalmove * Time.deltaTime, rb.velocity.y);
        }
        if (facedircetion != 0)
        {
            transform.localScale = new Vector3(facedircetion, 1, 1);
        }
        if (Input.GetButtonDown("Jump"))
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpforce * Time.deltaTime);
        }



    }

}
