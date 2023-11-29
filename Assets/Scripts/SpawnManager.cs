using Oculus.Interaction;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public HandVisual RightHand;
    public HandVisual LeftHand;

    public Transform RightIndexTip;
    public Transform LeftIndexTip;


    public GameObject SpawnObject;

    GameObject ObjectContainer;

    bool flagR = true;
    bool flagL = true;
    bool toggleG = true;
    // Start is called before the first frame update
    void Start()
    {
        ObjectContainer = new GameObject("ObjectContainer");
    }

    // Update is called once per frame
    void Update()
    {
        if (RightHand.Hand.IsTrackedDataValid)
        {
            if (RightHand.Hand.GetIndexFingerIsPinching()&& flagR)
            {
                SpawnRight(RightIndexTip);
                flagR=false;
            }
            else if(!RightHand.Hand.GetIndexFingerIsPinching())
            {
                flagR = true;
            }
        }
        if (LeftHand.Hand.IsTrackedDataValid)
        {
            if (LeftHand.Hand.GetIndexFingerIsPinching() && flagL)
            {
                SpawnLeft(LeftIndexTip);
                flagL=false;
            }
            else if(!LeftHand.Hand.GetIndexFingerIsPinching())
            {
                flagL=true;
            }
        }
    }

    void SpawnRight(Transform spawnPoint)
    {
        GameObject tempObject;
        tempObject = Instantiate(SpawnObject, spawnPoint.position, Quaternion.identity);
        tempObject.GetComponent<MeshRenderer>().material.color = Random.ColorHSV(0,1,1,1,0.5f,1);
        tempObject.transform.SetParent(ObjectContainer.transform);
    }
    void SpawnLeft(Transform spawnPoint)
    {
        GameObject temp;
        temp =Instantiate(SpawnObject, spawnPoint.position, Quaternion.identity);
        temp.GetComponent<MeshRenderer>().material.color = Random.ColorHSV(0, 1, 1, 1, 0.5f, 1);
        temp.transform.SetParent(ObjectContainer.transform);
    }


    public void changePrefab(GameObject prefab)
    {
        SpawnObject = prefab;
    }
    public void Undo()
    {
        Destroy((ObjectContainer.transform.GetChild(ObjectContainer.transform.childCount - 1)).gameObject);
    }
    public void EraseAll()
    {
        Destroy(ObjectContainer);
        ObjectContainer = new GameObject("ObjectContainer");

    }
    public void toggleGravity()
    {
        toggleG = !toggleG;
        for(int i= 0; i<ObjectContainer.transform.childCount; i++)
        {
            ObjectContainer.transform.GetChild(i).GetComponent<Rigidbody>().isKinematic = toggleG;
        }
    }
}
