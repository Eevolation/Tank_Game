using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Xp : MonoBehaviour
{
    public int XP;
    public int Currentlevel;

    // Update is called once per frame
    void Update()
    {
        UpdateXp(5);
    }

    public void UpdateXp(int xp)
    {
        XP += xp;
        float damage = PlayerShell.damage;

        int curlvl = (int)(0.1f * Mathf.Sqrt(XP));

        if(curlvl != Currentlevel)
        {
            Currentlevel = curlvl;
            PlayerShell.damage += 10;
            // add some cool text show you reached a new level
        }

        int xpnextlevel = 100 * (Currentlevel + 1) * (Currentlevel + 1);
        int differentxp = xpnextlevel - XP;

        int totaldifference = xpnextlevel - (100 * Currentlevel * Currentlevel);
    }
}
