using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TetrisBlock : MonoBehaviour
{
    public Vector3 rotationPoint;
    private float previousTime;
    //block drop time interval
    private static float fallTime=1.0f;
    public static float FallTime
    { get { return fallTime; } set { fallTime = value; } }
    //create the grid with the size
    public static int height = 23;
    public static int width = 12;
    private static Transform[,] grid = new Transform[width,height];
    //store the pressed key for one block spawn
    private bool isSupport;
    //get and store the current scene's name 
    string currentSceneName;
    //create the instance of needed scripts
    DestroyAudioController dac;
    ParticalController particalControl;
    PointController pointControl;
    MenuController menuControl;

    // Start is called before the first frame update
    void Start()
    {
        Application.targetFrameRate = 300;

        menuControl = GameObject.FindWithTag("Event").GetComponent<MenuController>();

        currentSceneName = SceneManager.GetActiveScene().name;

        //the following scripts are only effective in innovation mode
        if(currentSceneName=="SampleScene")
        {
            dac = GameObject.FindWithTag("Respawn").GetComponent<DestroyAudioController>();
            particalControl = GameObject.FindWithTag("Event").GetComponent<ParticalController>();
            pointControl = GameObject.FindWithTag("Point").GetComponent<PointController>();
            
        }
        

    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.LeftArrow))
        {
            transform.position += new Vector3(-1, 0, 0);
            if(!ValidMove())
            { transform.position -= new Vector3(-1, 0, 0); }
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            transform.position += new Vector3(1, 0, 0);
            if (!ValidMove())
            { transform.position -= new Vector3(1, 0, 0); }
        }
        else if(Input.GetKeyDown(KeyCode.UpArrow))
        {
            //rotate
            transform.RotateAround(transform.TransformPoint(rotationPoint), new Vector3(0, 0, 1), 90);
            if(!ValidMove())
            { transform.RotateAround(transform.TransformPoint(rotationPoint), new Vector3(0, 0, 1), -90); }
        }

        //drop the tetromino every second
        //when the down key is pressed, make the drop time interval 1/10 sec, equavalant to 10 times speed
        //if the incremented deltaTime> drop time interval, change tetromino's position down a row
        if(Time.time-previousTime>(Input.GetKey(KeyCode.DownArrow)?fallTime/10:fallTime))
        {
            transform.position += new Vector3(0, -1, 0);
            if(!ValidMove())
            {
                transform.position -= new Vector3(0, -1, 0);
                AddToGrid();
                CheckForLines();

                //disable this tetromino
                this.enabled = false;

                //check if to generate a new tetromino or to display gameover img
                if (CheckGrid() == false)
                {
                    //check if need to generate the single block
                    if(isSupport==false)
                        FindObjectOfType<SpawnTetromino>().NewTetromino();
                    else
                    {
                        FindObjectOfType<SpawnTetromino>().NewSingleBlock();
                        isSupport = false;
                    }
                }
                    
                else
                {
                    menuControl.EndGame();
                }
                    
            }
            previousTime = Time.time;
        }
        //set the variable to true for the one block drop identification
        if (Input.GetKeyDown(KeyCode.Space) && currentSceneName=="SampleScene")
            isSupport = true;
    }

    void CheckForLines()
    {
        //loop through all rows from up to down in the grid
        for(int i=height-1;i>=0;i--)
        {
            if(HasLine(i))
            {
                //isPlay must come after the row is assigned in ParticalController, otherwise the row will not be send to that class on time, so no row change, no partical will display
                //isPay might be set to false after the line is deleted
                
           
                DeleteLine(i);         
                RowDown(i);

                //show the visual and audio effects in innovation mode
                if(currentSceneName=="SampleScene")
                {
                    pointControl.UpdatePoint();
                    dac.PlayAudio();
                    ParticalController.row = i;
                    particalControl.DisplayPartical();
                }
                
         
            }
            
        }
    }

    //check if has a full filled line
    public static bool HasLine(int i)
    {
        for(int j=0;j<width;j++)
        {
            if(grid[j,i]==null)
            { return false; }
        }
        return true;
    }

    void DeleteLine(int i)
    {
        for(int j=0;j<width;j++)
        {
            Destroy(grid[j, i].gameObject);
            grid[j, i] = null;
        }
       
    }

    void RowDown(int i)
    {
        for(int y=i;y<height;y++)
        {
            for(int x=0;x<width;x++)
            {
                if(grid[x,y]!=null)
                {
                    grid[x, y - 1] = grid[x, y];
                    grid[x, y] = null;
                    grid[x, y - 1].transform.position -= new Vector3(0, 1, 0);

                }
            }
        }
    }



    void AddToGrid()
    {       
        foreach (Transform children in transform)
        {
           //convert the blocks' position into integer and add to array
            int roundedX = Mathf.RoundToInt(children.transform.position.x);
            int roundedY = Mathf.RoundToInt(children.transform.position.y);

            grid[roundedX, roundedY] = children;
        }

    }
    //check if the block is out of array boundary.
    bool CheckGrid()
    {
        foreach(Transform children in transform)
        {
            if (children.position.y >= 20)
                return true;
        }
        return false;
    }
    //restrict the moving location of tetrominoes in the grid
        bool ValidMove()
    {
        foreach(Transform children in transform)
        {
            int roundedX = Mathf.RoundToInt(children.transform.position.x);
            int roundedY = Mathf.RoundToInt(children.transform.position.y);

            if(roundedX<0 || roundedX >=width || roundedY<0 || roundedY>=height)
            {
                return false;
            }
            if(grid[roundedX,roundedY]!=null)
            { return false; }
            
        }
        return true;
    }

  
}
