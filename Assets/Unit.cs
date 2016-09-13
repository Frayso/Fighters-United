using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class Unit : NetworkBehaviour
{
    public int Life;
    public int Def;

    [SyncVar]
    public bool Team;    

    // Use this for initialization
    protected virtual void Start ()
    {
        if (Life <= 0)
        {
            Life = 1;
        }
	}
	
	// Update is called once per frame
	protected virtual void Update ()
    {
        if (Life <= 0)
        {
            Destroy(this.gameObject);
        }
	}
}
