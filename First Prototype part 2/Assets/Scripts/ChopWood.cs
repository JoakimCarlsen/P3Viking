using UnityEngine;
using System.Collections;

public class ChopWood : MonoBehaviour {

    public GameObject gameController;
    GameController gameControllerScript;
    public int hitCount;

	// Use this for initialization
	void Start () {
        gameController = GameObject.Find("ScriptManager");
        gameControllerScript = gameController.GetComponent<GameController>();
    }
	
	// Update is called once per frame
	void Update () 	{

        if (gameControllerScript.oneItem == false)
        {
           
                hitCount = 0;
         
        }
          

    }


	void OnTriggerEnter (Collider col) {

		if (col.gameObject.tag.Equals("Log")) {
			hitCount++;
			print ("is hit " + hitCount + " times");
            

		}


	}



}
