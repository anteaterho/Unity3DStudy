using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using System.Collections.Generic;
using UnityEngine.UI;

public class ObjectRotation : MonoBehaviour
{


    public float rotationSpeed = 10.0f;
    public float lerpSpeed = 30.0f;
    public Text temptext;
    public Text OutText;
    private Vector3 speed = new Vector3();
    private Vector3 avgSpeed = new Vector3();
    private bool dragging = false;
    private Quaternion rot = Quaternion.identity;

    void Start()
    {
        Input.ResetInputAxes();


        OutText.text = "Wow!!";
    }
    void OnMouseDown()
    {
        dragging = true;
    }


    public Vector2 nowPos, prePos;
    public Vector3 movePos;
    void Update()
    {


        if (Input.touchCount == 1)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began)
            {
                prePos = touch.position - touch.deltaPosition;
            }
            else if (touch.phase == TouchPhase.Moved)
            {
                speed = new Vector3(-Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"), 0);
                temptext.text = speed.x + ":" + speed.y + ":" + speed.z;
                avgSpeed = Vector3.Lerp(avgSpeed, speed, Time.deltaTime * 2);
                transform.Rotate(Camera.main.transform.up * speed.x * rotationSpeed, Space.World);
                transform.Rotate(Camera.main.transform.right * speed.y * rotationSpeed, Space.World);
                rot = transform.rotation;
                OutText.text = "T_T";
                GameObject.Find("UDPControler").GetComponent<UDPManager>().sendString(rot.x + ":" + rot.y + ":" + rot.z + ":" + rot.w);
            }
            else if (touch.phase == TouchPhase.Ended)
            {
                if (dragging)
                {
                    speed = avgSpeed;
                    dragging = false;
                }

                float i = Time.deltaTime * lerpSpeed;
                speed = Vector3.Lerp(speed, Vector3.zero, i);

                //			if(speed == Vector3.zero)
                {
                    Input.ResetInputAxes();

                    OutText.text = "Wow!!";
                }
            }
        }


    }
}
