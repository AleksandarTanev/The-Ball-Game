using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public float x;
    public float y;
    public float z;

	void Update () 
    {
        transform.position = new Vector3(gameObject.transform.position.x + x, gameObject.transform.position.y + y, gameObject.transform.position.z + z);
    }
}
