﻿using UnityEngine;
using System.Collections;

using System;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading;

public class UDPReceive : MonoBehaviour {

    Thread receiveThread;

    UdpClient client;

    public int port;

    public string lastReceivedUDPPackt = "";
    public string allReceivedUDPPackets = "";

    private static void Main()
    {
        UDPReceive receiveObj = new UDPReceive();
        receiveObj.init();

        string text = "";
        do
        {
            text = Console.ReadLine();
        }
        while (!text.Equals("exit"));
    }

	// Use this for initialization
	void Start () {
        init();
	}

    void OnGUI()
    {
        Rect rectObj = new Rect(40, 10, 200, 400);
        GUIStyle style = new GUIStyle();
        style.alignment = TextAnchor.UpperLeft;
        GUI.Box(rectObj, "# UDPReceive\n127.0.0.1 " + port + " #\n"
                    + "shell> nc -u 127.0.0.1 : " + port + " \n"
                    + "\nLast Packet: \n" + lastReceivedUDPPackt
                    + "\n\nAll Messages: \n" + allReceivedUDPPackets
                , style);
    }

    private void init()
    {
        print("UDPSend.init()");

        port = 8051;

        print("Sending to 127.0.0.1 : " + port);
        print("Test-Sending to this port : nc -u 127.0.0.1" + port + "");

        receiveThread = new Thread(new ThreadStart(ReceiveData));
        receiveThread.IsBackground = true;
        receiveThread.Start();
    }

    private void ReceiveData()
    {
        client = new UdpClient(port);
        while(true)
        {
            try
            {
                IPEndPoint anyIP = new IPEndPoint(IPAddress.Any, 0);
                byte[] data = client.Receive(ref anyIP);

                string text = Encoding.UTF8.GetString(data);

                print(">>" + text);

                lastReceivedUDPPackt = text;

                allReceivedUDPPackets = allReceivedUDPPackets + text;
            }
            catch(Exception err)
            {
                print(err.ToString());
            }
        }
    }

    public string getLatestUDPPacket()
    {
        allReceivedUDPPackets = "";
        return lastReceivedUDPPackt;
    }
}
