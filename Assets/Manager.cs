using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;

public class Manager : NetworkBehaviour
{
    public List<GameObject> SpawnList;
    public byte Selected;

    private bool Buildmood;
    private bool gotAteam;

    private GameObject temp;
    // Use this for initialization
    void Start ()
    {
        Buildmood = false;
        SpawnList = GameObject.Find("NetworkManager").GetComponent<NetworkManager>().spawnPrefabs;
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (Buildmood)
        {
            if (Input.GetMouseButtonDown(0))
            {                
                CmdSpawn(temp.transform.position, Selected);
                Destroy(temp);
                Buildmood = false;
            }
            if (Input.GetMouseButtonDown(1))
            {
                Destroy(temp);
                Buildmood = false;
            }
        }
        if (Buildmood)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);            
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                temp.transform.position = new Vector3(hit.point.x, temp.transform.position.y, hit.point.z);                
                //hit.collider.renderer.material.color = Color.red;                
            }            
        }
	}

    void OnGUI()
    {
        if (hasAuthority)
        {
            if (!Buildmood)
            {
                if (GUI.Button(new Rect(Screen.width - 160, Screen.height - 110, 150, 100), SpawnList[Selected].name))
                {
                    temp = (GameObject)Instantiate(SpawnList[Selected], SpawnList[Selected].transform.position, Quaternion.AngleAxis(0f, new Vector3(0f, 0f, 0f)));
                    Buildmood = true;
                }
                if (Input.mousePosition.x >= Screen.width - 160 && Input.mousePosition.y <= 110f)
                {
                    GUI.Label(new Rect(Screen.width - 150, Screen.height - 35, 150, 25), "8 vertices  FTW!!");
                }
            }
        }
    }

    public void spawn(Vector3 SpawnPosition, byte SpawnIndex)
    {
        CmdSpawn(SpawnPosition, SpawnIndex);
    }

    [Command(channel = 0)]
    private void CmdSpawn(Vector3 SpawnPosition, byte SpawnIndex)
    {
        GameObject temp = (GameObject)Instantiate(SpawnList[SpawnIndex], new Vector3(SpawnPosition.x, SpawnList[SpawnIndex].transform.position.y, SpawnPosition.z), Quaternion.AngleAxis(0f, new Vector3(0f, 0f, 0f)));
        NetworkServer.Spawn(temp);
        if (temp.GetComponent<Kaserne>())
        {
            temp.GetComponent<Kaserne>().spawn = this;
        }

    }

    public void spawn(Vector3 SpawnPosition, GameObject SpawnObject)
    {
        CmdSpawn2(SpawnPosition, SpawnObject);
    }

    [Command(channel = 0)]
    private void CmdSpawn2(Vector3 SpawnPosition, GameObject SpawnObject)
    {   //http://answers.unity3d.com/questions/45079/instantiated-objects-scripts-not-enabled-not-sure.html
        temp = (GameObject)Instantiate(SpawnObject, new Vector3(SpawnPosition.x, SpawnObject.transform.position.y, SpawnPosition.z), Quaternion.AngleAxis(0f, new Vector3(0f, 0f, 0f)));
        if (temp.GetComponent<Kaserne>())
        {
            temp.GetComponent<Kaserne>().spawn = this;
        }
        NetworkServer.Spawn(temp);
    }

    public void spawn(GameObject Spawnobject)
    {
        CmdSpawn3(Spawnobject);
    }

    [Command(channel = 0)]
    private void CmdSpawn3(GameObject Spawnobject)  //bugy
    {
        GameObject temp = (GameObject)Instantiate(Spawnobject, Spawnobject.transform.position, Quaternion.AngleAxis(0f, new Vector3(0f, 0f, 0f)));
        NetworkServer.Spawn(temp);
    }
}
