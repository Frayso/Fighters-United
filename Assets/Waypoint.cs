using UnityEngine;
using System.Collections;

[RequireComponent(typeof (BoxCollider))]
public class Waypoint : MonoBehaviour
{
    public GameObject NextWaypointTeam1;
    public GameObject NextWaypointTeam2;
    // Use this for initialization
    void Start ()
    {
        GetComponent<BoxCollider>().isTrigger = true;
	}
	
	// Update is called once per frame
	void Update ()
    {
	
	}

    void OnTriggerEnter(Collider col)
    {
        if (!col.isTrigger)
        {
            var tmp = col.gameObject.GetComponent<Creep>();
            if (tmp.Team)     // optimize
            {
                tmp.NextWaypoint = NextWaypointTeam2.transform.position;
            }
            else
            {
                tmp.NextWaypoint = NextWaypointTeam1.transform.position;
            }
        }
    }
}
