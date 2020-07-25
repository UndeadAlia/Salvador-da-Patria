using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{

    public string[] questMarkerNames;
    public bool[] questMarkersComplete;
    //public bool[] questActive;

    public static QuestManager instance;

    public string activeQuest;

   


    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        //questActive = new bool[questMarkerNames.Length];

        questMarkersComplete = new bool[questMarkerNames.Length];
        MarkQuestComplete("none");

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.O))
        {
            SaveQuestData();
        }
        if (Input.GetKeyDown(KeyCode.P))
        {
            LoadQuestData();
        }



        /*if(Input.GetKeyDown(KeyCode.Q))
        {
            Debug.Log(CheckIfComplete("Go to the Professor's Tower"));
            //StartQuest("Go to the Professor's Tower");
            MarkQuestComplete("Go to the Castle");
        }*/
    }
    public int GetQuestNumber(string questToFind)
    {
        for(int i = 0; i < questMarkerNames.Length; i++)
        {
            if(questMarkerNames[i] == questToFind)
            {
                return i;

            }
        }
        
        return 0;


    }

    public bool CheckIfComplete(string questToCheck)
    {
        if (GetQuestNumber(questToCheck) != 0)
        {
            return questMarkersComplete[GetQuestNumber(questToCheck)];
        }


        return false;
    }

    public void MarkQuestComplete(string questToMark)
    {
        questMarkersComplete[GetQuestNumber(questToMark)] = true;
        UpdateLocalQuestObjects();

    }

    public void MarkQuestIncomplete(string questToMark)
    {

        questMarkersComplete[GetQuestNumber(questToMark)] = false;
        UpdateLocalQuestObjects();

    }
    /*public void StartQuest(string questToStart)
    {
        for (int i = 0; i < questActive.Length; i++)
        {
            if (questActive[i] == true)
            {
                activeQuest = questToStart;
            }
        }
        
       

    }*/

    public void UpdateLocalQuestObjects()
    {
        QuestObjectActivator[] questObjects = FindObjectsOfType<QuestObjectActivator>();
        if(questObjects.Length > 0)
        {
            for (int i = 0; i < questObjects.Length; i++)
            {
                questObjects[i].CheckIfComplete();
            }
        }
    }


    public void SaveQuestData()
    {
        for(int i = 0; i < questMarkerNames.Length; i++)
        {
            if (questMarkersComplete[i])
            {
                PlayerPrefs.SetInt("QuestMarker_" + questMarkerNames[i], 1);
            }
            else
            {
                PlayerPrefs.SetInt("QuestMarker_" + questMarkerNames[i], 0);
            }
        }
    }

    public void LoadQuestData()
    {
        for (int i = 0; i < questMarkerNames.Length; i++)
        {
            int valueToSet = 0;
            if(PlayerPrefs.HasKey("QuestMarker_" + questMarkerNames[i]))
            {
                valueToSet = PlayerPrefs.GetInt("QuestMarker_" + questMarkerNames[i]);
            }
            if(valueToSet == 0)
            {
                questMarkersComplete[i] = false;
            }
            else
            {
                questMarkersComplete[i] = true;
            }


        }
    }




}
