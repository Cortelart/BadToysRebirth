using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth;
    private int health;

    public int maxArmor;
    private int armor;
    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
        armor = maxArmor;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            DamagePlayer(30);
            Debug.Log("Player han been Damaged!");
        }
    }

    public void DamagePlayer (int damage)
    {
        if(armor > 0)
        {
            if(armor >= damage)
            {
                armor -= damage;
            }
            else if(armor < damage)
            {
                int remainingDamage;

                remainingDamage = damage - armor;

                armor = 0;

                health -= remainingDamage;
            }
        }
        else 
        {
            health -= damage;
        }

        if (health <= 0)
        {
            Debug.Log("Player Died");
        }
    }

    public void GiveHealth (int Amount, GameObject PickUp)
    {
        if(health < maxHealth)
        {
            health += Amount;
            Destroy(PickUp);
        }

        health += Amount;

        if (health > maxHealth)
        {
            health = maxHealth;
        }

    }

    public void GiveArmor(int Amount, GameObject PickUp)
    {
        if(armor < maxArmor)
        {
            armor += Amount;
            Destroy(PickUp);
        }

        armor += Amount;

        if (armor > maxArmor)
        {
            armor = maxArmor;
        }
    }
}
