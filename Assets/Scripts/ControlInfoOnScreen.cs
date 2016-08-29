using UnityEngine;
using System.Collections;

public class ControlInfoOnScreen : MonoBehaviour {

    public GameObject ballInfo;
    public GameObject handInfo;

	// Use this for initialization
	void Start () 
    {
	
	}
	
	// Update is called once per frame
	void Update () 
    {
        if (Input.GetKeyDown("1"))
        {
            ballInfo.gameObject.SetActive(false);
            handInfo.gameObject.SetActive(false);
        }
        else if (Input.GetKeyDown("2"))
        {
            ballInfo.gameObject.SetActive(true);
            handInfo.gameObject.SetActive(true);
        }
	
	}
}
