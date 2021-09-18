using UnityEngine;

public class CameraAspectRatio : MonoBehaviour
{

    float height, width;

    // By  Adrian Lopez (https://gamedesigntheory.blogspot.com/2010/09/controlling-aspect-ratio-in-unity.html)
    // Use this for initialization
    void Start()
    {
        SetCameraSize();
    }

    void Update()
    {
        if (height != (float)Screen.height || width != (float)Screen.width)
        {
            SetCameraSize();
        }
    }

    void SetCameraSize()
    {
        // set the desired aspect ratio (the values in this example are
        // hard-coded for 16:9, but you could make them into public
        // variables instead so you can set them at design time)
        float targetaspect = 1.0f / 1.0f;

        // determine the game window's current aspect ratio
        height = (float)Screen.height;
        width = (float)Screen.width;
        float windowaspect = width / height;

        // current viewport height should be scaled by this amount
        float scaleheight = windowaspect / targetaspect;

        // obtain camera component so we can modify its viewport
        Camera camera = GetComponent<Camera>();

        // if scaled height is less than current height, add letterbox
        if (scaleheight < 1.0f)
        {
            Rect rect = GetComponent<Camera>().rect;

            rect.width = 1.0f;
            rect.height = scaleheight;
            rect.x = 0;
            rect.y = (1.0f - scaleheight) / 2.0f;

            GetComponent<Camera>().rect = rect;
        }
        else // add pillarbox
        {
            float scalewidth = 1.0f / scaleheight;

            Rect rect = GetComponent<Camera>().rect;

            rect.width = scalewidth;
            rect.height = 1.0f;
            rect.x = (1.0f - scalewidth) / 2.0f;
            rect.y = 0;

            GetComponent<Camera>().rect = rect;
        }
    }

}
