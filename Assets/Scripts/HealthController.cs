using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.UI;

public class HealthController : MonoBehaviour
{
    public Slider healthBar;
    public int maxHealth = 5;
    private int health;

    public DeathMenu deathMenu;
    public GameObject vehicle;


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

            if (health == 3)
            {
                FindObjectOfType<AudioManager>().Play("BeCareful");
            }
            else
            {
                FindObjectOfType<AudioManager>().Play("CarCrash");
            }
            StartCoroutine(DestroyBuildings(collision.gameObject, 2.0f));

            if (health == 0)
            {
                OnDeath();
            }
        }   
    }

    private void OnDeath()
    {
        Debug.Log("End Game Here");
        deathMenu.ToggleDeathMenu();
        vehicle.SetActive(false);
    }

    public IEnumerator DestroyBuildings(GameObject obj, float delay)
    {
        yield return new WaitForSeconds(delay);
        Destroy(obj);
    }

    public void SetMaxHealth (int value)
    {
        healthBar.maxValue = value;
        healthBar.value = value;
    }
    public void UpdateHealth(int value)
    {
        healthBar.value = value;
    }
    
}
