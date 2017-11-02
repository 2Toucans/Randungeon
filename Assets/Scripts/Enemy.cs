using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed;
    private bool[,] maze;
    private bool moving;
    private int startX, startZ;
    private int destX, destZ;
    private Vector3 velocity;
    private Rigidbody body;
	// Use this for initialization
	void Start()
    {
        body = GetComponent<Rigidbody>();
        moving = false;
	}
	
	/*void FixedUpdate()
    {
        //the maze hasn't given the information yet
        if(maze == null)
            return;

        if(!moving)
        {
            moving = true;

            List<string> validDirs = new List<string>();

            if (startZ - 1 >= 0 && maze[startX, startZ - 1])
                validDirs.Add("up");
            if (startX + 1 < maze.GetLength(0) && maze[startX + 1, startZ])
                validDirs.Add("right");
            if (startZ + 1 < maze.GetLength(1) && maze[startX, startZ + 1])
                validDirs.Add("down");
            if (startX - 1 >= 0 && maze[startX - 1, startZ])
                validDirs.Add("left");

            if (startZ - 1 >= 0)
                Debug.Log("up=" + maze[startX, startZ-1]);
            if (startX + 1 < maze.GetLength(0))
                Debug.Log("right=" + maze[startX+1, startZ]);
            if (startZ + 1 < maze.GetLength(1))
                Debug.Log("down=" + maze[startX, startZ+1]);
            if (startX - 1 >= 0)
                Debug.Log("left=" + maze[startX-1, startZ]);

            string chosenDir = "";
            if(validDirs.Count != 0)
                chosenDir = validDirs[Random.Range(0, validDirs.Count - 1)];

            switch (chosenDir)
            {
                case "up":
                    destX = startX;
                    destZ = startZ - 2;
                    break;
                case "right":
                    destX = startX + 2;
                    destZ = startZ;
                    break;
                case "down":
                    destX = startX;
                    destZ = startZ + 2;
                    break;
                case "left":
                    destX = startX - 2;
                    destZ = startZ;
                    break;
            }

            velocity = new Vector3(destX-startX, 0, destZ-startZ) * speed;
            Debug.Log(velocity);
        }

        body.velocity = velocity;

        Vector3 curPos = new Vector3(transform.position.x, 0, transform.position.z);
        //Vector3 destPos = new Vector3(destX, 0, destZ);
        Vector3 startPos = new Vector3(startX, 0, startZ);
        if((curPos-startPos).magnitude >= 1)
        {
            moving = false;
            body.velocity = Vector3.zero;
            startX = destX;
            startZ = destZ;
            transform.position = new Vector3(destX, transform.position.y, destZ);
        }
    }*/

    public void setMaze(bool[,] maze)
    {
        this.maze = maze;
    }

    public void setPosition(int x, int z)
    {
        startX = x;
        startZ = z;
    }
}
