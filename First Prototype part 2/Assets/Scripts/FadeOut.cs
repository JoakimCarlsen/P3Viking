using UnityEngine;
using System.Collections;

public class FadeOut : MonoBehaviour
{

    public GameObject obj;
    public bool canFade;
    private Color alphaColor;
    private float timeToFade = 0.3f;

    public void Start()
    {
        canFade = false;
        alphaColor = obj.GetComponent<MeshRenderer>().material.color;
        alphaColor.a = 0;
    }
    public void Update()
    {
        if (canFade)
        {
            obj.GetComponent<MeshRenderer>().material.color = Color.Lerp(obj.GetComponent<MeshRenderer>().material.color, alphaColor, timeToFade * Time.deltaTime);
        }
    }
}