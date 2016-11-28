using UnityEngine;
using System;
using System.Collections;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

public class SocketClient : MonoBehaviour
{

	// Use this for initialization

	public GameObject hero;
	public GameObject braveguy;
	public int count = 1;

	private static float xPos = 10.0f;
	private static float xPos2 = 10.0f;

	private static float yPos = 3.0f;
	private static float yPos2 = 3.0f;
	private float angle;
	public int port;
	public int port2;

	Thread receiveThread;
	Thread receiveThread2;
	UdpClient client;
	UdpClient client2;

	//info

	public string lastReceivedUDPPacket = "";
	public string allReceivedUDPPackets = "";

	public string lastReceivedUDPPacket2 = "";
	public string allReceivedUDPPackets2 = "";

	public void Start()
	{

		init(port);
		init(port2);
	}

	void init(int _port)
	{
		//print("UPDSend.init()");

		//print("Sending to 127.0.0.1 : " + _port);

		receiveThread = new Thread(() => ReceiveData(_port));
		receiveThread.IsBackground = true;
		receiveThread.Start();
		//print("new Thread Start" + count + " port " + _port);
		count++;

	}

	public void ReceiveData(int _portX)
	{
		if (_portX == 5005)
		{
			client = new UdpClient(_portX);
			while (true)
			{
				try
				{
					IPEndPoint anyIP = new IPEndPoint(IPAddress.Parse("127.0.0.1"), _portX);

					byte[] data = client.Receive(ref anyIP);

					string text = Encoding.UTF8.GetString(data);
					string[] parsedText = text.Split(',');
					//print(">> port  " + _portX + parsedText[0] + " , " + parsedText[1]);
					lastReceivedUDPPacket = text;
					allReceivedUDPPackets = allReceivedUDPPackets + text;
					xPos = float.Parse(parsedText[0]);
					xPos *= 0.021818f;
					yPos = float.Parse(parsedText[1]);
					yPos *= 0.021818f;
				}
				catch (Exception e)
				{
					print(e.ToString());
				}
			}
		}
		if (_portX == 5004)
		{
			client2 = new UdpClient(_portX);
			while (true)
			{
				try
				{
					IPEndPoint anyIP = new IPEndPoint(IPAddress.Parse("127.0.0.1"), _portX);

					byte[] data = client2.Receive(ref anyIP);

					string text = Encoding.UTF8.GetString(data);
					string[] parsedText = text.Split(',');
					//print(">> port  " + _portX + parsedText[0] + " , " + parsedText[1]);
					lastReceivedUDPPacket2 = text;
					allReceivedUDPPackets2 = allReceivedUDPPackets2 + text;
					xPos2 = float.Parse(parsedText[0]);
					xPos2 *= 0.021818f;
					yPos2 = float.Parse(parsedText[1]);
					yPos2 *= 0.021818f;
				}
				catch (Exception e)
				{
					print(e.ToString());
				}
			}
		}
	}

	public string getLatestUDPPacket()
	{
		allReceivedUDPPackets = "";
		return lastReceivedUDPPacket;
	}

	// Update is called once per frame
	void Update()
	{
		angle = yPos2-yPos;
		hero.transform.position = new Vector3(xPos - 10.0f, yPos-4, 0);
		braveguy.transform.position = new Vector3(xPos2 - 6.0f, yPos2-4, -yPos2);

		hero.transform.localEulerAngles = new Vector3 (-15 * angle,180,90);
		
		//hero.transform.rotation =∑ Quaternion.AngleAxis(yPos*30, Vector3.left);
//		braveguy.transform.rotation = Quaternion.AngleAxis(yPos2*30, Vector3.forward);



	}

	void OnApplicationQuit()
	{
		if (receiveThread != null)
		{
			receiveThread.Abort();
			Debug.Log(receiveThread.IsAlive); //must be false
		}
	}
}