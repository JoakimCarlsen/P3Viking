using UnityEngine;
using System.Collections;
using Leap.Unity.Interaction;

public class snapObject : MonoBehaviour {
    public GameObject moveObject;
    public GameObject handleObject;
    public Vector3 snapPosition;

    public string hitObjectName;
    public string handleName;
    public string ownName;
    public bool isHit = false;
    


	// Use this for initialization
	void Start () {
        moveObject = GameObject.Find(ownName);
        
    }
	
	// Update is called once per frame
	void Update () {

        
	}

    void OnTriggerEnter(Collider target)
    {
        if (target.gameObject.tag.Equals(hitObjectName))
        {
            moveObject.transform.position = snapPosition;
            isHit = true;
            if(isHit == true)
            {
                
                handleObject = GameObject.Find(handleName);
                transform.DetachChildren();
                Destroy(handleObject);
            }
            print("It hit");
        }
    }
}
    