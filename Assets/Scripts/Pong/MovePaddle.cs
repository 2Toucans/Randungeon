using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePaddle : MonoBehaviour
{
    public float speed = 30;
    public string axis = "Vertical";

    // Use this for initialization
    void Start()
    {
		
	}
	
	// Update is called once per frame
	void Update()
    {
        float v = Input.GetAxisRaw(axis);
        GetComponent<Rigidbody>().velocity = new Vector3(0, v, 0) * speed;
    }
}
