using UnityEngine;
using System.Collections;

public class CameraProjection : MonoBehaviour {

	public Matrix4x4 originalProjection;
	Camera camera;

	// Use this for initialization
	void Start () {
		camera = GetComponent<Camera> ();
	}
	
	// Update is called once per frame
	void Update () {
		Matrix4x4 p = originalProjection;
		p.m01 += Mathf.Sin (Time.time * 1.2F) * 0.1f;
		p.m10 += Mathf.Sin (Time.time * 1.5f) * 0.1f;
		camera.projectionMatrix = p;
	}
}
