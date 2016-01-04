using UnityEngine;
using System.Collections;
using System;
using System.IO;
using System.Net.Sockets;

public class s_TCP : MonoBehaviour 
{
	public bool socketReady = false;
	
	TcpClient mySocket;
	NetworkStream theStream;
	StreamWriter theWriter;
	StreamReader theReader;
	String Host = "localhost";
	Int32 Port = 13000; 
	
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
		if (!socketReady)
			return;
		// theWriter.Write(theLine);
		// theWriter.Flush();
		byte[] bytes = System.Text.Encoding.ASCII.GetBytes(theLine);
		theStream.Write(bytes, 0, bytes.Length);
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
}