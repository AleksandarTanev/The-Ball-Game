using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour
{

    public GameObject gameObject;

    public float x;
    public float y;
    public float z;

	void Update () 
    {
        transform.position = new Vector3(gameObject.transform.position.x + x, gameObject.transform.position.y + y, gameObject.transform.position.z + z);
    }
}
