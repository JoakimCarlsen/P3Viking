using UnityEngine;
using System.Collections;

public class ColourDetect : MonoBehaviour
{

	bool showBS = false;

	WebCamTexture WCT;
	Texture2D snapShot;
	Texture2D diffABS;
	Texture2D camera;

	WebCamDevice[] cams;


	Color[] pix;
	Color[] pix2;

	int area;
	int xPos;
	int yPos;
	int xPosOld;
	int yPosOld;

	void Start ()
	{
		cams = WebCamTexture.devices;
		print ("Cameras available : " + cams);
		WCT = new WebCamTexture ();
		GetComponent<Renderer> ().material.mainTexture = WCT;
		WCT.Play ();
		snapShot = new Texture2D (1280, 720);					//Same as comment below, but with exact numbers
		diffABS = new Texture2D (1280, 720);					//making it possible for it to be in start
		camera = new Texture2D (1280, 720);					//which hoprfully makes the computer process less

	}

	// Update is called once per frame
	void Update ()
	{

		if (Input.GetKeyDown (KeyCode.F1)) {
			//			snapShot = new Texture2D (WCT.width, WCT.height);				For some reason they have to be here on mac
			//			diffABS = new Texture2D (WCT.width, WCT.height);				if we do not use specific numbers.
			//			diffThres = new Texture2D (WCT.width, WCT.height);				Maybe it needs some time to find the size
			pix = WCT.GetPixels ();

			snapShot.SetPixels (pix);
			snapShot.Apply ();

			GameObject.Find ("Camera").GetComponent<Renderer> ().material.mainTexture = camera;
		}


		if (Input.GetKeyDown (KeyCode.F2)) {
			showBS = !showBS;
		}

		if (showBS) {
			pix = WCT.GetPixels ();
			pix2 = snapShot.GetPixels ();

			for (int i = 0; i < pix.Length; i++) {
				pix2 [i].r = Mathf.Abs (pix [i].r - pix2 [i].r);
				pix2 [i].g = Mathf.Abs (pix [i].g - pix2 [i].g);
				pix2 [i].b = Mathf.Abs (pix [i].b - pix2 [i].b);
			}

			diffABS.SetPixels (pix2);
			diffABS.Apply ();


			for (int i = 0; i < pix.Length; i++) {
				if (pix2 [i].r + pix2 [i].g + pix2 [i].b < 0.3f) {
					pix2 [i] = Color.black; 
				} else {
					pix2 [i] = Color.white;
				}
			}

			camera.SetPixels (pix2);
			camera.Apply ();


			// -------------------------- Calculate Area & CenterOfMass
			xPosOld = xPos;
			yPosOld = yPos;

			area = 0;
			xPos = 0;
			yPos = 0;

			for (int i = 0; i < pix.Length; i++) {
				if (pix2 [i] == Color.white) {	//can be 1 instead of color.white
					area++;

					xPos += i % WCT.width; 		// Finds xPos
					yPos += i / WCT.width;		// Finds yPos
				}
			}

			xPos /= area;
			yPos /= area;

			print ("Area (" + area + ") xPos (" + xPos + ") yPos (" + yPos + ") xVel (" + (xPosOld - xPos) + ") yVel (" + (yPosOld - yPos) + ")");


		}
		for (int y = 0; y < WCT.height; y++) {
			pix2 [y * WCT.width + xPos] = Color.green;
		}
		for (int x = 0; x < WCT.width; x++) {
			pix2 [yPos * WCT.width + x] = Color.green;
		}

		camera.SetPixels (pix2);
		camera.Apply ();
	}

}