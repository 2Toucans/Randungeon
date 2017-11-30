using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBall : MonoBehaviour
{
    public float lifeSpan;

	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        lifeSpan -= Time.deltaTime;

        if (lifeSpan <= 0)
            Destroy(gameObject);
	}

    void OnTriggerEnter(Collider hitInfo)
    {
        if(hitInfo.gameObject.name == "SkeletonPrefab")
        {
            GameObject.Find("MazeManagerPrefab").GetComponent<MazeManager>().ScorePoint();
            Destroy(gameObject);
        }
        else if(hitInfo.gameObject.name == "MazeWallPrefab" || hitInfo.gameObject.name == "GroundPrefab")
        {
            Destroy(gameObject);
        }
    }
}
