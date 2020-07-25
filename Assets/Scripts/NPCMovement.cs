using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCMovement : MonoBehaviour
{
    public float moveSpeed;
    private Rigidbody2D myRig;

    public bool isMoving;
    public bool canMove;

    public float walkTime;
    public float waitTime;


    private float walkCounter;
    private float waitCounter;

    private int walkDirection;

    public Collider2D walkZone;
    private bool hasWalkZone;
    public int choosenDirection;



    private Vector2 minWalkPoint, maxWalkPoint;

    private Animator anim;




    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponentInChildren<Animator>();

        myRig = GetComponent<Rigidbody2D>();
        
        
        if (walkZone != null)
        {

            minWalkPoint = walkZone.bounds.min;
            maxWalkPoint = walkZone.bounds.max;
            hasWalkZone = true;
        }


        waitCounter = waitTime;
        walkCounter = walkTime;
    }

    // Update is called once per frame
    void Update()
    {
        if (isMoving)
        {
            anim.SetBool("isMoving", true);
        }
        else
        {
            anim.SetBool("isMoving", false);
        }

        if (GameManager.instance.dialogActive)
        {
            canMove = false;
                }
        
        choosenDirection = walkDirection;

        if (!GameManager.instance.dialogActive)
        {
            canMove = true;
        }

        if (!canMove)
        {
            myRig.velocity = Vector2.zero;
            return;
        }

        if (isMoving)
        {
            walkCounter -= Time.deltaTime;

            switch (walkDirection)
            {
                case 0:
                    {
                        myRig.velocity = new Vector2(0, moveSpeed);
                        if (hasWalkZone && transform.position.y >= maxWalkPoint.y)
                        {
                            isMoving = false;
                        }
                        break;
                    }
                case 1:
                    {
                        myRig.velocity = new Vector2(moveSpeed, 0);
                        if (hasWalkZone && transform.position.x >= maxWalkPoint.x)
                        {
                            isMoving = false;
                        }
                        break;
                    }
                case 2:
                    {
                        myRig.velocity = new Vector2(0, -moveSpeed);
                        if (hasWalkZone && transform.position.y <= minWalkPoint.y)
                        {
                            isMoving = false;
                        }
                        break;
                    }
                case 3:
                    {
                        myRig.velocity = new Vector2(-moveSpeed, 0);
                        if (hasWalkZone && transform.position.x <= minWalkPoint.x)
                        {
                            isMoving = false;
                        }
                        break;
                    }
            }

            if (walkCounter < 0)
            {
                isMoving = false;
                waitCounter = waitTime;
            }
        }
        else
        {
            waitCounter -= Time.deltaTime;

            myRig.velocity = Vector2.zero;

            if (waitCounter < 0)
            {
                ChooseDirection();
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
    }

    public void ChooseDirection()
    {
        walkDirection = Random.Range(0, 4);
        isMoving = true;
        walkCounter = walkTime;
    }

}
