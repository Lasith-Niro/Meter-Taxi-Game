using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.UI;

public class HealthController : MonoBehaviour
{
    public Slider slider;
    public int maxHealth = 10;
    private int health;
    private void Start()
    {
        SetMaxHealth(maxHealth);
        health = maxHealth;
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "HostileBlock")
        {

            //reduce health
            health -= 1;
            SetHealth(health);
            StartCoroutine(DestroyBuildings(collision.gameObject, 3.0f));

            if (health == 5)
            {
                Debug.Log("End Game Here");
            }
        }   
    }

    public IEnumerator DestroyBuildings(GameObject obj, float delay)
    {
        yield return new WaitForSeconds(delay);
        Destroy(obj);
    }

    public void SetMaxHealth (int health)
    {
        slider.maxValue = health;
        slider.value = health;
    }
    public void SetHealth(int health)
    {
        slider.value = health;
    }
    
}
