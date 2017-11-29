using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeManager : MonoBehaviour
{
    public static MazeManager manager;
    public static bool[,] mazeData;
    public static int exitIndex;
    public Maze mazePrefab;
    private Maze myMaze;
    
    private void Reset()
    {
        myMaze.Reset();
        Destroy(myMaze.gameObject);
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
}
