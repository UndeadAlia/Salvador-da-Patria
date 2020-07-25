using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestStarter : MonoBehaviour
{


    public string questToStart;
    public string questToComplete;
    public string requiredQuest;
    public bool canActivate;

    public int rewardGold;


    // Start is called before the first frame update
    void Start()
    {
        canActivate = true;

    }

    // Update is called once per frame
    void Update()
    {


        if (QuestManager.instance.CheckIfComplete(requiredQuest) == false)
        {
            canActivate = false;
        }
        else
        {
            canActivate = true;
        }

        if(questToStart == PlayerController.instance.activeQuest)
        {
            canActivate = false;
        }
        
        if (QuestManager.instance.CheckIfComplete(questToStart) == true)
        {
            canActivate = false;
        }

        QuestManager.instance.CheckIfComplete(requiredQuest);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == ("Player"))
        {
            if (QuestManager.instance.CheckIfComplete(questToStart) == false && questToStart != "none" && canActivate)
            {
                PlayerController.instance.activeQuest = questToStart;
            }
            if (QuestManager.instance.CheckIfComplete(questToComplete) == false && questToComplete != "none" && canActivate)
            {
                QuestManager.instance.MarkQuestComplete(questToComplete);
                GameManager.instance.currentGold += rewardGold;


            }

        }
    }
}
