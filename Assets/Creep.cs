using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody))]
public class Creep : Unit
{
    public float SpawnTime;

    public bool AirUnit;

    public int Dmg;
    public float AttackSpeed;
    public float MovementSpeed;

    public float NoticeRange;
    public float AttackRange;

    private GameObject Target;

    private Vector3 nextWaypoint;
    // Use this for initialization
    protected override void Start ()
    {
        base.Start();
    }
	
	// Update is called once per frame
	protected override void Update ()
    {
        base.Update();
        transform.position += transform.forward * MovementSpeed * Time.deltaTime;
    }

    public Vector3 NextWaypoint
    {
        set
        { //if waypoint
            nextWaypoint = value;
            transform.LookAt(nextWaypoint);
        }
    }
}
