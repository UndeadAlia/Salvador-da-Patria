using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    [Header("Enemy Info")]
    //enemy Name
    public string enemyName;
    //target player
    Transform player;
    //rigidbody2D
    Rigidbody2D rb;
    //animator
    private Animator anim;
    //instance
    public static EnemyManager instance;

    //enemy movement
    public float speed = 2.5f;

    private Vector2 moveDirection;

    //public bool isChasing;

    [Header("Enemy Movement")]

    //wanderwalk
    public bool isMoving;
    public bool canMove;


    public float walkTime;
    public float waitTime;


    public float walkCounter;
    public float waitCounter;

    private int walkDirection;

    public Collider2D wanderZone;
    private bool hasWalkZone;

    //public GameObject wanderArea;

    public Transform birthplace;

    public Collider2D enemyArea;

    private Vector2 minAreaPoint, maxAreaPoint;



    private Vector2 minWalkPoint, maxWalkPoint;


    [Header("Enemy Stats")]
    //enemy health & exp system
    public int currentHP;
    public int maxHP;
    public int expToGive;


    [Header("Enemy Attack")]

    //enemy vision & attack range
    public float visionRange = 3f;
    public float attackRange = 1f;
    public int damage;

    //enemy attack


    public float attackTime;
    public bool isAttacking;
    public float attackTimeCounter;

    [Header("Enemy Damage Effects")]

    //public Transform hitPoint;

    public GameObject damageBurst;
    public GameObject damageNumber;

    [Header("Enemy Quests")]
    public string questToComplete;
    public string questToStart;

    public int rewardGold;



    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        currentHP = maxHP;
        anim = GetComponentInChildren<Animator>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        rb = GetComponent<Rigidbody2D>();

        //birthplace.transform = wanderArea.transform;
        //wanderwalk
        if (wanderZone != null)
        {

            minWalkPoint = wanderZone.bounds.min;
            maxWalkPoint = wanderZone.bounds.max;
            hasWalkZone = true;
        }

        if (enemyArea != null)
        {
            minAreaPoint = enemyArea.bounds.min;
            maxAreaPoint = enemyArea.bounds.max;
        }

        waitCounter = waitTime;
        walkCounter = walkTime;



        ///HP
        ///


    }

    
    
    
    
    // Update is called once per frame
    void Update()
    {
                        

        if (currentHP <= 0)
        {
            gameObject.SetActive(false);
            PlayerStats.instance.AddExp(expToGive);


            if (questToStart != "none")
            {
                PlayerController.instance.activeQuest = questToStart;

            }
            
            if (questToComplete != "none")
            {
                QuestManager.instance.MarkQuestComplete(questToComplete);
                GameManager.instance.currentGold += rewardGold;


            }

        }
    
        if (isAttacking)
        {
            anim.SetBool("isAttacking", true);
        }
        else
        {
            anim.SetBool("isAttacking", false);
        }
        if (isMoving)
        {
            anim.SetBool("isMoving", true);
        }
        else
        {
            anim.SetBool("isMoving", false);
        }





        canMove = true;
        if (!canMove)
        {
            rb.velocity = Vector2.zero;
            return;
        }

        //if Playerinrange

        if (Vector2.Distance(player.position, rb.position) <= visionRange)
        {


            LookAtPlayer();


            Vector2 target = new Vector2(player.position.x, player.position.y);
            Vector2 newPos = Vector2.MoveTowards(rb.position, target, speed * Time.fixedDeltaTime);
            rb.MovePosition(newPos);

            isMoving = true;

            if (Vector2.Distance(rb.position, birthplace.position) >= 20)
            {
                Vector2 firstPos = Vector2.MoveTowards(birthplace.position, target, speed * Time.fixedDeltaTime);
                rb.MovePosition(firstPos);
                isMoving = true;


            }
        }


        //if !playerinRange

        if (Vector2.Distance(player.position, rb.position) > visionRange)
        {
            //wankerwalk
            if (isMoving)
            {
                walkCounter -= Time.deltaTime;

                switch (walkDirection)
                {
                    case 0:
                        {
                            rb.velocity = new Vector2(0, speed);
                            if (hasWalkZone && transform.position.y >= maxWalkPoint.y)
                            {
                                isMoving = false;
                            }
                            break;
                        }
                    case 1:
                        {
                            rb.velocity = new Vector2(speed, 0);
                            if (hasWalkZone && transform.position.x >= maxWalkPoint.x)
                            {
                                isMoving = false;
                            }
                            break;
                        }
                    case 2:
                        {
                            rb.velocity = new Vector2(0, -speed);
                            if (hasWalkZone && transform.position.y <= minWalkPoint.y)
                            {
                                isMoving = false;
                            }
                            break;
                        }
                    case 3:
                        {
                            rb.velocity = new Vector2(-speed, 0);
                            if (hasWalkZone && transform.position.x <= minWalkPoint.x)
                            {
                                isMoving = false;
                            }
                            break;
                        }

                }
                if (walkCounter <= 0)
                {
                    isMoving = false;
                    waitCounter = waitTime;
                }

            }
            else
            {
                waitCounter -= Time.deltaTime;

                rb.velocity = Vector2.zero;

                if (waitCounter < 0)
                {
                    ChooseDirection();
                    waitCounter = waitTime;
                }
            }
            if (walkDirection == 1)
            {
                anim.SetFloat("MoveX", 1);
                anim.SetFloat("MoveY", 0);
            }

            if (walkDirection == 2)
            {
                anim.SetFloat("MoveX", 0);
                anim.SetFloat("MoveY", -1);
            }
            if (walkDirection == 3)
            {
                anim.SetFloat("MoveX", -1);
                anim.SetFloat("MoveY", 0);
            }
            if (walkDirection == 0)
            {
                anim.SetFloat("MoveX", 0);
                anim.SetFloat("MoveY", 1);
            }
        }

        
        
        //FACE RIGHT DIRECTION WANDERWALK

        


                //ifplayerinAttackRange

        if (!isAttacking)
        {
                attackTimeCounter = attackTime;

        }

            if (Vector2.Distance(player.position, rb.position) <= attackRange)
            {
                isAttacking = true;
                attackTimeCounter -= Time.deltaTime;
                //cant move

                Vector2 stopPos = new Vector2(rb.position.x, rb.position.y);
                rb.MovePosition(stopPos);

                ///

                rb.velocity = Vector2.zero;
                isMoving = false;
                anim.SetBool("isMoving", false);

                canMove = false;

                if (attackTimeCounter <= 0)
                {

                    PlayerStats.instance.HurtPlayer(damage);
                    attackTimeCounter = attackTime;
                    Instantiate(damageBurst, player.position, player.rotation);

                    var clone = (GameObject)Instantiate(damageNumber, player.position, Quaternion.Euler(Vector3.zero));
                    clone.GetComponent<FloatingNumbers>().damageNumber = damage;
                    clone.transform.position = new Vector2(player.position.x, player.position.y);

                }

                //anim.SetBool("isAttacking", true);



            }
            else
            {

            }



        

        ////////////   
 


    }



    //wanderwalk chooseDirection
    public void ChooseDirection()
    {
        walkDirection = Random.Range(0, 4);
        isMoving = true;
        walkCounter = walkTime;
    }



    //LOOKAT PLAYER
    public void LookAtPlayer()
    {
        if (transform.position.x > player.position.x)
        {
            anim.SetFloat("MoveX", player.position.x - transform.position.x);
        }
        else
        {
            anim.SetFloat("MoveX", 1);

        }


        if (transform.position.y > player.position.y)
        {
            anim.SetFloat("MoveY", player.position.y - transform.position.y);
        }
        else
        {
            anim.SetFloat("MoveY", player.position.y - transform.position.y);

        }



    }



    /////
    ///

    public void HurtEnemy(int damageToGive)
    {
        currentHP -= damageToGive;

    }
}
