using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : Unit {

    public static bool isGround, isMove, isDie = false;
    private float speedX;
    private new Collider2D collider;
    

    private bool isMode = true;

    [SerializeField] private Transform groundCheck;
    [SerializeField] private float checkRadius;
    [SerializeField] private LayerMask whatIsGround;

    public static bool IsGround { get { return isGround; } }
    public static bool IsMove { get { return isMove; } }

    public static bool IsDie { get { return isDie; } }

    private CharacterAudioManager foot;
    private string tagName;
    public float jumpDistance = 10f;
    

    protected void Start() {
        GetReferences();

        currentHealth = maxHealth;
        speedX = speed / 100;
        isDie = false;
    }

    private void GetReferences() { // Function for getting components
        collider = GetComponentInChildren<Collider2D>();
        rigidbody = GetComponent<Rigidbody2D>();
        sprite = GetComponentInChildren<SpriteRenderer>();
        animator = GetComponent<Animator>();
        foot = GetComponent<CharacterAudioManager>();
    }

    private void FixedUpdate() {
        animator.SetBool("inMove", false);
        animator.SetBool("isGround", isGround);
        isGround = Physics2D.OverlapCircle(groundCheck.position, checkRadius, whatIsGround);
      
        Run();
        GetTag();
        GetStep();
    }

    private void Update() {
        Jump();
        BattleMode();
    }


    private void Run() {

        if (Input.GetButton("Horizontal"))
        {
            Vector3 direction = transform.right * Input.GetAxis("Horizontal");
            transform.Translate(speedX * direction.x, 0, 0);
            isMove = true;
            animator.SetBool("inMove", true);
            sprite.flipX = direction.x < 0.0F;
            movingRight = direction.x > 0.0F;
        }
        else isMove = false;

    }


    private void Jump() {
        if(Input.GetButtonDown("Jump") && isGround)
        {
            rigidbody.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }
    }

    void BattleMode() { // Entering combat mode
        if (Input.GetKeyDown(KeyCode.E))
            if (isMode)
            {
                animator.SetBool("isBattleMode", isMode);
                animator.SetFloat("speed", 1.0f);
                isMode = !isMode;
            }
            else
            {
                animator.SetBool("isBattleMode", isMode);
                animator.SetFloat("speed", -1.0f);
                isMode = !isMode;
            }        
    }

    private void GetTag() { // Looks for the tag of the surface the player is standing on

        //All colliders of objects on the ground layer are written to an array
        Collider2D[] groundCollider = Physics2D.OverlapCircleAll(groundCheck.position, checkRadius, whatIsGround);

        foreach (Collider2D col in groundCollider) 
                tagName = col.gameObject.tag;       
    }


    /*Based on the accepted value of the variable "tagName", the case will be selected and specific 
    parameters will be passed to the FootstepsCharacter class method PlayStep()*/
    void GetStep() {
        switch (tagName)
        {
            case "Leaves":
                foot.PlayStep(CharacterAudioManager.StepsOn.Leaves, 1);
                break;
            case "Wood":
                foot.PlayStep(CharacterAudioManager.StepsOn.Wood, 0.3f);
                break;
        }
    }

    protected override void Die() {
        isDie = true;

        Debug.Log("Die " + this.gameObject);
        animator.SetBool("isDie", true);
        animator.SetTrigger("Hit");
        GetComponent<CapsuleCollider2D>().enabled = false;
        this.enabled = false;
        GetComponent<AttackCharacter>().enabled = false;
        GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
    }
}
