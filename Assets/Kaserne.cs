using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class Kaserne : Building
{
    public GameObject[] SpawnableCreeps;
    public int Creep;
    public bool Stop;
    public Vector3 Spawnoffset;

    private float PercentageSpawn;


    public GameObject Spawner;
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

        if (!isServer)
        {
            return;
        }        


        if (!Stop)
        {
            PercentageSpawn += Time.deltaTime;

            if (PercentageSpawn >= SpawnableCreeps[Creep].GetComponent<Creep>().SpawnTime) // performance ??
            {
                GameObject t = SpawnableCreeps[Creep];
                t.GetComponent<Creep>().Team = Team;

                Spawner.GetComponent<Manager>().spawn(transform.position + t.transform.position + Spawnoffset, 1); // 1 to spawnindex
                PercentageSpawn = 0f;
            }
        }        
    }    

    public GameObject spawn
    {
        set
        {
            Spawner = value;
        }
    }
}
