using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;      //角色碰撞物件
    private Animator anim;       //動畫效果

    public CircleCollider2D coll;     //場地碰撞物件
    public float speed;
    public float jumpforce;
    public LayerMask ground;    //LayerMask:塗層(角色人物要碰撞的圖)
    public int cherry;

    public Text cherrynumber;
    private bool ishurt; //默認是false，可用來做勾選開關

    void Start()
    {
        rb = GetComponent<Rigidbody2D>(); //遊戲開始讓系統自動去獲得Component等參數
        anim = GetComponent<Animator>();  //遊戲開始讓系統自動去獲得Component等參數
    }

    void FixedUpdate()
    {
        if (!ishurt)
        {
            Movement();
        }
        switchAnim();
    }

    void Movement()
    {
        float horizontalmove = Input.GetAxis("Horizontal");
        float facedircetion = Input.GetAxisRaw("Horizontal");

        //角色移動
        if (horizontalmove != 0)
        {
            rb.velocity = new Vector2(horizontalmove * speed * Time.fixedDeltaTime, rb.velocity.y);
            anim.SetFloat("running", Mathf.Abs(facedircetion));
        }
        //角色左右方向畫面
        if (facedircetion != 0)
        {
            transform.localScale = new Vector3(facedircetion, 1, 1);
        }
        //角色跳躍
        if (Input.GetButtonDown("Jump") && coll.IsTouchingLayers(ground))
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpforce * Time.deltaTime);
            anim.SetBool("jumping", true);
        }

    }
    //切換遊戲動畫
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
        else if (ishurt)
        {
            anim.SetBool("hurt", true);
            anim.SetFloat("running",0);
            if (Mathf.Abs(rb.velocity.x) < 0.1)
            {

                anim.SetBool("hurt", false);
                anim.SetBool("idle", true);
                ishurt = false;
            }

        }

        else if (coll.IsTouchingLayers(ground))     //如果掉落的過程中觸碰到了預設好得Ground
        {
            anim.SetBool("falling", false);
            anim.SetBool("idle", true);
        }
    }
    //收集cherry
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "collection")
        {
            Destroy(collision.gameObject);
            cherry += 1;
            cherrynumber.text = cherry.ToString();
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "enemies")
        {
            if (anim.GetBool("falling"))
            {
                Destroy(collision.gameObject);
                rb.velocity = new Vector2(rb.velocity.x, jumpforce * Time.deltaTime);
                anim.SetBool("jumping", true);
            }
            else if (transform.position.x <= collision.gameObject.transform.position.x)
            {
                rb.velocity = new Vector2(-10, rb.velocity.y);
                ishurt = true;
            }
            else if (transform.position.x <= collision.gameObject.transform.position.x)
            {
                rb.velocity = new Vector2(-10, rb.velocity.y);
                ishurt = true;
            }
        }
    }
}

