using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {
	
	public GameObject Log;
	public GameObject Plank;
	public GameObject Group1;

	public Vector3 plankPos;

	bool plankFree = false;

	public ChopWood chopWoodScript;


	// Use this for initialization
	void Start () {

		chopWoodScript = Group1.GetComponent<ChopWood> ();
	
	}
	
	// Update is called once per frame
	void Update () {

		if (chopWoodScript.hitCount == 5) {
//			Log.gameObject.SetActive (false);
			Plank.gameObject.SetActive (true);
			StartCoroutine(plankWait());

		}

		if (plankFree == true){
			

			Plank.transform.position = plankPos;

			Quaternion newRotation = Quaternion.identity;
			newRotation.eulerAngles = new Vector3(0f, 0f, 75f);
			Plank.transform.rotation = Quaternion.Lerp(Plank.transform.rotation , newRotation , Time.deltaTime*4f);
		}
	}

	IEnumerator plankWait() {
		yield return new WaitForSeconds(4);
		plankFree = true;
	}
}
