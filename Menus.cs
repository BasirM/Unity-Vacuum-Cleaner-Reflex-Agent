using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Menus : MonoBehaviour
{
    Scene scene; // reference to scene
    public Text counterText; //the counter text object
    public float seconds, minutes; // seconds and minutes for counter

	// Use this for initialization
	void Start ()
    {
        scene = SceneManager.GetActiveScene(); // fins the active scene
	}
	
	// Update is called once per frame
	void Update ()
    {
        minutes = (int)(Time.timeSinceLevelLoad / 60f); // gives the minutes since the level was loaded
        seconds = (int)(Time.timeSinceLevelLoad % 60f); // gives the seconds since the level was loaded
        counterText.text = minutes.ToString("00") + ":" + seconds.ToString("00"); // dispalys the minutes and seconds
	}

    public void Exit()
    {
        Application.Quit(); //quit the simulation
    }

    public void Reset()
    {
        SceneManager.LoadScene(scene.name); //reload the scene
    }

    public void Pause()
    {
        if (Time.timeScale == 0) // if game is paused
            Time.timeScale = 1; // unpause it
        else
            Time.timeScale = 0; // pause it
    }
}
