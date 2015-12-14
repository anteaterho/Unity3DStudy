using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SomeClass : MonoBehaviour {

	// Use this for initialization
	void Start () {
        List<BadGuy> badGuys = new List<BadGuy>();

        badGuys.Add(new BadGuy("Harvey", 50));
        badGuys.Add(new BadGuy("Magneto", 100));
        badGuys.Add(new BadGuy("Pip", 5));

        badGuys.Sort();

        foreach(BadGuy guy in badGuys)
        {
            print(guy.name + " " + guy.power);
        }

        badGuys.Clear();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
