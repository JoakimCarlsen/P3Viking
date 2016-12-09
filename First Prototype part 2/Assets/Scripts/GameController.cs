using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour
{

    public GameObject Log;
    public GameObject Plank;
    public GameObject Group1;


    public GameObject[] parts;
    public GameObject[] leapParts;

    private int partCount = -1;
    private int leapCount = -1;

    bool plankFire = false;
    public bool partPlace = false;
    bool oneCount = false;
    bool oneLeapCount = false;
   public bool oneItem = true;

    bool partIsThere = false;
    bool partToLeapPart = false;

    public ChopWood chopWoodScript;


    // Use this for initialization
    void Start()
    {

        chopWoodScript = Group1.GetComponent<ChopWood>();

    }

    // Update is called once per frame
    void Update()
    {
        
        
        if (chopWoodScript.hitCount == 5 && oneItem == true)
        {
            //			Log.gameObject.SetActive (false);
            Plank.gameObject.SetActive(true);
            StartCoroutine(plankWait());
            oneItem = false;
        }
        // move plank to fire
        if (plankFire == true)
        {

            Vector3 newPosition = new Vector3(-1.5f, 0.5f, -10f);

            Quaternion newRotation = Quaternion.identity;
            newRotation.eulerAngles = new Vector3(0f, 0f, 75f);
            Plank.transform.rotation = Quaternion.Lerp(Plank.transform.rotation, newRotation, Time.deltaTime * 0.5f);

            Plank.transform.position = Vector3.Lerp(Plank.transform.position, newPosition, Time.deltaTime * 0.5f);
        }

        if (plankFire == false)
        {

            Vector3 newPosition = new Vector3(-20.2f, -0.42f, -6.21f);

            Quaternion newRotation = Quaternion.identity;
            newRotation.eulerAngles = new Vector3(90f, 0f, 0f);
            Plank.transform.rotation = newRotation;

            Plank.transform.position = newPosition;
            
        }

        // Move plank to leap motion
        if (partPlace == true)
        {
            Plank.gameObject.SetActive(false);
            Vector3 newPosition = new Vector3(20f, -0.5f, -3.61f);

            Quaternion newRotation = Quaternion.identity;
            newRotation.eulerAngles = new Vector3(0f, 90f, 90f);
            parts[partCount].transform.rotation = Quaternion.Lerp(parts[partCount].transform.rotation, newRotation, Time.deltaTime * 0.5f);

            parts[partCount].transform.position = Vector3.Lerp(parts[partCount].transform.position, newPosition, Time.deltaTime * 0.5f);
            
        }

        if (partIsThere == true)
        {
            
             parts[partCount].SetActive(true);
        }

        if (partToLeapPart == true)
        {
            parts[leapCount].SetActive(false);
            leapParts[leapCount].SetActive(true);
        }

        //print ("Part Count : " + partCount);
        print("Leap Count : " + leapCount);
    }

    IEnumerator plankWait()
    {
        yield return new WaitForSeconds(4);
        plankFire = true;

        yield return new WaitForSeconds(8);
        plankFire = false;
        partIsThere = true;


        yield return new WaitForSeconds(1);
        partPlace = true;

        if (partCount <= 9 && oneCount == false)
        {
            partCount += 1;
            oneCount = true;
        }


        yield return new WaitForSeconds(0.2f);
        partPlace = false;

        if (leapCount <= 9 && oneLeapCount == false)
        {
            leapCount += 1;
            oneLeapCount = true;
        }
        partToLeapPart = true;
        partIsThere = false;


       
        oneCount = false;
        //oneItem = true;

        yield return new WaitForSeconds(1);
        oneLeapCount = false;


    }
}
