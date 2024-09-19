using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Test : MonoBehaviour
{
    GameObject[,] myCell; //array of cells
    public int width = 10;
    public int height = 10;

    public float scalemultiplier = 1.5f;

    //öppna lådan vill göra något. Objecterna ska ej röra på sig. 
    //ska bara kolla om dem ska "synas" eller ej

    // Start is called before the first frame update
    void Start()
    {
        myCell = new GameObject[width, height];

        //Create a blank texture
          var texture = new Texture2D(32, 32);

        //makes a new vector
        Vector2 Bounds = new();

        //fur the camera
        Bounds.x = Camera.main.orthographicSize * Camera.main.aspect * 2;
        Bounds.y = Camera.main.orthographicSize * 2;
        //for the camera
        float Cellx = Bounds.x /width;
        float Celly = Bounds.y /height;

        //starting positions. For the camera
        float CellstartX = Camera.main.transform.position.x - (Bounds.x * 0.5f) + (Cellx * 0.5f);
        float CellstartY = Camera.main.transform.position.y - (Bounds.y * 0.5f) + (Celly * 0.5f);

        //for the cells.
        for (int x = 0; x < width; x++)
        {

            for (int y = 0; y < height; y++)
            {
                //Create a Unity game object that we can use for art
                var newObj = new GameObject("Grid");

                //Set our game object as parent to the new object
                newObj.transform.parent = transform;

                //Add a sprite to the new object.
                newObj.AddComponent<SpriteRenderer>().sprite = Sprite.Create(texture, new Rect(0.0f, 0.0f, texture.width, texture.height), new Vector2(0.5f, 0.5f));

                float scaleX = Cellx / texture.width * scalemultiplier;
                float scaleY = Celly / texture.height * scalemultiplier;

                newObj.transform.localScale = new Vector2(scaleX, scaleY);

                myCell[x, y] = newObj;

                //camera fit :)
                float positionX = CellstartX + (x * Cellx);
                float positionY = CellstartY + (y * Celly);

                //  myCell[x, y].SetActive(false);

                newObj.transform.position = new Vector3(positionX, positionY, 0);
            }

        }
    }

    // Update is called once per frame
    void Update()
    {
       



    }
}
