using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SomeOtherClass : MonoBehaviour {

	// Use this for initialization
	void Start () {
        Dictionary<string, BadGuy> badGuys = new Dictionary<string, BadGuy>();

        BadGuy bg1 = new BadGuy("Harvey", 50);
        BadGuy bg2 = new BadGuy("Magneto", 100);

        badGuys.Add("gangster", bg1);
        badGuys.Add("mutant", bg2);

        BadGuy magneto = badGuys["mutant"];

        BadGuy temp = null;

        if(badGuys.TryGetValue("birds", out temp))
        {
           Debug.Log("OK!");
        }
        else
        {
           Debug.Log("Fail!!");
        }
    }
}
