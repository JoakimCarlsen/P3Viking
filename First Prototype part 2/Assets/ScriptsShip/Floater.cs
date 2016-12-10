using UnityEngine;
using System.Collections;

public class Floater : MonoBehaviour {
	public float waterLevel, floatHeight;
	public Vector3 buoyancyCentreOffset;
    public Vector3 buoyancyCentreOffset2;
    public float bounceDamp;
    float count=-0.010f;
	

	void FixedUpdate () {

		Vector3 actionPoint = transform.position + transform.TransformDirection(buoyancyCentreOffset);
        Vector3 actionPoint2 = transform.position + transform.TransformDirection(buoyancyCentreOffset2);

        float forceFactor = 1f - ((actionPoint.y - waterLevel) / floatHeight);
        float forceFactor2 = 1f - ((actionPoint.y - waterLevel) / floatHeight);

        if (forceFactor > 0f)
        {
            Vector3 uplift = -Physics.gravity * (forceFactor - GetComponent<Rigidbody>().velocity.y * bounceDamp);

            Vector3 uplift2 = -Physics.gravity * (forceFactor - GetComponent<Rigidbody>().velocity.y * bounceDamp);
                GetComponent<Rigidbody>().AddForceAtPosition(uplift2, actionPoint);
                
            
            GetComponent<Rigidbody>().AddForceAtPosition(uplift, actionPoint2);
        }
            

        }
	}

