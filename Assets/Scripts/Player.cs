using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class PlayerSpriteAnimator : MonoBehaviour
{
    public Sprite[] runningSprites; 
    public Sprite idleSprite;       
    public float animationSpeed = 0.1f; 
    public float speed = 5f;            
    public float jumpForce = 5f;       

    private SpriteRenderer spriteRenderer;
    private int currentFrame;
    private float timer;
    private Rigidbody2D rb;
    private bool isGrounded;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        
        float move = Input.GetAxis("Horizontal");
        transform.Translate(new Vector3(move * speed * Time.deltaTime, 0, 0));

        
        if (move != 0)
        {
            AnimateRun();
        }
        else
        {
            spriteRenderer.sprite = idleSprite;
        }

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
            isGrounded = false; 
        }
    }

    void AnimateRun()
    {
        timer += Time.deltaTime;

        if (timer >= animationSpeed)
        {
            timer = 0f;
            currentFrame = (currentFrame + 1) % runningSprites.Length;
            spriteRenderer.sprite = runningSprites[currentFrame];
        }
    }

    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }

        else if (collision.gameObject.CompareTag("itemBad"))
        {
            Destroy(collision.gameObject); 
            PlayerDeath();
        }
        else if (collision.gameObject.CompareTag("DeathZone"))
        {
            PlayerDeath(); 
        }
    }
    void PlayerDeath()
    {
        SceneManager.LoadScene("SampleScene");
    }
}
