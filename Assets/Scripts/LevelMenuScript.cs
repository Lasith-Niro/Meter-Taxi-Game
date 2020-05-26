using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelMenuScript : MonoBehaviour
{
    static public int x,y;
    public void Load_55()
    {
        x = -5;
        y = 5;
        GameObject.Find("GameData").GetComponent<GameData>().SetMap(1);
        SceneManager.LoadScene("Meter_Taxi");
    }
    
    public void Load_66()
    {
        x = -6;
        y = 6;
        GameObject.Find("GameData").GetComponent<GameData>().SetMap(2);
        SceneManager.LoadScene("Meter_Taxi");
    }
    public void Load_77()
    {
        x = -7;
        y = 7;
        GameObject.Find("GameData").GetComponent<GameData>().SetMap(3);
        SceneManager.LoadScene("Meter_Taxi");
    }
}
