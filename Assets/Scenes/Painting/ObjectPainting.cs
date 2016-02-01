using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ObjectPainting : MonoBehaviour
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

    Renderer renderer = new Renderer();

    void Start()
    {

    }

    // Update is called once per frame
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

        Ray ray = new Ray(this.transform.position, transform.forward);


        if (Physics.Raycast(ray, out hit, 100))
        {
            cursor.transform.position = hit.point;
        }

        Debug.DrawRay(transform.position, forward, Color.green);

        if (!Physics.Raycast(this.transform.position, forward, out hit))
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

        if (isSkinedMesh)
        {
            SkinnedMeshRenderer skinRenderer = hit.collider.GetComponentInChildren<SkinnedMeshRenderer>();
            renderer = skinRenderer as Renderer;
        }
        else
        {
            renderer = hit.collider.GetComponent<Renderer>();
        }

        Texture2D tex = renderer.material.mainTexture as Texture2D;

        /*
        if (renderer == null || renderer.sharedMaterial == null ||
            renderer.sharedMaterial.mainTexture == null || meshcollider == null)
        {
            return;
        }
        */



        var pixelUV = hit.textureCoord;
        pixelUV.x *= tex.width;
        pixelUV.y *= tex.height;

        //if(Input.GetKey(KeyCode.Space))
        //{
        //  tex.SetPixel ((int)pixelUV.x, (int)pixelUV.y, color);
        //}

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

        if (Input.GetKey(KeyCode.Space))
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
        tex.Apply();

        //텍스쳐 지우기(흰색으로 칠하기).
        if (Input.GetKey(KeyCode.C))
        {
            int y = 0;
            while (y < tex.height)
            {
                int x = 0;
                while (x < tex.width)
                {
                    Color fill = Color.white;
                    tex.SetPixel(x, y, fill);
                    ++x;
                }
                tex.Apply();
            }
        }

    }

    void FillTexture(Texture2D tex, Rect rect, string name)
    {
        int x = Mathf.FloorToInt(rect.x);
        int y = Mathf.FloorToInt(rect.y);
        int width = Mathf.FloorToInt(rect.width);
        int height = Mathf.FloorToInt(rect.height);
        Color[] pix = tex.GetPixels(x, y, width, height);
        Texture2D destTex = new Texture2D(width, height);
        destTex.SetPixels(pix);
        destTex.Apply();
    }
}
