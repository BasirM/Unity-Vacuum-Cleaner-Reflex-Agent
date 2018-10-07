using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawns : MonoBehaviour
{
    GameObject[] trashSpawnPoints; //array of trash spawn point
    GameObject[] vacuumSpawnPoints; //array of vacuum spawn points
    GameObject currentVacuumSpawn; //the current used vacuum spawn point
    GameObject currentTrashSpawn; //the current trash spawn point
    int index; //active spawn point in array
    public int trashPeices; //max amount of trash pieces
    public GameObject vacuum; //the vacuum to spawn
    public GameObject trash; //the tash object to spawn

	// Use this for initialization
	void Start ()
    {
        vacuumSpawnPoints = GameObject.FindGameObjectsWithTag("Vacuum Spawn Point");//fill the array with all the vacuum spawn points
        index = Random.Range(0, vacuumSpawnPoints.Length); //makes the active spawn point a random number
        currentVacuumSpawn = vacuumSpawnPoints[index]; //the current vacuum spawn point is a random one in the array

        Vector3 spawnLocation = new Vector3(currentVacuumSpawn.transform.position.x, currentVacuumSpawn.transform.position.y, currentVacuumSpawn.transform.position.z); //hold the location of the current vacuum spawn point

        if (currentVacuumSpawn.name == "Cube (1)" || currentVacuumSpawn.name == "Cube (3)") // if the active spawn point is one of the ones ath the top of the room
        {
            Quaternion rotation_1 = Quaternion.Euler(0, 180, 0); //makes the rotation face backwards and hold the rotation 
            GameObject vacuumClone = Instantiate(vacuum, spawnLocation, rotation_1) as GameObject; //spawn the vacuum at that location and rotation
        }

        else
        {
            Quaternion rotation = Quaternion.identity; //make the roation default and hold it

            GameObject vacuumClone = Instantiate(vacuum, spawnLocation, rotation) as GameObject; //spawn the vacuum at the location and rotation
        }

            trashSpawnPoints = GameObject.FindGameObjectsWithTag("TrashSpawn");//fill the array with all the trash spawn points

        for (int i = 0; i <= trashPeices; ++i)
        {
            index = Random.Range(0, trashSpawnPoints.Length);//makes the active spawn point a random number
            if (trashSpawnPoints[index].activeSelf == false) //if the spawn point was made inactive
            {
                index = Random.Range(0, trashSpawnPoints.Length);//make the active spawn a new random number
            }
            currentTrashSpawn = trashSpawnPoints[index];//the current trash spawn point is a random one in the array
            Vector3 trashLocation = new Vector3(currentTrashSpawn.transform.position.x, currentTrashSpawn.transform.position.y, currentTrashSpawn.transform.position.z);//hold the location of the current trash spawn point
            GameObject trashClone = Instantiate(trash, trashLocation, Quaternion.identity) as GameObject; //spawn the trash at the location and rotation
            trashSpawnPoints[index].SetActive(false);//make that spawn point inactive
        }
    }

    // Update is called once per frame
    void Update ()
    {
		
	}
}
