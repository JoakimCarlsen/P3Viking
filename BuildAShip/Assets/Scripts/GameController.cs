using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {
	
	public GameObject Log;
	public GameObject Plank;
	public GameObject Group1;

	public Vector3 plankPos;

	bool plankFree = false;

	public ChopWood chopWoodScript;

	public Transform Target;

	public float smoothTime = 0.3F;
	private Vector3 velocity = Vector3.zero;


	// Use this for initialization
	void Start () {

		chopWoodScript = Group1.GetComponent<ChopWood> ();
	
	}
	
	// Update is called once per frame
	void Update () {

		if (chopWoodScript.hitCount == 5) {
			Log.gameObject.SetActive (false);
			Plank.gameObject.SetActive (true);
			StartCoroutine(plankWait());

		}

		if (plankFree == true){
			Vector3 targetPosition = Target.TransformPoint(new Vector3(8, 1, 10));
			Target.transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);

			//Plank.transform.position = plankPos;

			Quaternion newRotation = Quaternion.identity;
			newRotation.eulerAngles = new Vector3(25f, 90f, 90f);
			Plank.transform.rotation = Quaternion.Lerp(Plank.transform.rotation , newRotation , Time.deltaTime*4f);
		}
	}

	IEnumerator plankWait() {
		yield return new WaitForSeconds(4);
		plankFree = true;
	}
}
