using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    // Controller
    private Rigidbody2D rigidbodyPlayer;
    private PlayerInput inputPlayer;
    public Transform spriteRenderer;
    private bool isGrounded;
    private bool isWalled;
    // Animator
    public Animator animator;
    // Jump
    public float jumpForce;
    public Transform feetPos;
    public float checkRadius;
    public LayerMask Ground;
    // Movement
    public float speed;
    private float inputMove;
    bool facingLeft = true;
    // Dash
    public float dashSpeed;
    private float dashTime;
    public float startDashTime;
    private int direction;
    public float dashRate = 1.0f;
    float nextDashTime = 0.0f;
    // Attack
    public Transform attackPoint;
    public float attackRange = 0.5f;
    public LayerMask enemyLayer;
    public float attackRate = 1.0f;
    float nextAttackTime = 0.0f;
    bool attacked = false;
    private void Awake()
    {
        rigidbodyPlayer = GetComponent<Rigidbody2D>();
        inputPlayer = GetComponent<PlayerInput>();
        dashTime = startDashTime;
    }
    public void MoveSideways(InputAction.CallbackContext context)
    {
        inputMove = context.ReadValue<float>();
    }
    void GameOver()
    {

    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 8)
        {
            health--;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 8)
        {
            health--;
        }
    }
    void Flip()
    {
        spriteRenderer.localScale = new Vector2(spriteRenderer.localScale.x * -1, spriteRenderer.localScale.y);
        facingLeft = !facingLeft;
    }
    public void Jump(InputAction.CallbackContext context)
    {
        if (context.performed && isGrounded == true)
        {
            rigidbodyPlayer.velocity = Vector2.up * jumpForce;
        }
    }
    public void DashPlayer(InputAction.CallbackContext context)
    {
        if (Time.time >= nextDashTime && context.performed)
        {
            if (direction == 0)
            {
                if (inputMove > 0)
                {
                    direction = 1;
                }
                else if (inputMove < 0)
                {
                    direction = 2;
                }
            }
            nextDashTime = Time.time + 1.0f / dashRate;
        }
    }
    public void Attack(InputAction.CallbackContext context)
    { 
        if (Time.time >= nextAttackTime && context.performed)
        {
            animator.SetTrigger("Attack");
            Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayer);
            foreach (Collider2D enemy in hitEnemies)
            {
                Debug.Log(hitEnemies);
                enemy.GetComponent<EnemyHP>().TakeDamage(this.gameObject.GetComponent<Collider2D>());
            }
            nextAttackTime = Time.time + 1.0f / attackRate;
        }
    }
    // Health
    public int health;
    public int nhearts;
    public Image[] hearts;
    public Sprite empty;
    public Sprite full;

    void Update()
    {
        // Health
        if (health > nhearts)
        {
            health = nhearts;
        }
        if (health <= 0)
        {
            GameOver();
        }
        for (int i = 0; i < hearts.Length; i++)
        {
            if (i < health)
            {
                hearts[i].sprite = full;
            }
            else
            {
                hearts[i].sprite = empty;
            }
            if (i < nhearts)
            {
                hearts[i].enabled = true;
            }
            else
            {
                hearts[i].enabled = false;
            }
        }
        // Movement
        if (!isWalled)
        {
            if (inputMove>0 && facingLeft)
            {
                Flip();
            }
            if (inputMove<0 && !facingLeft)
            {
                Flip();
            }
            rigidbodyPlayer.velocity = new Vector2(inputMove * speed * Time.deltaTime, rigidbodyPlayer.velocity.y);
            animator.SetFloat("Speed", Mathf.Abs(rigidbodyPlayer.velocity.x));
        }
        // Jump      
        isGrounded = Physics2D.OverlapCircle(feetPos.position, checkRadius, Ground);
        animator.SetBool("Grounded", isGrounded);
        // Dash
        if (direction != 0)
        {
            if (dashTime <= 0)
            {
                direction = 0;
                dashTime = startDashTime;
                rigidbodyPlayer.velocity = Vector2.zero;
            }
            else
            {
                dashTime -= Time.deltaTime;
                if(direction == 1)
                {
                    rigidbodyPlayer.velocity = Vector2.right * dashSpeed;
                }
                else if (direction == 2)
                {
                    rigidbodyPlayer.velocity = Vector2.left * dashSpeed;
                }
            }
        }
    }
}
