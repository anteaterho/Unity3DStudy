using UnityEngine;
using System.Collections;

public class CubueControl : MonoBehaviour {

    private Animator mAnimator;

    public enum Status
    {
        Idle,
        Run
    }

    public Status mStatus = Status.Idle;

	// Use this for initialization
	void Start () {

        mAnimator = gameObject.GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
        /*
        float speed = Mathf.Abs(Input.GetAxis("Horizontal"));
        SetStatus(Status.Run, speed);
        Debug.Log("Speed" + speed);
        */
        if(Input.GetKey(KeyCode.Alpha0))
        {
            SetStatus(Status.Idle);
        }
        else if(Input.GetKey(KeyCode.Alpha1))
        {
            SetStatus(Status.Run);
        }
	}

    public void SetStatus(Status status)
    {
        switch (status) {
            case Status.Idle:
                mAnimator.SetInteger("change", 0);
                break;
            case Status.Run:
                mAnimator.SetInteger("change", 1);
                break;
        }
    }
}
