using UnityEngine;
using System;
using System.Collections;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

public class SocketClient : MonoBehaviour {

	// Use this for initialization

	//private const int listenPort = 5005;
	public GameObject hero;
	public GameObject braveguy;

	private float xPos = 10.0f;
    private float xPos2 = 10.0f;

	public int port;
    public int port2;
	Thread receiveThread;
	UdpClient client;

	//info

	public string lastReceivedUDPPacket = "";
	public string allReceivedUDPPackets = "";

	void Start () {
		init(port);
        init(port2);
	}

	/*
	void OnGUI(){
		Rect  rectObj=new Rect (40,10,200,400);
		
		GUIStyle  style  = new GUIStyle ();
		
		style .alignment  = TextAnchor.UpperLeft;
		
		GUI .Box (rectObj,"# UDPReceive\n127.0.0.1 "+port +" #\n"
		          
		          //+ "shell> nc -u 127.0.0.1 : "+port +" \n"
		          
		          + "\nLast Packet: \n"+ lastReceivedUDPPacket
		          
		          //+ "\n\nAll Messages: \n"+allReceivedUDPPackets
		          
		          ,style );

	}*/

	int init(int _port){
		port = _port;

		print ("UPDSend.init()");

		print ("Sending to 127.0.0.1 : " + port);

		receiveThread = new Thread (new ThreadStart(ReceiveData));
		receiveThread.IsBackground = true;
		receiveThread.Start ();
		return port;

	}

	private void ReceiveData(){
		client = new UdpClient (port);
		while (true) {
			try{
				IPEndPoint anyIP = new IPEndPoint(IPAddress.Parse("127.0.0.1"), port);
				byte[] data = client.Receive(ref anyIP);

				string text = Encoding.UTF8.GetString(data);
				print (">> " + text);
				lastReceivedUDPPacket=text;
				allReceivedUDPPackets=allReceivedUDPPackets+text;
                if (port == 5005)
                {
                    xPos = float.Parse(text);
                    xPos *= 0.021818f;
                }
                else if(port == 5004)
                {
                    xPos2 = float.Parse(text);
                    xPos2 *= 0.021818f;
                }
			}catch(Exception e){
				print (e.ToString()+port);
			}
		}
	}

	public string getLatestUDPPacket(){
		allReceivedUDPPackets = "";
		return lastReceivedUDPPacket;
	}
	
	// Update is called once per frame
	void Update () {
		hero.transform.position = new Vector3(xPos-6.0f,-3,0);
		braveguy.transform.position = new Vector3(xPos2-6.0f,-3,0);
	}

	void OnApplicationQuit(){
		if (receiveThread != null) {
			receiveThread.Abort();
			Debug.Log(receiveThread.IsAlive); //must be false
		}
	}
}
