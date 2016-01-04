using UnityEngine;
using System.Collections;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System;

public class UDPManager : MonoBehaviour
{

    public string IP;  // define in init
    public int port;  // define in init

    // "connection" things
    IPEndPoint remoteEndPoint;
    UdpClient client;

    // Use this for initialization
    void Start()
    {
        IP = "192.168.203.45";
        port = 13579;

        remoteEndPoint = new IPEndPoint(IPAddress.Parse(IP), port);
        client = new UdpClient();
        print("Sending to " + IP + " : " + port);
        print("Testing: nc -lu " + IP + " : " + port);
    }


    public void sendString(string message)
    {
        try
        {

            IPEndPoint iep = new IPEndPoint(IPAddress.Parse(IP), port);
            //MessageCoder.encode(msg) is returning a valid byte[], no problem here.
            byte[] data = Encoding.UTF8.GetBytes(message);
            client.Send(data, data.Length, iep);
            //if (message != "")
            //{

            //			// Encode data using the UTF8 encoding to binary format.
            //			byte[] data = Encoding.UTF8.GetBytes(message);
            //			
            //			// Send the message to the remote client.
            //			client.Send(data, data.Length, remoteEndPoint);
            //}
        }
        catch (Exception err)
        {
            print(err.ToString());
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
