using UnityEngine;
using System.Collections;

public class Cam : MonoBehaviour
{

    public float Speed;
    public int Tolleranz;

    public float MaxX;
    public float minX;
    public float MaxZ;
    public float minZ;
    // Use this for initialization
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.mousePosition.y > Screen.height - Tolleranz)
        {
            if (Camera.main.transform.position.z < MaxZ)
            {
                transform.position += Vector3.forward * Speed * Time.deltaTime;
            }
        }
        if (Input.mousePosition.y < Tolleranz)
        {
            if (Camera.main.transform.position.z > minZ)
            {
                transform.position += Vector3.back * Speed * Time.deltaTime;
            }
        }
        if (Input.mousePosition.x > Screen.width - Tolleranz)
        {
            if (Camera.main.transform.position.x < MaxX)
            {
                transform.position += Vector3.right * Speed * Time.deltaTime;
            }
        }
        if (Input.mousePosition.x < Tolleranz)
        {
            if (Camera.main.transform.position.x > minX)
            {
                transform.position += Vector3.left * Speed * Time.deltaTime;
            }
        }



        if (Input.mouseScrollDelta.y >= 1f)
        {
            if (Camera.main.fieldOfView > 10f)
            {
                Camera.main.fieldOfView -= 5f;
            }
        }
        if (Input.mouseScrollDelta.y <= -1f)
        {
            if (Camera.main.fieldOfView < 105f)
            {
                Camera.main.fieldOfView += 5f;
            }
        }
    }
}
