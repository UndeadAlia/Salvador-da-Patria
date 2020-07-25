 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class PlayerController : MonoBehaviour
{
    public float attackRange;
    public float moveSpeed, attackTime;
    public Vector2 lastMove;
    //public string startPoint;
    public bool canMove;

    //public GameObject gameManager;


    private Vector2 moveInput;
    private Animator animator;
    private Rigidbody2D rb;
    //private SFXManager sfxManager;
    public bool playerMoving, attacking;
    private static bool playerExists;
    public float attackTimeCounter;
    public GameObject damageBurst;
    public GameObject damageNumber;
    public string areaTransitionName;
    //public string startPoint;
    public static PlayerController instance;


    public string activeQuest;
    void Start()

    {
   
        playerMoving = false;



        if (instance == null)
        {
            instance = this;
        }
        else
        {
            if (instance != this)
            {
                Destroy(gameObject);
            }
        }
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        //sfxManager = FindObjectOfType<SFXManager>();

        if (!playerExists)
        {
            playerExists = true;
            DontDestroyOnLoad(transform.gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        canMove = true;

        lastMove = new Vector2(0, -1f);

        

    }

    void FixedUpdate()
    {

        if(playerMoving == true)
        {
            animator.SetBool("PlayerMoving", true);
        }
        else
        {
            animator.SetBool("PlayerMoving", false);

        }


        if (!canMove)
        {
            rb.velocity = Vector2.zero;
            playerMoving = false;
            return;
        }

        if (!attacking)
        {

            ///test
            attackTimeCounter = attackTime;
            ////


            if (Input.GetAxisRaw("Horizontal") != 0)
            {
                rb.velocity = new Vector2(Input.GetAxisRaw("Horizontal") * moveSpeed, rb.velocity.y);
                //transform.Translate(new Vector3(Input.GetAxisRaw("Horizontal") * moveSpeed * Time.deltaTime, 0f, 0f));
                playerMoving = true;
                animator.SetBool("PlayerMoving", true);
                lastMove = new Vector2(Input.GetAxisRaw("Horizontal"), 0f);
            }
            else
            {
                rb.velocity = new Vector2(0f, rb.velocity.y);
                //playerMoving = false;

            }

            if (Input.GetAxisRaw("Vertical") != 0)
            {
                rb.velocity = new Vector2(rb.velocity.x, Input.GetAxisRaw("Vertical") * moveSpeed);
                //transform.Translate(new Vector3(0, Input.GetAxisRaw("Vertical") * moveSpeed * Time.deltaTime, 0f));
                playerMoving = true;
                lastMove = new Vector2(0f, Input.GetAxisRaw("Vertical"));
            }
            else
            {
                rb.velocity = new Vector2(rb.velocity.x, 0f);
                //playerMoving = false;

            }


                if (Input.GetAxisRaw("Vertical") == 0 && Input.GetAxisRaw("Horizontal") == 0)
            {
                playerMoving = false;
            }


                if (Input.GetKeyDown(KeyCode.J))
            {
                
                attacking = true;
                rb.velocity = Vector2.zero;
                animator.SetBool("Attack", true);
                
                //sfxManager.playerAttack.Play();
            }


        }

        if(attacking)
        {
            attackTimeCounter -= Time.deltaTime;
        }

        
        
            if (attackTimeCounter <= 0)
            {
                attacking = false;
                animator.SetBool("Attack", false);
                attackTimeCounter = attackTime;
            }


        

        animator.SetFloat("MoveX", Input.GetAxisRaw("Horizontal"));
        animator.SetFloat("MoveY", Input.GetAxisRaw("Vertical"));
        animator.SetBool("PlayerMoving", playerMoving);
        animator.SetFloat("LastMoveX", lastMove.x);
        animator.SetFloat("LastMoveY", lastMove.y);
    }


}
