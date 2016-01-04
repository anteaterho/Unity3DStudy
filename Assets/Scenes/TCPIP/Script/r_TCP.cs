using UnityEngine;
using System.Collections;
using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

public class r_TCP : MonoBehaviour
{

    TcpListener server = null;
    String data = null;
    int counter = 0;
    Byte[] bytes = new Byte[256];

    private string connectState = "";
    private string recieved = " ";

    private string sent = " ";

    public string level = "";

    Thread acceptThread;

    private static int count = 0;
    private int index;

    public string levelState = " ";

    public bool level1 = false;
    public bool level2 = false;

    void Awake()
    {

        index = count;
        count++;
        //Debug.Log("awake, " + gameObject.name + ", index is " + index);

        if (index == 0)
            DontDestroyOnLoad(gameObject);
        else
            Destroy(gameObject);

    }

	void OnApplicationQuit()
    {
        if (this.server != null)
        {
            this.server.Stop();
        }

        if (this.acceptThread != null)
        {
            this.acceptThread.Abort();
        }
    }

    void Start()
    {
        Int32 port = 13000;
        IPAddress localAddr = IPAddress.Parse("127.0.0.1");

        // TcpListener server = new TcpListener(port);
        server = new TcpListener(localAddr, port);

        // Start listening for client requests.
        server.Start();

        // Buffer for reading data

        acceptThread = new Thread(new ThreadStart(Accept));
        acceptThread.IsBackground = true;
        acceptThread.Start();

        Debug.Log("Start Server");
        //StartCoroutine("Quit", 10f);
    }

    IEnumerator Quit(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        Application.Quit();
    }

    private void Receive(object obj)
    {
        TcpClient client = obj as TcpClient;
        NetworkStream stream = client.GetStream();

        try
        {
            while (true)
            {
                if (stream.CanRead)
                {
                    // Reads NetworkStream into a byte buffer. 
                    byte[] bytes = new byte[client.ReceiveBufferSize];

                    // Read can return anything from 0 to numBytesToRead.  
                    // This method blocks until at least one byte is read.
                    stream.Read(bytes, 0, (int)client.ReceiveBufferSize);

                    var scene = BitConverter.ToInt32(bytes, 0);

                    if (scene == 1)
                    {
                        Debug.Log("Level1");
                        level1 = true;
                        level2 = false;
                    }
                    else if (scene == 2)
                    {
                        Debug.Log("Level2");
                        level1 = false;
                        level2 = true;
                    }
                }
            }
        }
        catch
        {
            stream.Close();
            client.Close();
        }
    }

    private void Accept()
    {
        while (true)
        {
            connectState = "Waiting for a connection...";

            TcpClient client = server.AcceptTcpClient();

            connectState = "Connected!";

            Thread receiveThread = new Thread(new ParameterizedThreadStart(Receive));
            receiveThread.IsBackground = true;
            receiveThread.Start(client);
        }
    }
    void Update()
    {
        if (level1)
        {
            //Application.LoadLevel(0);
			Debug.Log("Level 1");
        }
        if (level2)
        {
            //Application.LoadLevel(1);
			Debug.Log("Level 2");
        }
    }

    void OnGUI()
    {
        GUI.Label(new Rect(50, 50, 300, 50), "Level: " + level);
        GUI.Label(new Rect(50, 100, 300, 50), connectState);
        GUI.Label(new Rect(50, 150, 300, 50), "Recieve: " + recieved);

		if (level1) {
			levelState = " Level 1";
            Invoke("FlipFlop", 0.1f);
		} else if (level2) {
			levelState = " Level 2";
            Invoke("FlipFlop", 0.1f);
        } else {
			levelState = " Nothing";
		}

        GUI.Label(new Rect(50, 200, 300, 50), "Sent: " + sent + levelState);

        // if(recieved =="The is from Level 1 Button")
        // {
        // Application.LoadLevel(0);
        // }
        // if(recieved =="The is from Level 2 Button")
        // {
        // Application.LoadLevel(1);
        // }


        if (Input.GetKey(KeyCode.Space))
        {
            Application.LoadLevel(1);
        }

        if (Input.GetKey(KeyCode.Z))
        {
            Application.LoadLevel(0);
        }
    }

    void FlipFlop()
    {
        if(level1)
        {
            level1 = false;
        }
        else if(level2)
        {
            level2 = false;
        }
    }

}
