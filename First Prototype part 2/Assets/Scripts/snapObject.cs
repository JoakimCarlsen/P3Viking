using UnityEngine;
using System.Collections;
using Leap.Unity.Interaction;

public class snapObject : MonoBehaviour {
    public GameObject gameController; 
    public GameObject moveObject;
    public GameObject handleObject;
    public Vector3 snapPosition;

    public int partCounter = 0; 
    public string hitObjectName;
    public string handleName;
    public string ownName;
    public string meshName; 
    public bool isHit = false;
    public bool hitShip; 
    Vector3 plankPosition = new Vector3(-20.2f, -0.42f, -6.21f);
    Quaternion newRotation = Quaternion.identity;
    


    // Use this for initialization
    void Start () {
        moveObject = GameObject.Find(ownName);
        gameController = GameObject.Find("ScriptManager");
        
    }
	
	// Update is called once per frame
	void Update () {

        if(handleObject.gameObject.GetComponent<MeshRenderer>().enabled == true) {
            
            handleObject.gameObject.GetComponent<MeshRenderer>().enabled = false; 
        }
        
	}

    void OnTriggerEnter(Collider target)
    {
        if (target.gameObject.tag.Equals(hitObjectName))
        {

            moveObject.transform.position = snapPosition;
            isHit = true;
            if(isHit == true)
            {
                partCounter++; 
                gameController.GetComponent<GameController>().oneItem = true; 
                transform.DetachChildren();
                Destroy(handleObject);
                GameController.ResetPlank = true;


                if(hitShip == true)
                {
                    gameController.GetComponent<GameController>().BuildDone = true; 
                }

            }
            print("It hit");
        }
    }
}
    