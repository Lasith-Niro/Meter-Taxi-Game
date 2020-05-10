using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathMenu : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        gameObject.SetActive(false);
    }

    // Update is called once per frame

    public void ToggleDeathMenu()
    {
        gameObject.SetActive(true);
        FindObjectOfType<AudioManager>().Stop("Background");
        FindObjectOfType<AudioManager>().Stop("CarDrive");
        FindObjectOfType<AudioManager>().Stop("CarCrash");
        FindObjectOfType<AudioManager>().Play("End");

    }

    public void LoadMenu()
    {
        SceneManager.LoadScene("Menu");
    }

    public void QuitGame()
    {
        Debug.Log("Quit");
        Application.Quit();
    }
}
