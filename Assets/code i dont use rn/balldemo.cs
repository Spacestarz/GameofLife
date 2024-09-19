using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallDemo : MonoBehaviour
{
    //HAR EJ GJORT DENNA
    //LÄRAREN LA DENNA PÅ GITHUB VILLE SE HUR DEN VA
    Ball[] myBall; //Array of balls

    // Start is called before the first frame update
    void Start()
    {
        //Create our array.
        myBall = new Ball[100];

        //Create a blank texture
        var texture = new Texture2D(32, 32);

        //Repeat 10 times
        for (int i = 0; i < 10; i++)
        {
            //Create a Unity game object that we can use for art
            var newObj = new GameObject();

            //Set our game object as parent to the new object
            newObj.transform.parent = transform;

            //Add a sprite to the new object.
            newObj.AddComponent<SpriteRenderer>().sprite = 
                Sprite.Create(texture, new Rect(0.0f, 0.0f, texture.width, texture.height), new Vector2(0.5f, 0.5f));

            //Use the new keyword to create our ball object.
            myBall[i] = new Ball(Random.Range(-5, 5), Random.Range(-5, 5), newObj.transform);
        }
    }

    // Update is called once per frame
    void Update()
    {
        //For each of our balls update it's position
        for (int i = 0; i < 10; i++)
        {
            myBall[i].UpdatePos();
        }
    }
}


class Ball
{
    //Our class variables
    public Vector2 position; //Ball position
    Vector2 velocity; //Ball direction
    Transform transform;

    //Ball Constructor, called when we type new Ball(x, y);
    public Ball(float x, float y, Transform artHolderGameObject)
    {
        //Set our position when we create the code.
        position = new Vector2(x, y);

        transform = artHolderGameObject;

        //Create the velocity vector and give it a random direction.
        velocity = Random.insideUnitCircle * 5;
    }

    //Update our ball
    public void UpdatePos()
    {
        //Here we can add bounce or screen wrap for all balls at once. 

        //Update position
        position += velocity * Time.deltaTime;

        //Send new position to the art game object.
        transform.position = position;
    }
}

