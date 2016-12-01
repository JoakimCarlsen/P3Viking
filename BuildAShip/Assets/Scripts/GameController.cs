using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {
	
	public GameObject Log;
	public GameObject Plank;
	public GameObject Group1;

	public ChopWood chopWoodScript;


	// Use this for initialization
	void Start () {

		chopWoodScript = Group1.GetComponent<ChopWood> ();
	
	}
	
	// Update is called once per frame
	void Update () {

		if (chopWoodScript.hitCount == 5) {
			Log.gameObject.SetActive (false);
			Plank.gameObject.SetActive (true);

		}
	}
}
