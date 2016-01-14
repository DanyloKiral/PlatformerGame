using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class Player : MonoBehaviour {

    public float maxSpeed = 10f;
    public float jumpForce = 700f;
    public LayerMask whatIsGround;

    public int coins = 0;

    private bool grounded = false;
    private bool facingRight = true;
    private float move;

    void Start()
    {
        GetComponent<Rigidbody2D>().centerOfMass = GetComponent<CircleCollider2D>().offset;
        Physics2D.gravity = new Vector2(0, Physics2D.gravity.y * GetComponent<Rigidbody2D>().mass);
        transform.position = new Vector3(CameraScript.leftBottom.x + 1.28f*5, CameraScript.leftBottom.y + 10, 1);
        maxSpeed *= GetComponent<Rigidbody2D>().mass;
        jumpForce *= GetComponent<Rigidbody2D>().mass;
        //transform.localScale = new Vector3(Constants.instance.Scale, Constants.instance.Scale, 1);
        GameManager.instance.player = gameObject;
    }

    // Use this for initialization
    void FixedUpdate()
    {
        grounded = GetComponent<CircleCollider2D>().IsTouchingLayers(whatIsGround);
        move = Input.GetAxis("Horizontal");
    }

    // Update is called once per frame
    void Update()
    {
        Mover();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        switch (other.gameObject.tag)
        {
            case "BronseCoin":
            case "SilverCoin":
            case "GoldCoin":
                PickCoin(other.gameObject);
                break;
            default:

                break;
        }
    }

    void PickCoin(GameObject coin)
    {
        SoundManager.instance.PlayEffect(Constants.instance.pickCoinSound);
        switch(coin.tag)
        {
            case "BronseCoin":
                print("bronse coin!");
                coins += Constants.instance.bronseCoinPoints;
                break;
            case "SilverCoin":
                print("silver coin!");
                coins += Constants.instance.silverCoinPoints;
                break;
            case "GoldCoin":
                print("gold coin!");
                coins += Constants.instance.goldCoinPoints;
                break;
        }
        coin.SetActive(false);
    }

    void Mover()
    {
        AnimationCaller();
        if (grounded && (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow)))
        {
            GetComponent<Rigidbody2D>().AddForce(new Vector2(0f, jumpForce));
        }
        GetComponent<Rigidbody2D>().velocity = new Vector2(move * maxSpeed, GetComponent<Rigidbody2D>().velocity.y); 
        if (move > 0 && !facingRight)
            Flip();
        else if (move < 0 && facingRight)
            Flip();
        if (Input.GetKey(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        /* if (Input.GetKey(KeyCode.Escape))
        {
        Application.Quit();
        }*/
    }

    void AnimationCaller()
    {
        if (grounded)
        {
            GetComponent<Animator>().SetBool("Jump", false);
        }
        else
        {
            GetComponent<Animator>().SetBool("Jump", true);           
        }
        if (move != 0 && grounded)
        {
            GetComponent<Animator>().SetBool("Walk", true);
        }
        else
        {
            GetComponent<Animator>().SetBool("Walk", false);
        }

    }

    void Flip()
    {
        GetComponent<Rigidbody2D>().centerOfMass *= -1;
        facingRight = !facingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
}
