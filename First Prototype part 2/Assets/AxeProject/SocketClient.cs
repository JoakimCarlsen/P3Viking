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
//	public GameObject braveguy;
	int count =1;
	private float xPos;
	private float xPos2;

	private float yPos;
	private float yPos2;

	public int port;
	public int port2;
	float tempangle;

	Vector3 tempVector;
	Quaternion tempRotation;

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
		print("new thread start " + count);
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
					xPos = (xPos / 20) - 0.5f;

					yPos = (yPos / 20) + 1;

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
					xPos2= (xPos / 20)-0.5f;
					yPos2 = (yPos2 / 20) + 1;

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
		
	float tempxPos;
	// Update is called once per frame
	void Update()
	{
		
		float newangle = (yPos-yPos2) *20;
        //float newangle = 15 * ((yPos - yPos2) * 20) - 90;
        //-25, -95
        //newangle = Mathf.Clamp(newangle, 0.5f, 8f);
        //float SetAngle = Mathf.Lerp (tempangle, newangle, Time.deltaTime*2);

        Vector3 newVector = new Vector3 (-21.5f, yPos-1.2f, xPos*12);
		Quaternion newRotation = Quaternion.identity;
		newRotation.eulerAngles = new Vector3 (0f, 0f, 15 * (newangle) - 90);

        print(15*(newangle)-90);
        newangle = Mathf.Clamp(newangle, 0.5f, 8f);

        hero.transform.position = Vector3.Lerp(hero.transform.position, newVector, Time.deltaTime);

		//hero.transform.position = new Vector3(xPos - 10.0f, yPos-4, 0);
		//braveguy.transform.position = new Vector3(xPos2 - 6.0f, yPos2-4, 0);

		//hero.transform.localEulerAngles = new Vector3 (-15 * -SetAngle,0,0);

		hero.transform.rotation = Quaternion.Lerp(hero.transform.rotation , newRotation , Time.deltaTime*4f);

		//hero.transform.rotation = Quaternion.Lerp (hero.transform.rotation, Quaternion.Euler( new Vector3 (0f, 0f, 15 * (newangle) - 90)), Time.deltaTime * 4);

//		print ("X "+xPos);
//		print ("X2 "+xPos2);

		//print ("Y2 "+yPos2);
		//		-15 * newangle + 45
		//hero.transform.rotation =∑ Quaternion.AngleAxis(yPos*30, Vector3.left);
		//		braveguy.transform.rotation = Quaternion.AngleAxis(yPos2*30, Vector3.forward);

		//tempRotation = newRotation;
		//tempVector = newVector;
		tempxPos=xPos;
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