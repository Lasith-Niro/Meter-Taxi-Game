using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.UI;

public class HealthController : MonoBehaviour
{
    public Slider healthBar;
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
            UpdateHealth(health);
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
        healthBar.maxValue = health;
        healthBar.value = health;
    }
    public void UpdateHealth(int health)
    {
        healthBar.value = health;
    }
    
}
