using UnityEngine;
using System.Collections;

public class Animation : MonoBehaviour {

	Animator SplitWoodAnim;
	public ChopWood chopWoodScript;
    GameController gameControllerScript;
    bool reset = false;

	public GameObject Group1;
    public GameObject gameController;

	int IsHitID;
    int resetID; 

	// Use this for initialization
	void Start () {

		chopWoodScript = Group1.GetComponent<ChopWood> ();
		SplitWoodAnim = GetComponent<Animator> ();
        gameControllerScript = gameController.GetComponent<GameController>();
        
		IsHitID = Animator.StringToHash("IsHitXTimes");
        resetID = Animator.StringToHash("ResetAnim");

    }
	
	// Update is called once per frame
	void Update () {

		if(chopWoodScript.hitCount == 7 && gameControllerScript.oneItem == false){
            
			SplitWoodAnim.SetBool(IsHitID, true);
            SplitWoodAnim.SetBool(resetID, false);
            
            reset = true;
            print(IsHitID + "When animation is going");
            print(chopWoodScript.hitCount + " whAT THE FUCK ");
		}

        if(gameControllerScript.oneItem == true)
        {
            SplitWoodAnim.SetBool(resetID, true);
            SplitWoodAnim.SetBool(IsHitID, false);
            
        }

		// just need the booleans to set the transitions
	}
}
