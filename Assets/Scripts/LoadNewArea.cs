using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class LoadNewArea : MonoBehaviour
{

    public string levelToLoad;
    public string areaTransitionName;
    //public string exitPoint;
    //public AreaEntrance theEntrance;
    public float WaitToLoad = 1f;
    //private PlayerController thePlayer;
    private bool ShouldLoadAfterFade;

    // Start is called before the first frame update
    void Start()
    {
        //AreaEntrance.instance.transitionName = areaTransitionName;
        
    }

    // Update is called once per frame
    void Update()
    {
        if (ShouldLoadAfterFade)
        {
            WaitToLoad -= Time.deltaTime;
            if(WaitToLoad <= 0)
            {
                ShouldLoadAfterFade = false;
                SceneManager.LoadScene(levelToLoad);
                PlayerController.instance.transform.position = AreaEntrance.instance.transform.position;
            }
        }
    }


    void OnTriggerEnter2D(Collider2D other)
    {

        if (other.gameObject.tag == "Player")
        {
            PlayerController.instance.areaTransitionName = areaTransitionName;

            //SceneManager.LoadScene(levelToLoad);
            ShouldLoadAfterFade = true;
            GameManager.instance.fadingBetweenAreas = true;
            UIFade.instance.FadeToBlack();


            
        }

    } 
    
}
