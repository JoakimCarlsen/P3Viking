using UnityEngine;
using System.Collections;

public class ratate : MonoBehaviour
{
    public float speed = 8f;


    void Update()
    {
        
        transform.Rotate(Vector3.up, speed * Time.deltaTime);
    }
}