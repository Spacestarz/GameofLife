using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOfLife : MonoBehaviour
{
    float Spawnchance = Slider_Value_Holder.spawnChancePercentage;

    private float Cellsize = 0.05f; // Desired size of each square (width and height)
    GameObject[,] myCells; // Array of cells
    
    public Slider frameSlider;

    int numberOfColumns, numberOfRows;
    private bool alive;

    float CamHeight = Camera.main.orthographicSize * 2f;
    float CamWidth = Camera.main.orthographicSize * Camera.main.aspect * 2f;

    //HEADER for inspector see more clearly etc.
    [Header("Speed of Simulation")]
    //framerate of simulation
    [Range(10, 100)]
    public int Framerate;
    

    // Start is called before the first frame update 
    void Start()
    {
        Debug.Log(Spawnchance);

        // Automatically calculate the number of squares that fit in the camera bounds
        numberOfColumns = (int)Mathf.Floor(CamWidth / Cellsize) + 1;
        numberOfRows = (int)Mathf.Floor(CamHeight / Cellsize);

        myCells = new GameObject[numberOfColumns, numberOfRows];

        frameSlider.value = Framerate; //makes the slider the framerate the value

        InitializeGrid();     
    }

    private void InitializeGrid()
    {
        //Create a blank texture
        var texture = new Texture2D(32, 32);

        //  Calculated pixelsPerUnit to match squareSize
        float pixelsPerUnit = texture.width / Cellsize; //how many pixels fits in 1 unity meter

        for (int x = 0; x < numberOfColumns; x++)
        {
            for (int y = 0; y < numberOfRows; y++)
            {
                // Create a Unity game object
                var newObj = new GameObject("X=" + x + " " + "Y=" + y);

                // Set our game object as parent to the new object
                newObj.transform.parent = transform;

                // Add a SpriteRenderer component to the object
                var spriteRenderer = newObj.AddComponent<SpriteRenderer>();

                // Create the sprite from the texture and set the pixels per unit to control its size
                spriteRenderer.sprite = Sprite.Create(texture, new Rect(0.0f, 0.0f, texture.width, texture.height),
                                                      new Vector2(0.5f, 0.5f), pixelsPerUnit);

                myCells[x, y] = newObj;

                // Calculate the position
                float posX = (x * Cellsize - CamWidth / 2) + Cellsize / 2;
                float posY = (y * Cellsize - CamHeight / 2) + Cellsize / 2;

                newObj.transform.position = new Vector3(posX, posY, 0);

                //Random check to see if it should be alive
                if (Random.Range(0, 100) > Spawnchance)
                {
                    myCells[x, y].SetActive(false);
                }
                else
                {
                    myCells[x, y].SetActive(true);
                }

            }

        }
    }

    void Update()
    {
        Application.targetFrameRate = (int)frameSlider.value; //framerate of simulation Go faster or slower.

        bool[,] nextGeneration = new bool[numberOfColumns, numberOfRows];

        //going through the cells and their coords
        for (int x = 0; x < numberOfColumns; x++)
        {
            for (int y = 0; y < numberOfRows; y++)
            {
                int Alive = CheckNeighbours(x, y);

                //rules for the cells 

                if (myCells[x, y].activeSelf)
                {
                    if (Alive < 2 || Alive > 3)
                    {
                        nextGeneration[x, y] = false; // Cell dies                       
                    }

                    else
                    {
                        nextGeneration[x, y] = true; // Cell lives
                        myCells[x, y].GetComponent<SpriteRenderer>().color = Color.blue; //live longer seems like
                    }

                }
                else
                {
                    if (Alive == 3)
                    {
                        nextGeneration[x, y] = true; // Cell becomes alive
                        myCells[x, y].GetComponent<SpriteRenderer>().color = Color.red; //next gen
                    }

                    else
                        nextGeneration[x, y] = false; // Cell stays dead
                }

            }
        }

        //Update Buffer
        for (int x = 0; x < numberOfColumns; x++)
        {
            for (int y = 0; y < numberOfRows; y++)
            {
                myCells[x, y].SetActive(nextGeneration[x, y]);
            }
        }

    }
    int CheckNeighbours(int x, int y)
    {
        int Alive = 0;
        //checking the neighbours
        for (int NextX = -1; NextX <= 1; NextX++)
        {
            for (int NextY = -1; NextY <= 1; NextY++)
            {
                if (NextX == 0 && NextY == 0) continue; //if its the cell that it check ignore it. 

                //Get the coords for the cells
                int neighborX = x + NextX; //cords for X
                int neighborY = y + NextY; //coords for Y

                if (neighborX >= 0 && neighborX < numberOfColumns && neighborY >= 0 && neighborY < numberOfRows)
                {
                    if (myCells[neighborX, neighborY].activeSelf)
                    {
                        Alive++;
                    }
                }
            }
        }
        return Alive;
    }
    
}


