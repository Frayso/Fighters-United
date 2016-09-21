using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class Unit : NetworkBehaviour
{
    public int maxLife;

    [SyncVar]
    public int Life;

    public int Def;

    //[SyncVar]
    public bool Team;  

    // Use this for initialization
    protected virtual void Start ()
    {
        if (Life <= 0)
        {
            Life = 1;
        }
        Life = maxLife;        
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
