using UnityEngine;
using System.Collections;
using System;

public static class DamageCalculation
{
    public static void DoDamage(GameObject DamageDealer, GameObject Target)
    {
        try
        {
            var tar = Target.GetComponent<Unit>();
            var mak = DamageDealer.GetComponent<Creep>();

            //tar.Life = tar.Life - (mak.Dmg - (mak.Dmg / 100) * tar.Def);
            tar.Life = tar.Life - mak.Dmg;
        }
        catch (Exception)
        {
            throw new Exception("One of the GameObjects aren´t a Unit");
        }
    }
}
