using UnityEngine;
using System.Collections;

public class ratate : MonoBehaviour
{
    public float speed = 8f;
    public float time = 3f;
    public float time2 = 15f;
    public float time3 = 10f;


    public bool side = false;
    public bool forback = false;
    public bool zet = false;

    Quaternion sideRotation = Quaternion.identity;
    Quaternion forwardbackward = Quaternion.identity;
    Quaternion leftright = Quaternion.identity;

    void Update()
    {
        
        time -= Time.deltaTime;
        time2 -= Time.deltaTime;
        time3 -= Time.deltaTime;
        Debug.Log(time);

        //transform.Rotate(Vector3.up, speed * Time.deltaTime);


        //sides
        if (side == false)
        {

            sideRotation.eulerAngles = new Vector3(7f, 0f, 0f);
            transform.rotation = Quaternion.Lerp(transform.rotation, sideRotation, Time.deltaTime * 0.2f);
            if (time <= 0)
            {
                side = true;
                time = 6f;
            }
        }
        if (side == true)
        {
            sideRotation.eulerAngles = new Vector3(-7f, 0f, 0f);
            transform.rotation = Quaternion.Lerp(transform.rotation, sideRotation, Time.deltaTime * 0.2f);
            if (time <= 0)
            {
                side = false;
                time = 6f;
            }
        }

        //forward backward
        if (forback == false)
        {

            forwardbackward.eulerAngles = new Vector3(0, 10f, 0f);
            transform.rotation = Quaternion.Lerp(transform.rotation, forwardbackward, Time.deltaTime * 0.025f);
            if (time2 <= 0)
            {
                forback = true;
                time2 = 30f;
            }
        }
        if (forback == true)
        {
            forwardbackward.eulerAngles = new Vector3(0, 10f, 0f);
            transform.rotation = Quaternion.Lerp(transform.rotation, forwardbackward, Time.deltaTime * 0.025f);
            if (time2 <= 0)
            {
                forback = false;
                time2 = 30f;
            }
        }

        //forward backward
        if (zet == false)
        {

            leftright.eulerAngles = new Vector3(0, 0f, 5f);
            transform.rotation = Quaternion.Lerp(transform.rotation, forwardbackward, Time.deltaTime * 0.1f);
            if (time3 <= 0)
            {
                zet = true;
                time3 = 20f;
            }
        }
        if (zet == true)
        {
            leftright.eulerAngles = new Vector3(0, 0f, -5f);
            transform.rotation = Quaternion.Lerp(transform.rotation, leftright, Time.deltaTime * 0.1f);
            if (time3 <= 0)
            {
                zet = false;
                time3 = 20f;
            }
        }
    }
}