using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {
	
	public GameObject Log;
	public GameObject Plank;
	public GameObject Group1;

	bool plankFire = false;
	bool plankPlace = false;

	public ChopWood chopWoodScript;


	// Use this for initialization
	void Start () {

		chopWoodScript = Group1.GetComponent<ChopWood> ();
	
	}
	
	// Update is called once per frame
	void Update () {

		if (chopWoodScript.hitCount == 2) {
//			Log.gameObject.SetActive (false);
			Plank.gameObject.SetActive (true);
			StartCoroutine(plankWait());

		}

		if (plankFire == true){
			
			Vector3 newPosition = new Vector3(-5f, 0.5f, 10f);

			Quaternion newRotation = Quaternion.identity;
			newRotation.eulerAngles = new Vector3(0f, 0f, 75f);
			Plank.transform.rotation = Quaternion.Lerp(Plank.transform.rotation , newRotation , Time.deltaTime*0.5f);

			Plank.transform.position = Vector3.Lerp(Plank.transform.position , newPosition , Time.deltaTime*0.5f);
		}

		if (plankPlace == true){
			Vector3 newPosition = new Vector3(20f, -0.5f, -3.61f);

			Quaternion newRotation = Quaternion.identity;
			newRotation.eulerAngles = new Vector3(0f, 90f, 100f);
			Plank.transform.rotation = Quaternion.Lerp(Plank.transform.rotation , newRotation , Time.deltaTime*0.5f);

			Plank.transform.position = Vector3.Lerp(Plank.transform.position , newPosition , Time.deltaTime*0.5f);
		}
	}

	IEnumerator plankWait() {
		yield return new WaitForSeconds(4);
		plankFire = true;

		yield return new WaitForSeconds (10);
		plankFire = false;
		plankPlace = true;

	}
}
