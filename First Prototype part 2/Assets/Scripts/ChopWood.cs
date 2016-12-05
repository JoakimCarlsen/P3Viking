using UnityEngine;
using System.Collections;

public class ChopWood : MonoBehaviour {


	public int hitCount;

	// Use this for initialization
	void Start () {
	 
	}
	
	// Update is called once per frame
	void Update () 	{


	}


	void OnTriggerEnter (Collider col) {

		if (col.gameObject.tag.Equals("Log")) {
			hitCount++;
			print ("is hit " + hitCount + " times");


		}


	}



}
