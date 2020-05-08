using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleMiniMap : MonoBehaviour
{
    public GameObject miniMap;
    private bool isShow = true;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (isShow)
            {
                Show();
            }
            else
            {
                Hide();
            }
        }
    }

    void Show()
    {
        miniMap.SetActive(true);
        isShow = false;
    }

    void Hide()
    {
        miniMap.SetActive(false);
        isShow = true;
    }
}
