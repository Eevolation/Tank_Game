using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Xp : MonoBehaviour
{
    public int XP;
    public int Currentlevel;
    public float damage = PlayerShell.damage;
    private float maxhealth = PlayerTankHealth.m_StartingHealth;
    private float currenthealth = PlayerTankHealth.m_CurrentHealth;

    // Update is called once per frame
    void Update()
    {
        UpdateXp(5);
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
                PlayerTankHealth.m_StartingHealth += 5;
                PlayerTankHealth.m_CurrentHealth = PlayerTankHealth.m_StartingHealth;
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
