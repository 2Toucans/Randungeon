using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Maze : MonoBehaviour
{
    public int sizeX, sizeZ;
	public GameObject wallPrefab;
	public GameObject groundPrefab;
    public const int SIDES = 4;

    private bool[,] sectionCleared;
	private GameObject[,] tiles;
    private int exitIndex;

	// Use this for initialization
	void Start()
    {
        sectionCleared = new bool[sizeX, sizeZ];
        exitIndex = 1;

        tiles = new GameObject[sizeX + 2, sizeZ + 2];
        
		for(int i = 0; i < sizeX; i++){
            for(int j = 0; j < sizeZ; j++){
                sectionCleared[i,j] = false;
            }
        }
        //Start digging the maze starting from the top left
        clearPath(0,0);
        
        List<int> lastCol = new List<int>();
        for(int i = 0; i < sizeZ-1; i++)
        {
            if(sectionCleared[sizeX-1,i])
                lastCol.Add(i);
        }
        
        exitIndex = lastCol[Random.Range(0, lastCol.Count-1)] + 1;

        for(int i = 0; i < sizeZ+2; i++)
            createTile(0, i);
        for (int i = 0; i < sizeX; i++){
            createTile(i+1, 0);
            for (int j = 0; j < sizeZ; j++){
                createTile(i+1,j+1);
            }
            createTile(i+1, sizeZ+1);
        }
        for (int i = 0; i < sizeZ+2; i++)
            createTile(sizeZ + 1, i);
		
		GameObject ground = Instantiate(groundPrefab);
        ground.transform.Translate(sizeX+1, sizeZ+1, 1);
        ground.transform.localScale += new Vector3(sizeX * 2, sizeZ * 2, 0);
    }
	
	// Update is called once per frame
	void Update()
    {
		
	}

    private void createTile(int row, int column)
    {
        if (((row == 0 || column == 0 || row == sizeZ + 1 || column == sizeX + 1)
            || !sectionCleared[column - 1, row - 1]) && !(column == sizeX + 1 && row == exitIndex))
        {
            tiles[column, row] = Instantiate(wallPrefab);
            tiles[column, row].transform.Translate(column * 2, 0, row * 2);
        }
    }

    private int getAdj(int column, int row)
    {
        int adj = 0;
        
        if(column >= 1 && sectionCleared[column-1,row])
            adj++;
        if(column < sizeX-1 && sectionCleared[column+1,row])
            adj++;
        if(row >= 1 && sectionCleared[column,row-1])
            adj++;
        if(row < sizeZ-1 && sectionCleared[column,row+1])
            adj++;
        
        return adj;
    }

    /**
     * Clears and visits a piece of path then picks a random adjacent section
     * and recursively calls the function on that section of path.
     * 
     * @param row The row of the current path
     * @param column The column of the current path
     */
    private void clearPath(int column, int row) {
        int randDir, temp;
        int max = SIDES;
        if(column < 0 || column >= sizeX || row < 0 || row >= sizeZ ||
                getAdj(column, row) > 1 || sectionCleared[column,row])
            return;
            
        sectionCleared[column,row] = true;
        
        if(column == sizeX-1)
            return;
            
        int[] choices = new int[SIDES];
        for(int i = 0; i < SIDES; i++)
            choices[i] = i;
        for(int j = 0; j < SIDES; j++){
            randDir = Random.Range(0, max--);
            switch(choices[randDir]){
                case 0: clearPath(column-1,row);
                        break;
                case 1: clearPath(column,row-1);
                        break;
                case 2: clearPath(column+1,row);
                        break;
                case 3: clearPath(column,row+1);
                        break;
            }
            temp = choices[randDir];
            choices[randDir] = choices[max];
            choices[max] = temp;
        }
    }
}
