using UnityEngine;
using System.Collections;

public class ActivateBridge : MonoBehaviour 
{

    public GameObject Target_1;
    public GameObject Target_2;
    public GameObject Target_3;

	// Use this for initialization
	void Start () 
    {

    }
	
	// Update is called once per frame
	void Update () 
    {
             foreach (Transform child in this.transform)
             {
                 if (Target_1.activeInHierarchy == false && Target_2.activeInHierarchy == false && Target_3.activeInHierarchy == false)
                 {
                     child.gameObject.SetActive(true);
                 }
             }
    }
}
