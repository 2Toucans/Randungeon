using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed;
    private bool[,] maze;
    private Vector2 startPos;
    private Vector2 destPos;
    private Vector3 startPos3D;
    private Vector3 destPos3D;
    private bool moving;
    private Rigidbody body;
	// Use this for initialization
	void Start()
    {
        body = GetComponent<Rigidbody>();
        moving = false;
	}
	
	/*void FixedUpdate()
    {
        //the maze hasn't given it's information yet
        if(maze == null)
            return;

        //if he has reached the destination tile, find a new tile to go to
        if(!moving)
        {
            moving = true;
            Debug.Log("moving set to true");

            List<string> validDirs = new List<string>();
            if ((startPos.y - 1) > 0 && maze[(int)startPos.x, (int)startPos.y - 1])
                validDirs.Add("up");
            if ((startPos.x + 1) < maze.GetLength(0)-1 && maze[(int)startPos.x + 1, (int)startPos.y])
                validDirs.Add("right");
            if ((startPos.y + 1) < maze.GetLength(1)-1 && maze[(int)startPos.x, (int)startPos.y + 1])
                validDirs.Add("down");
            if ((startPos.x - 1) > 0 && maze[(int)startPos.x - 1, (int)startPos.y])
                validDirs.Add("left");
            
            foreach(string hi in validDirs)
                Debug.Log("validDirs: " + hi);

            string chosenDir = validDirs[Random.Range(0, validDirs.Count - 1)];

            Debug.Log("chosenDir" + chosenDir);

            switch (chosenDir)
            {
                case "up":
                    destPos = startPos + new Vector2(0, -2);
                    break;
                case "right":
                    destPos = startPos + new Vector2(2, 0);
                    break;
                case "down":
                    destPos = startPos + new Vector2(0, 2);
                    break;
                case "left":
                    destPos = startPos + new Vector2(-2, 0);
                    break;
            }

            Debug.Log("destPos = " + destPos);
            Debug.Log("startPos = " + startPos);
            startPos3D = new Vector3(startPos.x, 0, startPos.y);
            destPos3D = new Vector3(destPos.x, 0, destPos.y);

            body.velocity = (destPos3D - startPos3D) * Time.fixedDeltaTime * speed;
            Debug.Log("velocity = " + body.velocity);
        }

        Vector2 transPos = new Vector2(transform.position.x, transform.position.z);
        //when it has moved to or past the destination it stops 
        if ((transPos - startPos).magnitude
            >= (destPos - startPos).magnitude)
        {
            Debug.Log("found destination");
            moving = false;
            startPos = destPos;
            transform.position = destPos3D;
            body.velocity = Vector3.zero;
            
        }
	}*/

    public void setMaze(bool[,] maze)
    {
        this.maze = maze;
    }

    public void setPosition(int x, int z)
    {
        startPos = new Vector2(x, z);
        startPos3D = new Vector3(x, 0, z);
    }
}
