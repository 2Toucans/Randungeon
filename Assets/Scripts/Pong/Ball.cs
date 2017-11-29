using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public float speed = 30;
    private bool moving = false;

	// Use this for initialization
	void Start ()
    {
        
    }
	// Update is called once per frame
	void Update ()
    {
        if(Input.GetKeyDown(KeyCode.Space) && !moving)
        {
            moving = true;
            float initDirection = Random.value;

            if(initDirection > 0.75f)
                GetComponent<Rigidbody>().velocity = new Vector3(1, 0.5f, 0) * speed;
            else if(initDirection > 0.5f)
                GetComponent<Rigidbody>().velocity = new Vector3(-1, 0.5f, 0) * speed;
            else if (initDirection > 0.25f)
                GetComponent<Rigidbody>().velocity = new Vector3(1, -0.5f, 0) * speed;
            else
                GetComponent<Rigidbody>().velocity = new Vector3(-1, -0.5f, 0) * speed;
        }
    }

    public void ResetBall()
    {
        transform.position = new Vector3(0, 0, 14);
        moving = false;
        GetComponent<Rigidbody>().velocity = Vector3.zero;
    }

    public void SpeedUp()
    {
        speed *= 1.5f;
    }
}
