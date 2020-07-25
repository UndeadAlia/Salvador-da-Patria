using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestObjectActivator : MonoBehaviour
{

    public GameObject objectToTrigger;

    public string questToCheck;

    public bool activeIfComplete;

    private bool initialCheckDone;

    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        if (!initialCheckDone)
        {
            initialCheckDone = true;

            CheckIfComplete();
        }
    }



    public void CheckIfComplete()
    {

        if (QuestManager.instance.CheckIfComplete(questToCheck))
        {
            objectToTrigger.SetActive(activeIfComplete);
        }
    }
}
