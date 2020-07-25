using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaEntrance : MonoBehaviour
{

	public string transitionName;

	public static AreaEntrance instance;


	// Use this for initialization
	void Start()
	{

		instance = this;

		if (transitionName == PlayerController.instance.areaTransitionName)
		{
			PlayerController.instance.transform.position = transform.position;
		}

		UIFade.instance.FadeFromBlack();
		GameManager.instance.fadingBetweenAreas = false;
	}

	// Update is called once per frame
	void Update()
	{

	}
}
