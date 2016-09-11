using UnityEngine;
using System.Collections;

public class Kaserne : Building
{
    public GameObject[] SpawnableCreeps;
    public int Creep;
    public bool Stop;

    private float PercentageSpawn;

    public Manager Spawner;
    // Use this for initialization
    protected override void Start()
    {
        base.Start();
        //if (hasAuthority)
        //{
        //    GameObject[] gm = GameObject.FindGameObjectsWithTag("GameController");
        //    for (int i = 0; i < gm.Length; i++)
        //    {
        //        if (gm[i].GetComponent<Manager>().hasAuthority == true)
        //        {
        //            Spawner = gm[i].GetComponent<Manager>();
        //        }
        //    }
        //    Team = Spawner.team;
        //}
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();

        if (Spawner == null)
        {
            if (hasAuthority)
            {
                GameObject[] gm = GameObject.FindGameObjectsWithTag("GameController");
                for (int i = 0; i < gm.Length; i++)
                {
                    if (gm[i].GetComponent<Manager>().hasAuthority == true)
                    {
                        Spawner = gm[i].GetComponent<Manager>();
                    }
                }
                Team = Spawner.team;
            }
        }
        else
        {            
            PercentageSpawn += Time.deltaTime;

            if (PercentageSpawn >= SpawnableCreeps[Creep].GetComponent<Creep>().SpawnTime) // performance ??
            {
                GameObject t = SpawnableCreeps[Creep];
                t.GetComponent<Creep>().Team = Team;
                Spawner.spawn(SpawnableCreeps[Creep]);
                PercentageSpawn = 0f;
            }
        }
    }

    public Manager spawn
    {
        set
        {
            Spawner = value;
        }
    }
}
