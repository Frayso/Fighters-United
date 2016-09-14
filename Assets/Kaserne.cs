using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class Kaserne : Building
{
    public GameObject[] SpawnableCreeps;
    public int Creep;
    public bool Stop;

    private float PercentageSpawn;

    [SyncVar]
    public string SpawnerName;

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
        if (!hasAuthority)
        {
            return;
        }

        if (Spawner == null)
        {
            GameObject[] gm = GameObject.FindGameObjectsWithTag("GameController");
            for (int i = 0; i < gm.Length; i++)
            {
                if (gm[i].GetComponent<Manager>().hasAuthority == true)
                {
                    Spawner = gm[i];
                }
            }
            Team = Spawner.GetComponent<Manager>().team;
        }
        //if (Spawner == null)
        //{
        //    //if (hasAuthority)
        //    //{
        //    //    GameObject[] gm = GameObject.FindGameObjectsWithTag("GameController");
        //    //    SpawnerName = "Player" + gm.Length.ToString();
        //    //    Spawner = GameObject.Find(SpawnerName).GetComponent<Manager>();
        //    //    Team = Spawner.team;
        //    //    return;
        //    //}

        //    ////if (SpawnerName != "Player" && SpawnerName != "" && SpawnerName != null)
        //    ////{
        //    //    Spawner = GameObject.Find(SpawnerName).GetComponent<Manager>();
        //    //    Team = Spawner.team;
        //    ////}                      
        //}
        //else
        //{
        //    if (hasAuthority)
        //    {
        Debug.LogWarning("do da update");
                PercentageSpawn += Time.deltaTime;

                if (PercentageSpawn >= SpawnableCreeps[Creep].GetComponent<Creep>().SpawnTime) // performance ??
                {
            Debug.LogWarning("Spawn");
            GameObject t = SpawnableCreeps[Creep];
                    t.GetComponent<Creep>().Team = Team;
            
            CmdSpawn(t, Spawner);
                    PercentageSpawn = 0f;
                }
        //    }
        //}
    }

    [Command(channel = 0)]
    public void CmdSpawn(GameObject Spawnobject, GameObject spawner)  //bugy
    {
        GameObject temp = (GameObject)Instantiate(Spawnobject, Spawnobject.transform.position, Quaternion.identity);
        //NetworkServer.Spawn(temp);
        NetworkServer.SpawnWithClientAuthority(temp, base.connectionToClient);
    }

    public GameObject spawn
    {
        set
        {
            Spawner = value;
        }
    }
}
