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

        if (Target != null)
        {
            if (attack == false)
            {
                attackCooldown = 0f;          // should it

                transform.LookAt(Target.transform.position);
                transform.position += transform.forward * MovementSpeed * Time.deltaTime;
            }
            else
            {
                attackCooldown = attackCooldown - Time.deltaTime;
                if (attackCooldown <= 0f)
                {
                    DamageCalculation.DoDamage(gameObject, Target);
                    attackCooldown = AttackSpeed;
                }
            }
        }
        else
        {
            attackCooldown = 0f;
            range.radius = NoticeRange / transform.localScale.x;

            transform.LookAt(nextWaypoint);
            transform.position += transform.forward * MovementSpeed * Time.deltaTime;
        }
    }

    public Vector3 NextWaypoint
    {
        set
        { //if waypoint
            nextWaypoint = value;            
        }
    }

    void OnTriggerEnter(Collider col)
    {
        if (!col.isTrigger && col.gameObject.GetComponent<Unit>() != null)
        {
            if (col.gameObject.GetComponent<Unit>().Team != Team)
            {
                if (Target == null)
                {
                    Target = col.gameObject;
                    range.radius = AttackRange / transform.localScale.x;
                }
                else
                {
                    attack = true;
                }
            }
        }
    }

    void OnTriggerExit(Collider col)
    {
        if (!col.isTrigger && col.gameObject.GetComponent<Unit>() != null)
        {
            if (col.gameObject.GetComponent<Unit>() != Team)
            {
                if (Target != null)
                {
                    attack = false;
                }
                else
                {
                    range.radius = NoticeRange / transform.localScale.x;
                }
            }
        }
    }

    void OnTriggerStay(Collider col)
    {        // ist nötig ?      savety first ??
        if (!col.isTrigger && col.gameObject.GetComponent<Unit>() != null)
        {
            if (col.gameObject.GetComponent<Unit>() != Team)
            {
                if (Target != null)
                {
                    attack = true;
                }
            } 
        }
    }
}
