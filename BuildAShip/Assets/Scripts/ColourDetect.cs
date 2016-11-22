using UnityEngine;
using System.Collections;

public class ColourDetect : MonoBehaviour
{

	bool showBS = false;

	WebCamTexture WCT;
	Texture2D snapShot;
	Texture2D diffABS;
	Texture2D showCamera;

	WebCamDevice[] cams;


	Color[] pix;
	Color[] pix2;
	int[,] labelMap;

	Color[] labelColors = {
		Color.black,
		Color.red,
		Color.green,
		Color.blue,
		Color.yellow,
		Color.magenta,
		Color.cyan,
		new Color (1, 0.5f, 0),
		new Color (0, 0.5f, 0),
		new Color (0, 0.5f, 1),
		new Color (0, 0.5f, 1)
	};

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
//		diffABS = new Texture2D (1280, 720);					//making it possible for it to be in start
		showCamera = new Texture2D (1280, 720);					//which hoprfully makes the computer process less
		labelMap = new int[WCT.height, WCT.width];
		pix = WCT.GetPixels ();

		snapShot.SetPixels (pix);
		snapShot.Apply ();

		GameObject.Find ("Camera").GetComponent<Renderer> ().material.mainTexture = showCamera;

	}

	// Update is called once per frame
	void Update ()
	{

		if (Input.GetKeyDown (KeyCode.F1)) {
			//			snapShot = new Texture2D (WCT.width, WCT.height);				For some reason they have to be here on mac
			//			diffABS = new Texture2D (WCT.width, WCT.height);				if we do not use specific numbers.
			//			diffThres = new Texture2D (WCT.width, WCT.height);				Maybe it needs some time to find the size
//			pix = WCT.GetPixels ();
//
//			snapShot.SetPixels (pix);
//			snapShot.Apply ();
//
//			GameObject.Find ("Camera").GetComponent<Renderer> ().material.mainTexture = showCamera;
		}


//		if (Input.GetKeyDown (KeyCode.F2)) {
//			showBS = !showBS;
//		}

//		if (showBS) {
			pix = WCT.GetPixels ();
			pix2 = snapShot.GetPixels ();

			for (int y = 0; y < WCT.height; y++) {
				for (int x = 0; x < WCT.width; x++) {
					float sum = pix [y * WCT.width + x].r + pix [y * WCT.width + x].g + pix [y * WCT.width + x].b + 0.000001f;
					pix2 [y * WCT.width + x].r = pix [y * WCT.width + x].r / sum;
					pix2 [y * WCT.width + x].g = pix [y * WCT.width + x].g / sum;
					pix2 [y * WCT.width + x].b = pix [y * WCT.width + x].b / sum;

				}
			}
			showCamera.SetPixels (pix2);
			showCamera.Apply ();	


//			for (int i = 0; i < pix.Length; i++) {
//				if (pix2 [i].r > 50 && pix2 [i].g < 50 && pix2 [i].b < 50) {
//					pix2 [i] = Color.black; 
//				} else {
//					pix2 [i] = Color.white;
//				}
//			}
//
//			showCamera.SetPixels (pix2);
//			showCamera.Apply ();


			// -------------------------- Calculate Area & CenterOfMass
//			xPosOld = xPos;
//			yPosOld = yPos;
//
//			area = 0;
//			xPos = 0;
//			yPos = 0;
//
//			for (int i = 0; i < pix.Length; i++) {
//				if (pix2 [i] == Color.white) {	//can be 1 instead of color.white
//					area++;
//
//					xPos += i % WCT.width; 		// Finds xPos
//					yPos += i / WCT.width;		// Finds yPos
//				}
//			}
//
//			xPos /= area;
//			yPos /= area;
//
//			print ("Area (" + area + ") xPos (" + xPos + ") yPos (" + yPos + ") xVel (" + (xPosOld - xPos) + ") yVel (" + (yPosOld - yPos) + ")");
//
//
//			for (int y = 0; y < WCT.height; y++) {
//				pix2 [y * WCT.width + xPos] = Color.green;
//			}
//			for (int x = 0; x < WCT.width; x++) {
//				pix2 [yPos * WCT.width + x] = Color.green;
//			}
//
//			showCamera.SetPixels (pix2);
//			showCamera.Apply ();
//		}
//
//
//		if (Input.GetKeyDown (KeyCode.F3)) {
//			Label();
//		}
//		}
	}


	void Label ()
	{

		int label = 1;
		for (int y = 1; y < WCT.height - 1; y++) {
			for (int x = 1; x < WCT.width - 1; x++) {
				if (pix2 [y * WCT.width + x] == Color.white) {

					grassfire (y, x, label);
					label++;

				}
			}
		}
		showCamera.SetPixels (pix2);
		showCamera.Apply ();
		pix = showCamera.GetPixels ();
	}

	void grassfire (int y, int x, int label)
	{
		labelMap [y, x] = label;

		pix2 [y * WCT.width + x] = labelColors [label];

		if (y > 1 && pix2 [(y - 1) * WCT.width + x] == Color.white)
			grassfire (y - 1, x, label);
		if (y < WCT.height - 1 && pix2 [(y + 1) * WCT.width + x] == Color.white)
			grassfire (y + 1, x, label);
		if (x > 1 && pix2 [y * WCT.width + (x - 1)] == Color.white)
			grassfire (y, x - 1, label);
		if (x < WCT.width - 1 && pix2 [y * WCT.width + (x + 1)] == Color.white)
			grassfire (y, x + 1, label);
	}

}