using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(SphereCollider))]
public class Creep : Unit
{ 
    /// <summary>
/// Class 4 creeps
/// </summary>
    public float SpawnTime;

    public bool AirUnit;

    public int Dmg;
    public float AttackSpeed;
    private float attackCooldown;
    public float MovementSpeed;

    public float NoticeRange;
    public float AttackRange;
    private bool attack;

    public GameObject Target;

    private Vector3 nextWaypoint;

    private SphereCollider range;
    // Use this for initialization
    protected override void Start ()
    {
        base.Start();
        
        range = gameObject.GetComponent<SphereCollider>();
        range.radius = NoticeRange / transform.localScale.x;

        attackCooldown = 0f;
    }
	
	// Update is called once per frame
	protected override void Update ()
    {
        base.Update();
        //if (isServer)
        //{
            if (Target != null)
            {
                if (Target.GetComponent<Unit>().Team == Team)
                {
                    Target = null;
                }
            }

            if (lookAtEnemie)
            {
                transform.LookAt(Target.transform);

            if (attack)
            {
                if (isServer)
                {
                    if (attackCooldown <= 0f)
                    {
                        Target.GetComponent<Unit>().Life = Target.GetComponent<Unit>().Life - Dmg;
                        attackCooldown = AttackSpeed;
                    }
                    attackCooldown = attackCooldown - Time.deltaTime;
                    attack = false;
                    return;
                }
            }
            else
            {
                //attackCooldown = 0;
                transform.position += transform.forward * MovementSpeed * Time.deltaTime;
            }
            }
            else
            {
                transform.LookAt(nextWaypoint);
                range.radius = NoticeRange / transform.localScale.x;
            }

            transform.position += transform.forward * MovementSpeed * Time.deltaTime;

            attack = false;
        //}
    }

    public Vector3 NextWaypoint
    {
        set
        { //if waypoint
            nextWaypoint = value;            
        }
    }

    void OnTriggerStay(Collider col)
    {        // if server
        if (!col.isTrigger)
        {
            if (Target == null && col.gameObject.GetComponent<Unit>() != null)
            {
                if (col.gameObject.GetComponent<Unit>().Team != Team)
                {
                    Target = col.gameObject;
                    range.radius = AttackRange / transform.localScale.x;
                }
            }
            else
            {
                if (col.gameObject.GetComponent<Unit>() != null)
                {
                    if (col.gameObject.GetComponent<Unit>().Team != Team)
                    {
                        attack = true;
                    }
                }
            }            
        }
    }

    private bool lookAtEnemie
    {
        get
        {
            if (Target != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
