using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    public bool isHealth;
    public bool isWeapon;
    public bool isAmmo;
    public bool isArmor;

    public int Amount;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (isHealth)
            {
                other.GetComponent<PlayerHealth>().GiveHealth(Amount, this.gameObject);

            }
            if (isWeapon)
            {

            }
            if (isAmmo)
            {

            }
            if (isArmor)
            {
                other.GetComponent<PlayerHealth>().GiveArmor(Amount, this.gameObject);

            }
            Destroy(gameObject);
        }
    }
}
