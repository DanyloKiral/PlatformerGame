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
        GameManager.instance.player = gameObject;
        GameManager.instance.sceneObjects.Add(this);
    }

    // Use this for initialization
    void FixedUpdate()
    {
        grounded = GetComponent<Rigidbody2D>().IsTouchingLayers(whatIsGround);
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
            case "Coin":
                PickCoin(other.gameObject);
                break;
            default:

                break;
        }
    }

    void PickCoin(GameObject coin)
    {
        SoundManager.instance.PlayEffect(Constants.instance.pickCoinSound);
        switch(coin.name)
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
            default:
                return;
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
        facingRight = !facingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
}
