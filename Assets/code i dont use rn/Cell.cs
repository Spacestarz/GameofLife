using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell : MonoBehaviour
{
    GameObject[,] myCell; //array of cells
    public int width = 5;
    public int height = 5;
    //öppna lådan vill göra något. Objecterna ska ej röra på sig. 
    //ska bara kolla om dem ska "synas" eller ej

    // Start is called before the first frame update
    void Start()
    {
        myCell = new GameObject[width, height];

        //Create a blank texture
        var texture = new Texture2D(32, 32);

        for (int x = 0; x < width; x++)
        {

            for (int y = 0; y < height; y++)
            {
                //Create a Unity game object that we can use for art
                var newObj = new GameObject("Grid");

                //Set our game object as parent to the new object
                newObj.transform.parent = transform;

                //Add a sprite to the new object.
                newObj.AddComponent<SpriteRenderer>().sprite = Sprite.Create(texture, new Rect(0.0f, 0.0f, texture.width, texture.height), new Vector2(0, 0));

                myCell[x, y] = newObj;


                //  myCell[x, y].SetActive(false);

                newObj.transform.position = new Vector3(x, y, 0);
            }

        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
