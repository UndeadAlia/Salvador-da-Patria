using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogEngine : MonoBehaviour
{

    public Text dText, nText;
    public GameObject dBox, nBox;

    public string[] dialogueLines;

    public int currentLine;

    public bool canActivate;

    public string nameText;

    [Header("Quest System")]


    public string questToStart;

    public string questToComplete;

    public string requiredQuest;

    public int rewardGold;



    // Start is called before the first frame update
    void Start()
    {


        dBox.SetActive(false);



        dText.text = dialogueLines[currentLine]; 
        //nameText.Length = dialogueLines
        //nameText = new string[dialogueLines.Length];

        
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerController.instance.activeQuest == questToStart)
        {
            canActivate = false;
        }
        if (QuestManager.instance.CheckIfComplete(requiredQuest) == false && requiredQuest != "none")
        {
            canActivate = false;

        }




        if (Input.GetKeyUp(KeyCode.E) && canActivate)
        {
            //PlayerController.instance.canMove = false;
            GameManager.instance.dialogActive = true;

            dBox.SetActive(true);
            nBox.SetActive(true);

            currentLine++;
            nText.text = nameText;


            if (currentLine >= dialogueLines.Length)
            {
                //PlayerController.instance.canMove = true;
                GameManager.instance.dialogActive = false;
                dBox.SetActive(false);
                nBox.SetActive(false);
                currentLine = 0;


                if (questToStart != null)
                {

                    PlayerController.instance.activeQuest = questToStart;

                }
                if (questToComplete != null)
                {

                    QuestManager.instance.MarkQuestComplete(questToComplete);
                    GameManager.instance.currentGold += rewardGold;

                }




            }
            else
            {
                dText.text = dialogueLines[currentLine];
            }




        }
    }
    void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player" && PlayerController.instance.activeQuest != questToStart && QuestManager.instance.CheckIfComplete(questToStart) == false)
        {
            
                canActivate = true;
            
        }
        if(PlayerController.instance.activeQuest == questToStart)
        {
            canActivate = false;
        }
        if(QuestManager.instance.CheckIfComplete(questToStart) == true)
        {
            canActivate = false;

        }
    }
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            canActivate = false;
        }
    }
}
