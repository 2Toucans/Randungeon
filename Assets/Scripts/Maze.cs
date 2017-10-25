using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Maze : MonoBehaviour
{
    public int sizeX, sizeY;
	public MazeTile tilePrefab;
    public MazeTile wallPrefab;
    public const int SIDES = 4;

    private bool[,] sectionCleared;
	private MazeTile[,] tiles;

	// Use this for initialization
	void Start()
    {
        sectionCleared = new bool[sizeX, sizeY];
        tiles = new MazeTile[sizeX + 2, sizeY + 2];
        
		for(int i = 0; i < sizeX; i++){
            for(int j = 0; j < sizeY; j++){
                sectionCleared[i,j] = false;
            }
        }
        //Start digging the maze starting from the top left
        clearPath(0,0);

        for(int i = 0; i < sizeY+2; i++)
            createTile(0, i);
        for (int i = 0; i < sizeX; i++){
            createTile(i+1, 0);
            for (int j = 0; j < sizeY; j++){
                createTile(i+1,j+1);
            }
            createTile(i+1, sizeY+1);
        }
        for (int i = 0; i < sizeY+2; i++)
            createTile(sizeY + 1, i);

    }
	
	// Update is called once per frame
	void Update()
    {
		
	}

    private void createTile(int row, int column)
    {
        if(row > 0 && column > 0 && row < sizeX+1 && column < sizeY+1
            && sectionCleared[row-1, column-1])
            tiles[row, column] = Instantiate(tilePrefab) as MazeTile;
        else
            tiles[row, column] = Instantiate(wallPrefab) as MazeTile;
        tiles[row, column].transform.Translate(row*2, column*2, 0);
    }

    private int getAdj(int row, int column)
    {
        int adj = 0;
        
        if(row >= 1 && sectionCleared[row-1,column])
            adj++;
        if(row < sizeX-1 && sectionCleared[row+1,column])
            adj++;
        if(column >= 1 && sectionCleared[row,column-1])
            adj++;
        if(column < sizeY-1 && sectionCleared[row,column+1])
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
    private void clearPath(int row, int column) {
        int randDir,temp;
        int max = SIDES;
        if(row < 0 || row >= sizeX || column < 0 || column >= sizeY ||
                getAdj(row, column) > 1 || sectionCleared[row,column])
            return;
            
        sectionCleared[row,column] = true;
        
        if(column == sizeY-1)
            return;
            
        int[] choices = new int[SIDES];
        for(int i = 0; i < SIDES; i++)
            choices[i] = i;
        for(int j = 0; j < SIDES; j++){
            randDir = Random.Range(0, max--);
            switch(choices[randDir]){
                case 0: clearPath(row-1,column);
                        break;
                case 1: clearPath(row,column-1);
                        break;
                case 2: clearPath(row+1,column);
                        break;
                case 3: clearPath(row,column+1);
                        break;
            }
            temp = choices[randDir];
            choices[randDir] = choices[max];
            choices[max] = temp;
        }
    }
}
