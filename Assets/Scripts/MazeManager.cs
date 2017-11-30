using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class MazeManager : MonoBehaviour
{
    static readonly string SAVE_FILE = "save_data.txt";
    public static MazeManager manager;
    public static bool[,] mazeData;
    public static int exitIndex;
    public static float pXPos, pZPos;
    public static int myScore;
    public Maze mazePrefab;
    private Transform player;
    private Maze myMaze;
    private string fileName;

    private void Reset()
    {
        myMaze.Reset();
        Destroy(myMaze.gameObject);
        mazeData = null;
        myScore = 0;
        pXPos = 2;
        pZPos = 2;

        if (File.Exists(fileName))
        {
            File.Delete(fileName);
        }

        myMaze = Instantiate(mazePrefab) as Maze;
        player.Translate(new Vector3(pXPos - 2, 0, pZPos - 2));
    }
    
	// Use this for initialization
	void Awake()
    {
        fileName = Path.Combine(Application.persistentDataPath, SAVE_FILE);
        player = GameObject.Find("PlayerPrefab").transform;
        pXPos = 2;
        pZPos = 2;

        if (manager == null)
        {
            DontDestroyOnLoad(gameObject);

            manager = this;

            Load();
        }
        myMaze = Instantiate(mazePrefab) as Maze;

        player.Translate(new Vector3(pXPos - 2, 0, pZPos - 2));
    }
	
	// Update is called once per frame
	void Update()
    {
        if (Input.GetButtonDown("ResetMaze"))
            Reset();
        if (Input.GetButtonDown("Save") && fileName != null)
            Save();
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
        pXPos = x;
        pZPos = z;
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
        return pXPos;
    }

    public float GetExitZ()
    {
        return pZPos;
    }

    private void Save()
    {
        pXPos = player.position.x;
        pZPos = player.position.z;

        Debug.Log("Saving File");

        if(File.Exists(fileName))
        {
            File.Delete(fileName);
        }

        BinaryFormatter bf = new BinaryFormatter();
        FileStream fs = new FileStream(fileName, FileMode.Append);

        bf.Serialize(fs, myScore);
        bf.Serialize(fs, exitIndex);
        bf.Serialize(fs, pXPos);
        bf.Serialize(fs, pZPos);
        bf.Serialize(fs, mazeData);
    }

    private void Load()
    {
        if (File.Exists(fileName))
        {
            try
            {
                BinaryFormatter bf = new BinaryFormatter();
                FileStream fs = new FileStream(fileName, FileMode.Open);
                myScore = (int)bf.Deserialize(fs);
                exitIndex = (int)bf.Deserialize(fs);
                pXPos = (float)bf.Deserialize(fs);
                pZPos = (float)bf.Deserialize(fs);
                mazeData = (bool[,])bf.Deserialize(fs);
            }
            catch (System.Exception e)
            {
                File.Delete(fileName);
                mazeData = null;
                myScore = 0;
                pXPos = 2;
                pZPos = 2;
            }
        }
        else
        {
            mazeData = null;
            myScore = 0;
            pXPos = 2;
            pZPos = 2;
        }
    }

    public void ScorePoint()
    {
        myScore++;
    }
}
