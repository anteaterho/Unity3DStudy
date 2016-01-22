using UnityEngine;
using System.Collections;
using System;
using System.IO;
using System.Net.Sockets;

public class s_TCP : MonoBehaviour 
{
    public string textFieldString;

    public bool socketReady = false;
	
	TcpClient mySocket;
	NetworkStream theStream;
	StreamWriter theWriter;
	StreamReader theReader;
	public String Host = "192.168.203.128";
	public int Port = 13000; 
	
	// Use this for initialization
	void Start() {

    }
	
	// Update is called once per frame
	void Update() {
		
	}
	
	public void setupSocket() {
		try {
			mySocket = new TcpClient(Host, Port);
			theStream = mySocket.GetStream();
			theWriter = new StreamWriter(theStream);
			theReader = new StreamReader(theStream);
			socketReady = true;
		}
		catch (Exception e) {
			Debug.Log("Socket error:" + e);
		}
	}
	
	public void writeSocket(string theLine) {
        setupSocket();
        if (!socketReady)
			return;
        //theWriter.Write(theLine);
        // theWriter.Flush();
        //byte[] bytes = System.Text.Encoding.ASCII.GetBytes(theLine);
        byte[] bytes = System.Text.Encoding.ASCII.GetBytes(theLine);

        theStream.Write(bytes, 0, bytes.Length);
        theWriter.Flush();
        closeSocket();
    }
	
	//스트링으로 보내는것에 문제가 있어서 바이트로 보낸부분을 추가. 
	public void writeSocketByte(byte b) {
		if (!socketReady)
			return;
		theStream.WriteByte(b);
	}
	
	
	
	public String readSocket() {
		if (!socketReady)
			return "";
		if (theStream.DataAvailable)
			return theReader.ReadLine();
		return "";
	}
	
	public void closeSocket() {
		if (!socketReady)
			return;
		theWriter.Close();
		theReader.Close();
		mySocket.Close();
		socketReady = false;
	}
	
	public void maintainConnection(){
		if(!theStream.CanRead) {
			setupSocket();
		}
	}

    /*
    void OnGUI()
    {
        textFieldString = GUI.TextField(new Rect(25, 100, 300, 30), textFieldString);

        if (GUI.Button(new Rect(20, 70, 80, 20), "Send"))
        {
            //byte[] byteValue = Encoding.UTF8.GetBytes(textFieldString);
            writeSocket(textFieldString);
        }
    }
    */
}