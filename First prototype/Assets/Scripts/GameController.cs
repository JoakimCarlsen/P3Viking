using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour
{
	
	public GameObject Log;
	public GameObject Plank;
	public GameObject Group1;

	public GameObject[] parts;
	private int partCount = -1;

	bool plankFire = false;
	bool partPlace = false;
	bool oneCount = false;
	bool oneItem = true;

	bool partIsThere = false;

	public ChopWood chopWoodScript;


	// Use this for initialization
	void Start ()
	{

		chopWoodScript = Group1.GetComponent<ChopWood> ();
	
	}
	
	// Update is called once per frame
	void Update ()
	{

		if (chopWoodScript.hitCount == 3 && oneItem == true) {
//			Log.gameObject.SetActive (false);
			Plank.gameObject.SetActive (true);
			StartCoroutine (plankWait ());
			oneItem = false;
		}
		// move plank to fire
		if (plankFire == true) {
			
			Vector3 newPosition = new Vector3 (-5f, 0.5f, 10f);

			Quaternion newRotation = Quaternion.identity;
			newRotation.eulerAngles = new Vector3 (0f, 0f, 75f);
			Plank.transform.rotation = Quaternion.Lerp (Plank.transform.rotation, newRotation, Time.deltaTime * 0.5f);

			Plank.transform.position = Vector3.Lerp (Plank.transform.position, newPosition, Time.deltaTime * 0.5f);
		}

		if (plankFire == false) {

			Vector3 newPosition = new Vector3 (-20.2f, -0.42f, -3.61f);

			Quaternion newRotation = Quaternion.identity;
			newRotation.eulerAngles = new Vector3 (90f, 0f, 0f);
			Plank.transform.rotation = newRotation;

			Plank.transform.position = newPosition;
		}

		// Move plank to leap motion
		if (partPlace == true) {
			Vector3 newPosition = new Vector3 (20f, -0.5f, -3.61f);

			Quaternion newRotation = Quaternion.identity;
			newRotation.eulerAngles = new Vector3 (0f, 90f, 90f);
			parts [partCount].transform.rotation = Quaternion.Lerp (parts [partCount].transform.rotation, newRotation, Time.deltaTime * 0.5f);

			parts [partCount].transform.position = Vector3.Lerp (parts [partCount].transform.position, newPosition, Time.deltaTime * 0.5f);
		}

		if (partIsThere == true) {
			parts [partCount].SetActive (true);
		}

		print (partCount);
	}

	IEnumerator plankWait ()
	{
		yield return new WaitForSeconds (4);
		plankFire = true;

		yield return new WaitForSeconds (4); // 8
		plankFire = false;
		//Plank.SetActive(false);
		partIsThere = true;


		yield return new WaitForSeconds (1);
		partPlace = true;

		if (partCount <= 9 && oneCount == false){
			partCount += 1;
			oneCount = true;
		}


		yield return new WaitForSeconds (5);
		partPlace = false;

		partIsThere = false;


		chopWoodScript.hitCount = 0;
		oneCount = false;
		oneItem = true;


	}
}
