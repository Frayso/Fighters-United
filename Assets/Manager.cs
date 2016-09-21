using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;

public class Manager : NetworkBehaviour
{
    public List<GameObject> SpawnList;
    public byte Selected;

    private bool Buildmood;
    private bool hasTeam;
    public bool team;

    private GameObject temp;

    public Vector3 StartCameraPosTeam1;
    public Vector3 StartCameraPosTeam2;

    // Use this for initialization
    void Start ()
    {
        Buildmood = false;
        SpawnList = GameObject.Find("NetworkManager").GetComponent<NetworkManager>().spawnPrefabs;
        hasTeam = false;

        if (hasAuthority)
        {
            GameObject[] gm = GameObject.FindGameObjectsWithTag("GameController");
            gameObject.name = "Player" + gm.Length.ToString();
        }
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (Buildmood)
        {
            if (Input.GetMouseButtonDown(0))
            {                
                spawn(temp.transform.position, Selected);
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
            if (!hasTeam)
            {
                Rect b1 = new Rect(Screen.width / 2, Screen.height / 2, 150, 100);
                Rect b2 = new Rect(Screen.width / 2, (Screen.height / 2) + 110, 150, 100);
                if (GUI.Button(b1, "Join Team 1"))
                {
                    team = false;
                    CmdNewTeam(this.team);
                    hasTeam = true;
                    Camera.main.transform.position = StartCameraPosTeam1;
                    //Camera.main.fieldOfView = 60f;
                }
                if (b1.Contains(new Vector2(Input.mousePosition.x, (Screen.height - Input.mousePosition.y))))
                {
                    GUI.Label(new Rect(Input.mousePosition.x + 25, (Screen.height - Input.mousePosition.y), 150, 50), "Klick to join Team 1!");
                }

                if (GUI.Button(b2, "Join Team 2"))
                {
                    team = true;
                    CmdNewTeam(this.team);
                    hasTeam = true;
                    Camera.main.transform.position = StartCameraPosTeam2;
                    //Camera.main.fieldOfView = 60f;
                }
                if (b2.Contains(new Vector2(Input.mousePosition.x, (Screen.height - Input.mousePosition.y))))
                {
                    GUI.Label(new Rect(Input.mousePosition.x + 25, (Screen.height - Input.mousePosition.y), 150, 50), "why da fak r u reading dis???");
                }
            }


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
        GameObject temp = (GameObject)Instantiate(SpawnList[SpawnIndex], new Vector3(SpawnPosition.x, SpawnList[SpawnIndex].transform.position.y, SpawnPosition.z), Quaternion.identity);

        NetworkServer.SpawnWithClientAuthority(temp, gameObject);

        temp.GetComponent<Unit>().Team = team;
        if (temp.GetComponent<Kaserne>() != null)
        {
            temp.GetComponent<Kaserne>().Spawner = this.gameObject;
        }
    }   

    public void spawn(Vector3 SpawnPosition, GameObject SpawnObject)
    {
        CmdSpawn2(SpawnPosition, SpawnObject);
    }

    [Command(channel = 0)]
    private void CmdSpawn2(Vector3 SpawnPosition, GameObject SpawnObject)
    {   //http://answers.unity3d.com/questions/45079/instantiated-objects-scripts-not-enabled-not-sure.html
        temp = (GameObject)Instantiate(SpawnObject, new Vector3(SpawnPosition.x, SpawnObject.transform.position.y, SpawnPosition.z), Quaternion.identity);        
        //NetworkServer.Spawn(temp);
        NetworkServer.SpawnWithClientAuthority(temp, gameObject);
    }

    public void spawn(GameObject Spawnobject)
    {
        CmdSpawn3(Spawnobject);
    }

    [Command(channel = 0)]
    public void CmdSpawn3(GameObject Spawnobject)  //bugy
    {
        GameObject temp = (GameObject)Instantiate(Spawnobject, Spawnobject.transform.position, Quaternion.identity);
        //NetworkServer.Spawn(temp);
        NetworkServer.SpawnWithClientAuthority(temp, gameObject);
    }

    [Command(channel = 0)]
    public void CmdNewTeam(bool newTeam)
    {
        this.team = newTeam;
    }

    public bool Team
    {
        get
        {
            return team;
        }
    }
}
