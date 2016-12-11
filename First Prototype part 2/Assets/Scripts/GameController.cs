using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameController : MonoBehaviour
{

    public GameObject Log;
    public GameObject Plank;
    public GameObject Group1;
    public GameObject Steam;
    public GameObject sthalf;
    public GameObject ndhalf;
    public GameObject currentlyMesh; 
    public GameObject[] parts;
    public GameObject[] leapParts;
    public GameObject UpperShip;
    public Material[] materialsOutline; 

    public static bool ResetPlank = false;
    private int partCount = -1;
    private int leapCount = -1;
    
    bool plankFire = false;
    public bool partPlace = false;
    bool oneCount = false;
    bool oneLeapCount = false;
   public bool oneItem = true;
    public bool BuildDone = false;
    bool partIsThere = false;
    bool partToLeapPart = false;
    Vector3 UpperShipdown = new Vector3(0.04896355f, 0.8f, -3.5f);

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
            
            StartCoroutine(plankWait());
            oneItem = false;
        }
        // move plank to fire
        if (plankFire == true)
        {
            sthalf.SetActive(false);
            ndhalf.SetActive(false);
            Vector3 newPosition = new Vector3(-1.5f, 0.5f, -15f);

            Quaternion newRotation = Quaternion.identity;
            newRotation.eulerAngles = new Vector3(0f, 0f, 75f);
            Plank.transform.rotation = Quaternion.Lerp(Plank.transform.rotation, newRotation, Time.deltaTime * 0.5f);

            Plank.transform.position = Vector3.Lerp(Plank.transform.position, newPosition, Time.deltaTime * 0.5f);
        }



        if (plankFire == false && oneItem == true)
        {

            Plank.SetActive(true);
            Vector3 newPosition = new Vector3(-20.2f, -0.42f, -6.21f);
            Quaternion newRotation = Quaternion.identity;
            newRotation.eulerAngles = new Vector3(90f, 0f, 0f);
            
            Plank.transform.rotation = newRotation;

            Plank.transform.position = newPosition;
            sthalf.SetActive(true);
            ndhalf.SetActive(true);
        }

        // Move part to leap motion
        if (partPlace == true)
        {
            Plank.gameObject.SetActive(false);
            Vector3 restPosition = new Vector3(11.54f, 0.73f, -9.51f);
            Quaternion restRotation = Quaternion.identity;

            restRotation.eulerAngles = new Vector3(0f, 0f, 0f);
            leapParts[partCount].transform.rotation = Quaternion.Lerp(leapParts[partCount].transform.rotation, restRotation, Time.deltaTime * 1f);
            leapParts[partCount].transform.position = Vector3.Lerp(leapParts[partCount].transform.position, restPosition, Time.deltaTime * 1f);
            
        }

        if (BuildDone == true)
        {
            UpperShip.transform.position = Vector3.Lerp(UpperShip.transform.position, UpperShipdown, Time.deltaTime * 0.5f);
            StartCoroutine(changeShip());
        }

    }


    IEnumerator plankWait()
    {
        yield return new WaitForSeconds(1f);
        plankFire = true;
        yield return new WaitForSeconds(4);
        Steam.SetActive(true);
        
        yield return new WaitForSeconds(3);
        
        plankFire = false;
        Steam.SetActive(false);


        if (partCount <= 9 && oneCount == false)
        {
            partCount += 1;
            oneCount = true;
        }
        leapParts[partCount].SetActive(true);
        leapParts[partCount].GetComponentInChildren<MeshRenderer>().material = materialsOutline[1];

        yield return new WaitForSeconds(0.2f);
        partPlace = true;
        yield return new WaitForSeconds(3f);
        partPlace = false;
        oneCount = false;
        
        
    }

    IEnumerator changeShip()
    {
      

            yield return new WaitForSeconds(10);
            SceneManager.LoadScene(1);
       
    }
}
