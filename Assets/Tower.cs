using UnityEngine;
using System.Collections;

public class Tower : Building
{
    public int Dmg;
    public float AttackSpeed;        
    public float AttackRange;

    private GameObject Target;
    // Use this for initialization
    protected override void Start ()
    {
        base.Start();
    }

    // Update is called once per frame
    protected override void Update ()
    {
        base.Update();
    }
}
