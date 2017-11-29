using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreGoal : MonoBehaviour
{
    public HUDManager manager;
	// Use this for initialization
	void Start()
    {
		
	}
	
	// Update is called once per frame
	void Update()
    {
		
	}

    void OnTriggerEnter(Collider hitInfo)
    {
        if (hitInfo.name == "Ball")
        {
            string wallName = transform.name;
            manager.Score(wallName);
            hitInfo.gameObject.SendMessage("ResetBall", 1.0f, SendMessageOptions.RequireReceiver);
        }
    }
}
