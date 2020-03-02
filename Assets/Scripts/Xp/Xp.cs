using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Xp : MonoBehaviour
{
    public int XP;
    public int Currentlevel;
    public float damage = PlayerShell.damage;
    public TankHealth playerHealth;
    public TankHealth Enemy1Health;

    private float maxhealth;
    private float currenthealth;

    private void Start()
    {
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        foreach (GameObject player in players)
        {
            playerHealth = player.GetComponent<TankHealth>();
            if (playerHealth)
            {
                maxhealth = playerHealth.m_StartingHealth;
                currenthealth = playerHealth.m_CurrentHealth;
                return;
            }
        }
        GameObject enemy1 = GameObject.FindGameObjectWithTag("Enemy1");
        if(enemy1)
        {
            Enemy1Health = enemy1.GetComponent<TankHealth>();
            if (Enemy1Health)
            {
                maxhealth = Enemy1Health.m_StartingHealth;
                currenthealth = Enemy1Health.m_CurrentHealth;
                return;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void EnemyXP()
    {
        if (Enemy1Health.m_StartingHealth <= 0)
        {
            UpdateXp(100);
        }
    }

    public void UpdateXp(int xp)
    {
        XP += xp;

        int curlvl = (int)(0.1f * Mathf.Sqrt(XP));

        if(curlvl != Currentlevel)
        {
            Currentlevel = curlvl;
            if(Currentlevel == 2)
            {
                PlayerShell.damage += 5;
            }
            if(Currentlevel == 3)
            {
                playerHealth.m_StartingHealth += 5;
                playerHealth.m_CurrentHealth = playerHealth.m_StartingHealth;
            }
            if (Currentlevel == 4) ;
            {
                PlayerShell.damage += 5;
            }
            // add some cool text show you reached a new level
        }

        int xpnextlevel = 100 * (Currentlevel + 1) * (Currentlevel + 1);
        int differentxp = xpnextlevel - XP;

        int totaldifference = xpnextlevel - (100 * Currentlevel * Currentlevel);
    }
}
