using UnityEngine;
using System.Collections;

public class Animation : MonoBehaviour {

	Animator SplitWoodAnim;
	public ChopWood chopWoodScript;

	public GameObject Group1;

	int IsHitID;

	// Use this for initialization
	void Start () {

		chopWoodScript = Group1.GetComponent<ChopWood> ();
		SplitWoodAnim = GetComponent<Animator> ();
		IsHitID = Animator.StringToHash("IsHitXTimes");
	
	}
	
	// Update is called once per frame
	void Update () {

		if(chopWoodScript.hitCount == 3){

			SplitWoodAnim.SetBool(IsHitID, true);
		}

		// just need the booleans to set the transitions
	}
}
