using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public enum MovementState
    {
        Idle,
        Running,
        Jumping,
        Atacking
    };


    [Header("Character Component")]
    Rigidbody2D characterRigidbody;
    Animator characterAnimator;
    SpriteRenderer spriteRenderer;

    [Header("Movement Values")]
    public int speed;
    public int jumpSpeed;
    public float moveInput;
  
    [Header("Movement State")]
    public bool isFlipX = false;
    public MovementState characterMovementState;

    [Header("Raycast State")]
    public LayerMask platformLayerMask;
    public float isGrounedRaycastHeight = 0.5f;

    [Header("Attack Values")]
    public int Damage;
    public float attackRange;
    public bool canAttack = true;
    public GameObject attackPoint;
    public LayerMask targetLayer;


    [Header("Plants")]
    public bool isTriger = false;
        

    // Start is called before the first frame update
    void Start()
    {
        characterRigidbody = GetComponent<Rigidbody2D>();
        characterAnimator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
        handleAnimation();
    }

    public void Movement()
    {
        moveInput = Input.GetAxis("Horizontal");

        characterRigidbody.velocity = new Vector2(moveInput * speed, characterRigidbody.velocity.y);

        Flip();

        if (Input.GetKeyDown(KeyCode.Space)  && isGrounded() && canAttack)
        {
            characterRigidbody.AddForce(Vector2.up * jumpSpeed);
        }

        if (Input.GetMouseButtonDown(0) && isGrounded() && canAttack)
        {
            Attack();
        }

        if (Input.GetKeyDown(KeyCode.F))
        {
            PlantTriger();
        }
        if (Input.GetKeyUp(KeyCode.F))
        {
            PlantTriger();
        }

    }

    public void Flip()
    {
        if (isFlipX == false && moveInput < 0)
        {
            Vector3 scaler = transform.localScale;
            scaler.x *= -1;
            transform.localScale = scaler;
            isFlipX = true;
        }
        else if (isFlipX == true && moveInput > 0)
        {
            Vector3 scaler = transform.localScale;
            scaler.x *= -1;
            transform.localScale = scaler;
            isFlipX = false;
        }
    }
    public bool isGrounded()
    {
        RaycastHit2D raycastHit2D = Physics2D.BoxCast(spriteRenderer.bounds.center, spriteRenderer.bounds.size,0f,Vector2.down,isGrounedRaycastHeight,platformLayerMask);
        return raycastHit2D.collider != null; ;
    }

    void handleAnimation()
    {
        if (isGrounded())
        {
            if(characterRigidbody.velocity.x == 0)
            {
                characterMovementState = MovementState.Idle;
                characterAnimator.SetBool("isJumping", false);
                characterAnimator.SetBool("isRunning", false);
            }
            else
            {
                characterMovementState = MovementState.Running;
                characterAnimator.SetBool("isJumping", false);
                characterAnimator.SetBool("isRunning", true);
            }
        }
        else
        {
            characterMovementState = MovementState.Jumping;
            characterAnimator.SetBool("isJumping", true);
            characterAnimator.SetBool("isRunning", false);
        }
    }


     void Attack()
    {
        StartCoroutine("AttackDelay");
    }

    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
            return;
        Gizmos.DrawWireSphere(attackPoint.transform.position, attackRange);
    }

    IEnumerator AttackDelay()
    {
        canAttack = false;
        Collider2D[] hitResult = Physics2D.OverlapCircleAll(attackPoint.transform.position, attackRange, targetLayer);

        if (hitResult != null)
        {
            characterAnimator.SetTrigger("isAttack");
        }
        yield return new WaitForSeconds(0.72f);
        canAttack = true;
        foreach (Collider2D hit in hitResult)
        {
            if (hit.GetComponent<Health>() != null)
            {
                Debug.Log("name:" + hit.transform.name);
                hit.GetComponent<Health>().TakeDamage(Damage);
            }
        }
    }



    void PlantTriger()
    {
        GameObject[] Plants = GameObject.FindGameObjectsWithTag("Plant");
        foreach(GameObject Plant in Plants)
        {
            Plant.GetComponent<BoxCollider2D>().isTrigger = !isTriger;
            
        }
        isTriger = !isTriger;
    }
}
