using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeManager : MonoBehaviour
{
    public Maze mazePrefab;
	private Maze myMaze;

    private void Setup()
    {
        myMaze = Instantiate(mazePrefab) as Maze;
    }
    
    private void Reset()
    {
        Destroy(myMaze.gameObject);
        Setup();
    }
    
	// Use this for initialization
	void Start()
    {
		Setup();
	}
	
	// Update is called once per frame
	void Update()
    {
		if (Input.GetKeyDown(KeyCode.Z))
        {
			Reset();
		}
	}
}
