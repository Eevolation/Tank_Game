using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShell : MonoBehaviour
{

    // The time in seconds before the shell is removed
    public float MaxLifeTime = 2f;
    // The amount of damage done if the oxplosion is centereed on the tank
    public float MaxDamage = 2f;
    // The maximum distance away from the explosion tanks can be and are still affected
    public float ExplosionRadius = 5;
    // The amount of force added to the tank at the center of the explosion
    public float ExplosionForce = 100f;
    public static float damage;

    // Refernece to the particles taht will play on explosion
    public ParticleSystem ExplosionParticles;

    // Start is called before the first frame update
    private void Start()
    {
        // If it isn't destroyed by then, destroy the shell after it's lifetime
        Destroy(gameObject, MaxLifeTime);
    }

    private void OnCollisionEnter(Collision other)
    {
        // Find the rigidbody of the collision object
        Rigidbody targetRigidbody = other.gameObject.GetComponent<Rigidbody>();

        // only tanks will have rigidbody scripts
        if (targetRigidbody != null)
        {
            // Add an explosion force
            targetRigidbody.AddExplosionForce(ExplosionForce, transform.position, ExplosionRadius);

            // find the TankHealth script associated with the rigidbody
            TankHealth targetHealth = targetRigidbody.GetComponent<TankHealth>();

            if (targetHealth != null)
            {
                // Deal this damage to the tank
                targetHealth.TakeDamage(damage);
            }
        }

        // Unparent the particles from the shell
        ExplosionParticles.transform.parent = null;

        // Play the particle system
        ExplosionParticles.Play();

        // Once the particles have finished, destroy the gameObject they are on
        Destroy(ExplosionParticles.gameObject, ExplosionParticles.main.duration);

        // Destroy the shell
        Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
