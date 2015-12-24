using UnityEngine;
using System.Collections;

public class ObjectRotationByMouseDrag : MonoBehaviour {

    public Vector2 clickPos;
    public Vector2 offsetPos;
    public float divider = 80;

	// Use this for initialization
	void Start () {
        clickPos = new Vector2(0, 0);
        offsetPos = new Vector2(0, 0);
	}
	
	// Update is called once per frame
	void Update () {
        offsetPos = new Vector2(0, 0);

        if(Input.GetKeyDown(leftClick()))
        {
            clickPos = MouseXY();
        }

        if(Input.GetKey(leftClick()))
        {
            offsetPos = clickPos - MouseXY();
        }

        this.transform.Rotate(new Vector3(-(offsetPos.y / divider), offsetPos.x / divider, 0.0f), Space.World);
	}

    KeyCode leftClick()
    {
        return KeyCode.Mouse0;
    }

    Vector2 MouseXY()
    {
        Vector2 mouseXY = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        return mouseXY;
    }

    float mouseX()
    {
        return Input.mousePosition.x;
    }

    float mouseY()
    {
        return Input.mousePosition.y;
    }
}
