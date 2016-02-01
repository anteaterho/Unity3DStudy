using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ObjectPaintingSimple : MonoBehaviour
{

    public float rayLength = 5.0f;
    public int brushRadius;

    public GameObject cursor;

    private bool isSkinedMesh = false;
    private Color color;
    private string checkName;
    private string tempName;

    private bool isPressed = false;

    private string tagName;

    public Texture2D sourceTex;
    public Rect sourceRect;

    //Renderer renderer = new Renderer();

    public Camera cam;
    public GameObject obj;
    Texture2D tex;
    Vector2 pixelUV;

    void Start()
    {
        FillTexture(sourceTex, sourceRect, obj);
    }

    void Update()
    {

        if (Input.GetKeyDown(KeyCode.R))
        {
            color = Color.red;
        }

        else if (Input.GetKeyDown(KeyCode.G))
        {
            color = Color.green;
        }

        else if (Input.GetKeyDown(KeyCode.B))
        {
            color = Color.blue;
        }

        else if (Input.GetKeyDown(KeyCode.K))
        {
            color = Color.black;
        }

        else if (Input.GetKeyDown(KeyCode.W))
        {
            color = Color.white;
        }

        RaycastHit hit = new RaycastHit();

        Vector3 forward = transform.TransformDirection(Vector3.forward) * rayLength;

        Ray ray = cam.ScreenPointToRay(Input.mousePosition);


        if (!Physics.Raycast(ray, out hit))
        {
            return;
        }

        if (hit.collider.GetComponentInChildren<SkinnedMeshRenderer>())
        {
            isSkinedMesh = true;
        }
        else
        {
            isSkinedMesh = false;
        }

        MeshCollider meshcollider = hit.collider as MeshCollider;

        Renderer renderer;

        if (isSkinedMesh)
        {
            SkinnedMeshRenderer skinRenderer = hit.collider.GetComponentInChildren<SkinnedMeshRenderer>();
            renderer = skinRenderer as Renderer;
        }
        else
        {
            renderer = hit.collider.GetComponent<Renderer>();
        }

        tex = renderer.material.mainTexture as Texture2D;

        pixelUV = hit.textureCoord;
        pixelUV.x *= tex.width;
        pixelUV.y *= tex.height;

        if (Input.GetKey(KeyCode.O))
        {
            if (brushRadius < 100)
            {
                brushRadius++;
            }
        }

        if (Input.GetKey(KeyCode.P))
        {
            print("P");
            if (brushRadius > 1)
            {
                brushRadius--;
            }
        }

        if (Input.GetKeyDown(KeyCode.I))
        {
            brushRadius = 2;
            print("I");
        }

#if UNITY_EDITOR
        if (Input.GetMouseButton(0))
        {
            PaintSpray();
        }
        tex.Apply();
#endif

#if UNITY_ANDROID
        if (Input.touchCount == 1)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began)
            {
                PaintSpray();
            }

            if (touch.phase == TouchPhase.Moved)
            {
                PaintSpray();
            }
            tex.Apply();
        }
#endif

        if (Input.GetKey(KeyCode.C))
        {
            FillTexture(sourceTex, sourceRect, obj);
        }

    }

    void FillTexture(Texture2D tex, Rect rect, GameObject obj)
    {
        int x = Mathf.FloorToInt(rect.x);
        int y = Mathf.FloorToInt(rect.y);
        int width = Mathf.FloorToInt(rect.width);
        int height = Mathf.FloorToInt(rect.height);
        Color[] pix = tex.GetPixels(x, y, width, height);
        Texture2D destTex = new Texture2D(width, height);
        destTex.SetPixels(pix);
        destTex.Apply();
        obj.GetComponent<Renderer>().material.mainTexture = destTex;

    }

    void PaintSpray()
    {
        for (int x = 0; x < brushRadius * 2; x++)
        {
            for (int y = 0; y < brushRadius * 2; y++)
            {
                if (brushRadius * brushRadius > ((x - brushRadius) * (x - brushRadius)) + ((y - brushRadius) * (y - brushRadius)))
                {
                    tex.SetPixel((int)pixelUV.x + x - brushRadius, (int)pixelUV.y + y - brushRadius, color);
                }
            }
        }
    }
}
