using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed;
    public float closeEnough;
    private GameObject[,] maze;
    private int startX, startZ, xDir, zDir;
    private Vector3 chosenDest;
    private bool started;
	// Use this for initialization
	void Start()
    {

	}
	
	void FixedUpdate()
    {
        //the maze hasn't given the information yet
        if(maze == null)
            return;

        if (!started)
        {
            chooseDestination();
            started = true;
        }

        Vector3 dir = chosenDest - transform.position;

        transform.position += dir * Time.deltaTime * speed;

        if(dir.magnitude <= closeEnough)
        {
            startX = (int)(chosenDest.x / 2);
            startZ = (int)(chosenDest.z / 2);
            chooseDestination();
        }
    }

    private void chooseDestination()
    {
        List<Vector2> validDests = new List<Vector2>();

        if (xDir != 0 && zDir != 0
            && maze[startX + xDir, startZ + zDir] != null
            && maze[startX + xDir, startZ + zDir].GetComponent<WayPoint>() != null)
        {
            //Debug.Log("continue");
            validDests.Add(maze[startX + xDir, startZ + zDir].transform.position);
        }
        else
        {
            //Debug.Log("change direction");
            if (maze[startX, startZ - 1].GetComponent<WayPoint>() != null)
                validDests.Add(new Vector2(0, -1));
            if (maze[startX + 1, startZ] != null
                && maze[startX + 1, startZ].GetComponent<WayPoint>() != null)
                validDests.Add(new Vector2(1, 0));
            if (maze[startX, startZ + 1].GetComponent<WayPoint>() != null)
                validDests.Add(new Vector2(0, 1));
            if (maze[startX - 1, startZ].GetComponent<WayPoint>() != null)
                validDests.Add(new Vector2(-1, 0));
        }

        if (validDests.Count == 0)
            return;

        Vector2 rand = validDests[Random.Range(0, validDests.Count - 1)];
        xDir = (int)rand.x;
        zDir = (int)rand.y;
        chosenDest = maze[startX+xDir, startZ+zDir].transform.position;
        chosenDest.x *= 2;
        chosenDest.z *= 2;

        //Debug.Log("X= " + startX*2 + " Z= " + startZ*2);
        //Debug.Log("xDir= " + xDir + " zDir= " + zDir);
    }

    public void setMaze(GameObject[,] maze)
    {
        this.maze = maze;
    }

    public void setPosition(int x, int z)
    {
        startX = x;
        startZ = z;
    }
}
