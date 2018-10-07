using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class VacuumAI : MonoBehaviour
{
    bool isClean;//is the room clean
    int layerMask = 1 << 8;// the wall layer mask for the raycasts
    public int speed;//the speed the vacuum moves at
    bool justTurnedLeft, justTurnedRight, outCorner, startedBottomLeft, startedBottomRight;//booleans for vacuum logic
    Vector3 fwd, lft, rig, bck;//the 4 vectors the vacuum checks
    private int floorCount = 0;//the amount of floor tiles
    public int trashPickedUp = 0;//the amount of trash picked up
   
	// Use this for initialization
	void Start ()
    {
        tag = "Vacuum";//set the tag of the vacuum

        justTurnedLeft = false;//initial set ups of the booleans
        justTurnedRight = false;
        outCorner = false;
        isClean = false;

        StartCoroutine(LateStart(.5f));//do this .5 seconds after start
    }

    private IEnumerator LateStart(float v)
    {
        yield return new WaitForSeconds(v);//return control

        if (!Physics.Raycast(transform.position, transform.InverseTransformDirection(Vector3.forward), 10f, layerMask) && Physics.Raycast(transform.position, transform.InverseTransformDirection(Vector3.right), 10f, layerMask) && !Physics.Raycast(transform.position, transform.InverseTransformDirection(Vector3.left), 10f, layerMask))
        {//if vacuum is spawned with NO wall in front, YES wall to the right, NO wall to the left
            startedBottomRight = true;//vacuum was started in the bottom right corner
        }

        else if (!Physics.Raycast(transform.position, transform.InverseTransformDirection(Vector3.forward), 10f, layerMask) && !Physics.Raycast(transform.position, transform.InverseTransformDirection(Vector3.right), 10f, layerMask) && Physics.Raycast(transform.position, transform.InverseTransformDirection(Vector3.left), 10f, layerMask))
        {//if vacuum is spawned with NO wall in front, NO wall to the right, YES wall to the left
            startedBottomLeft = true;//vacuum was started in the bottom left corner
        }
    }

    // Update is called once per frame
    void Update ()
    {
        if(isClean)//if room is clean
        {
            GetComponent<Rigidbody>().velocity = Vector3.zero;//stop all movement
            Time.timeScale = 0;//stop the timer
        }

        fwd = transform.InverseTransformDirection(Vector3.forward); //forward vector is always in front of the vacuum
        lft = transform.InverseTransformDirection(Vector3.left); //left vector is always left of the vacuum
        rig = transform.InverseTransformDirection(Vector3.right); //right vector is always right of the vacuum
        bck = transform.InverseTransformDirection(Vector3.back); //back vector is always behind of the vacuum

        transform.Translate(Vector3.forward * Time.deltaTime * speed);//move the vacuum forward

        if (Physics.Raycast(transform.position, fwd, 5f, layerMask) && Physics.Raycast(transform.position, rig, 5f, layerMask) && !Physics.Raycast(transform.position, lft, 5f, layerMask) && !justTurnedLeft)
        {//if wall in front and right and not left and did not just turn left

                transform.Rotate(0, -90, 0); // turn left
                StartCoroutine(TurnLeft()); //tturn left again to come back
                justTurnedLeft = true; //vacuum just turned left
                justTurnedRight = false; //reset turn right
        }

        else if (Physics.Raycast(transform.position, fwd, 5f, layerMask) && Physics.Raycast(transform.position, lft, 5f, layerMask) && !Physics.Raycast(transform.position, rig, 5f, layerMask) && !justTurnedRight)
        {//if wall in front and left and not right and did not just turn right

                transform.Rotate(0, 90, 0); // turn right
                StartCoroutine(TurnRight()); //turn right again to come back
                justTurnedLeft = false; //reset turn left
                justTurnedRight = true; //vacuum just turned right
        }

        else if (Physics.Raycast(transform.position, fwd, 5f, layerMask) && !Physics.Raycast(transform.position, bck, 5f, layerMask) && !Physics.Raycast(transform.position, lft, 5f, layerMask) && !Physics.Raycast(transform.position,rig, 5f, layerMask))
        {//if wall in front and not back and right and not left
            if (!justTurnedLeft && outCorner)//if vacuum did not just turn left and is out of the corner
            {
                transform.Rotate(0, -90, 0);//turn left
                StartCoroutine(TurnLeft());//turn left again to come back
                justTurnedLeft = true; //vacuum just turned left
                justTurnedRight = false;// reset turn right
            }
            else if(!justTurnedRight && outCorner)//if vacuum did not just turn right and is out of corner
            {
                transform.Rotate(0, 90, 0); // turn right
                StartCoroutine(TurnRight()); //turn right again to come back
                justTurnedLeft = false;//reset turn left
                justTurnedRight = true;//vacuum just turned right
            }
        }
    }

    IEnumerator TurnLeft()
    {
        yield return new WaitForSeconds(.5f);//return control
        transform.Rotate(0f, -90f, 0f); //turn left again
        outCorner = true;//vacuum is out of corner now
    }

    IEnumerator TurnRight()
    {
        yield return new WaitForSeconds(.5f);//return control
        transform.Rotate(0f, 90f, 0f); // turn right again
        outCorner = true;//vacuum is out of corner now
    }

    void OnTriggerEnter(Collider other)//collider interacts with vacuum's collider
    {
        if (other.gameObject.tag == "Trash")// if the item is tagged as trash
        {
            Destroy(other.gameObject);//clean it
            trashPickedUp++;//and increase the pickup count by one
        }

        if (other.gameObject.tag == "Floor")//if the game object is the floor
        {
            floorCount++;//increase the floor count by one
            other.gameObject.tag = "CleanFloor";//mark the gameobject as a clean floor tile
        }

        if(other.gameObject.tag == "Wall")//if the game object is the wal
        {
           isClean = true;//the room is clean becasue the we hit the other wall
        }
    }
}
