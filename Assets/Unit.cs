using UnityEngine;
using System.Collections;

public class Unit : MonoBehaviour
{
    public int Life;
    public int Def;

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
