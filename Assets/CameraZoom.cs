using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraZoom : MonoBehaviour
{
    private float zoom = 5;
    private float minZoom = 2;
    private float maxZoom = 5;

    public Vector2 mousePosition;
    

    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
      // Capture scroll wheel input
        float scroll = Input.mouseScrollDelta.y;

        // Only apply zoom if there's scrolling input
        if (scroll != 0)
        {
            // Get the mouse position in world space before zooming
            Vector3 mousePositionBeforeZoom = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            // Update the zoom level
            zoom -= scroll;
            zoom = Mathf.Clamp(zoom, minZoom, maxZoom); // Clamp zoom between min and max
            Camera.main.orthographicSize = zoom;

            // Get the mouse position in world space after zooming
            Vector3 mousePositionAfterZoom = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            // Calculate the difference in position and move the camera
            Camera.main.transform.position += mousePositionBeforeZoom - mousePositionAfterZoom;

        }

        //om orthagraphic size är på 5 så ha denna positionen
        if (Camera.main.orthographicSize == 5)
        {
            Camera.main.transform.position = new Vector3(0, 0, -10);
        }
    }
}
