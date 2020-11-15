using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class hero : MonoBehaviour
{
    [SerializeField]
    private int health;
    [SerializeField]
    private float speed; 
    [SerializeField]
    public float jumpForce;

    private bool isGrounded = false;

    public List<partOfHero> PartsOfHero;

    public List<partOfHero> Legs;

    new private Rigidbody2D rigidbody;
    private Animator animator;
    private SpriteRenderer sprite;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    private CharState State
    {
        get { return (CharState)animator.GetInteger("State"); }
        set { animator.SetInteger("State", (int)value); }
    }

    private void FixedUpdate()
    {
        CheckGround();
    }

    private void CheckGround()
    {
        if (Legs.Where(x => x.IsGrounded).Count() > 1)
            isGrounded = true;
        else
            isGrounded = false;

        if (!isGrounded) State = CharState.Jump;
    }

    public void ReciveDamage(int damage)
    {
        health -= damage;
    }

    private void Update()
    {
        if (isGrounded) State = CharState.Idle;

        if (Input.GetButton("Horizontal") || Input.GetAxis("Horizontal") != 0) Run(Input.GetAxis("Horizontal"));
        if (Input.GetButtonDown("Jump"))
            if (isGrounded)
                Jump(jumpForce);
}

    public void Jump(float jf)
    {
        rigidbody.velocity = Vector3.Scale(rigidbody.velocity, new Vector3(1, 0, 0));
        rigidbody.AddForce(transform.up * jf, ForceMode2D.Impulse);
    }

    private void Run(float axis)
    {
        if (isGrounded) State = CharState.Run3;

        Vector3 direction = transform.right * axis;
        transform.position = Vector3.MoveTowards(transform.position, transform.position + direction, speed * Time.deltaTime);

        //sprite.flipX = direction.x < 0.0F;
    }
}

public enum CharState
{
    Idle,
    Run,
    Jump,
    Run2,
    Run3,
}