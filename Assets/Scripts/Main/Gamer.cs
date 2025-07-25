using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gamer : MonoBehaviour
{
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] public float jumpHeight = 0.5f;

    public float moveSpeed = 5.0f;
    public float runSpeed = 8.0f;
       
    public float jumpDuration = 0.4f;

    private Rigidbody2D rb;
    private Animator animator;
    private bool isJumping = false;
    private Vector3 originalPosition;

    private bool isFrozen = false;

    public void FreezeMovement()
    {
        isFrozen = true;
        rb.velocity = Vector2.zero;
    }

    public void UnfreezeMovement()
    {
        isFrozen = false;
    }


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0.0f;

        spriteRenderer = transform.Find("Model").GetComponent<SpriteRenderer>();
        animator = transform.Find("Model").GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isFrozen || isJumping) return;

        if (isJumping) return;

        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");

        bool isRunning = Input.GetKey(KeyCode.LeftShift);
        float currentSpeed = isRunning ? runSpeed : moveSpeed; 
        
        Vector2 moveDir = new Vector2(moveX, moveY).normalized;
        rb.velocity = moveDir * currentSpeed;
       

        if (moveX != 0)
        {
            spriteRenderer.flipX = moveX < 0;
        }

        animator.SetBool("isRunning", isRunning);
        animator.SetFloat("speed", Mathf.Abs(moveX + moveY));

        if (Input.GetKeyDown(KeyCode.Space))
        {
            //animator.SetTrigger("Jump");
            StartCoroutine(JumpRoutine());

        }
        IEnumerator JumpRoutine()
        {
            isJumping = true;
            animator.SetTrigger("Jump");

            originalPosition = transform.position;

            // Y°ª »ó½Â
            transform.position += new Vector3(0, jumpHeight, 0);

            yield return new WaitForSeconds(jumpDuration);

            // ¿ø·¡ À§Ä¡·Î º¹±Í
            transform.position = originalPosition;

            isJumping = false;

        }


    }

   
}
