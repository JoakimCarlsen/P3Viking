using UnityEngine;
using System.Collections;

public class Move : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		transform.position += new Vector3 (Input.GetAxis ("Horizontal"), 0, 0);
		//transform.Rotate(Input. ,0,0);
		//transform.Rotate(Time.deltaTime, 0, 0);
	}

	void OnCollisionEnter(){
		//Destroy (gameObject);
	}
}
