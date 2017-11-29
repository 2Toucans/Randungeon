using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeManager : MonoBehaviour
{
    public static MazeManager manager;
    public static bool[,] mazeData;
    public static int exitIndex;
    public static float pXExit, pZExit;
    public Maze mazePrefab;
    private Maze myMaze;
    
    private void Reset()
    {
        myMaze.Reset();
        Destroy(myMaze.gameObject);
        mazeData = null;
        exitIndex = -1;
        myMaze = Instantiate(mazePrefab) as Maze;
    }
    
	// Use this for initialization
	void Awake()
    {
        if (manager == null)
        {
            DontDestroyOnLoad(gameObject);

            manager = this;
        }
        myMaze = Instantiate(mazePrefab) as Maze;
    }
	
	// Update is called once per frame
	void Update()
    {
        if (Input.GetButtonDown("ResetMaze"))
            Reset();
    }

    public void SetMaze(bool[,] m)
    {
        mazeData = m;
    }

    public void SetExit(int e)
    {
        exitIndex = e;
    }

    public void SetExitCoords(float x, float z)
    {
        pXExit = x;
        pZExit = z;
    }

    public bool[,] GetMazeData()
    {
        return mazeData;
    }

    public int GetExit()
    {
        return exitIndex;
    }

    public float GetExitX()
    {
        return pXExit;
    }

    public float GetExitZ()
    {
        return pZExit;
    }
}
