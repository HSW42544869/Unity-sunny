using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Rigidbody2D rb;      //角色碰撞物件
    public Animator anim;       //動畫效果
    public Collider2D coll;     //場地碰撞物件
    public float speed;
    public float jumpforce;
    public LayerMask ground;    //LayerMask:塗層(角色人物要碰撞的圖)

    void Start()
    {

    }

    void FixedUpdate()
    {
        Movement();
        switchAnim();
    }

    void Movement()
    {
        float horizontalmove = Input.GetAxis("Horizontal");
        float facedircetion = Input.GetAxisRaw("Horizontal");

        //角色移動
        if (horizontalmove != 0)
        {
            rb.velocity = new Vector2(horizontalmove * speed * Time.deltaTime, rb.velocity.y);
            anim.SetFloat("running", Mathf.Abs(facedircetion));
        }
        //角色左右方向畫面
        if (facedircetion != 0)
        {
            transform.localScale = new Vector3(facedircetion, 1, 1);
        }
        //角色跳躍
        if (Input.GetButtonDown("Jump"))
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpforce * Time.deltaTime);
            anim.SetBool("jumping", true);
        }

    }
 
    void switchAnim()
    {
        anim.SetBool("idle", false);

        if (anim.GetBool("jumping"))
        {
            if (rb.velocity.y < 0)      //如果跳躍的速度低於0時
            {
                anim.SetBool("jumping", false);
                anim.SetBool("falling", true);
            }
        }
        else if (coll.IsTouchingLayers(ground))     //如果掉落的過程中觸碰到了預設好得Ground
        {
            anim.SetBool("falling", false);
            anim.SetBool("idle", true);
        }
    }
}
