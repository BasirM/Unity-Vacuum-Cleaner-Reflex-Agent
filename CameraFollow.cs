using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public GameObject vacuum;//the vacuum object to follow
    Vector3 offset;//the offset for the camera

    // Use this for initialization
    void Start ()
    {
        StartCoroutine(LateStart(.5f));//do this after .5 seconds after start
    }
	
	// Update is called once per frame
	void Update ()
    {
        transform.position = vacuum.transform.position + offset;//put the camera at the vacuum's position plus the offset
    }

    IEnumerator LateStart(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);//return control back
        vacuum = GameObject.FindGameObjectWithTag("Vacuum"); //find the current spawned vacuum
        offset = new Vector3(0f, vacuum.transform.position.y + 20f, 0f);//set the offset at this position
    }
}
