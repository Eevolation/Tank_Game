using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{

    public float duration;

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            // destroy the powerup
            Destroy(gameObject);

            // if its the player, double their speed
            TankMovement tankMovement = other.GetComponent<TankMovement>();
            if(tankMovement != null)
            {
                tankMovement.doubleSpeedTimer = duration;
            }
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
