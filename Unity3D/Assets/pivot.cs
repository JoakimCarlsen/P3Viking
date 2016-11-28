using UnityEngine;
using System.Collections;

public class pivot : MonoBehaviour {
    public float size;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	}

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, size);
    }
}
