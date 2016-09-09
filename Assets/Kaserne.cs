using UnityEngine;
using System.Collections;

public class Kaserne : Building
{
    public GameObject[] SpawnableCreeps;
    public int Creep;
    public bool Stop;

    private float PercentageSpawn;

    private Manager Spawner;
    // Use this for initialization
    protected override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();

        PercentageSpawn += Time.deltaTime; 

        if (PercentageSpawn >= SpawnableCreeps[Creep].GetComponent<Creep>().SpawnTime) // performance ??
        {
            Spawner.spawn(SpawnableCreeps[Creep]);
            PercentageSpawn = 0f;
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
