using GRIDCITY;
using System.Collections;
using UnityEngine;

public class SetCountDown : MonoBehaviour
{
    private HostileCityManager HCM;

    public void SetCountdown()
    {
        HCM = GameObject.Find("HostileCityManager").GetComponent<HostileCityManager>();
        HCM.countdownDone = true;
    }
}
