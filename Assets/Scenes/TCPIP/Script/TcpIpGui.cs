using UnityEngine;
using System;
using System.Text;
using System.Net;
using System.Collections;

public class TcpIpGui : MonoBehaviour {

	private string textFieldString;
	private s_TCP myTcp;

	void Awake()
	{
		myTcp = gameObject.AddComponent<s_TCP> ();
        myTcp.setupSocket();
    }

	void OnGUI()
	{
		if (myTcp.socketReady == false) {
			if (GUI.Button (new Rect (20, 10, 80, 20), "Connect")) {
				myTcp.setupSocket ();
			}
		} else {
			myTcp.maintainConnection ();

            /*
			if (GUI.Button (new Rect (20, 40, 80, 20), "Level 1")) {
				//   myTCP.writeSocket("The is from Level 1 Button");
				//myTCP.writeSocket("L1");
				myTcp.writeSocketByte(1);
				textFieldString = myTcp.readSocket ();
			}
			if (GUI.Button (new Rect (20, 70, 80, 20), "Level 2")) {
				//   myTCP.writeSocket("The is from Level 2 Button");
				//myTCP.writeSocket("L2");
				myTcp.writeSocketByte(2);
				textFieldString = myTcp.readSocket ();
			}
			*/

                textFieldString = GUI.TextField (new Rect (25, 100, 300, 30), textFieldString);

            if (GUI.Button(new Rect(20, 70, 80, 20), "Send"))
            {
                //byte[] byteValue = Encoding.UTF8.GetBytes(textFieldString);
                myTcp.writeSocket(textFieldString);
            }

                if (GUI.Button (new Rect (20, 140, 80, 20), "Disconnect")) {
				myTcp.closeSocket ();
				textFieldString = "Socket Disconnected...";
			}
		}
	}

}
