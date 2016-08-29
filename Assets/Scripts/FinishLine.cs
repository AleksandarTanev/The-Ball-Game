using UnityEngine;
using System.Collections;

public class FinishLine : MonoBehaviour 
{
    public GameObject fireWorks;
    public GameObject youWinText;
	// Use this for initialization
	void Start () 
    {
	
	}
	
	// Update is called once per frame
	void Update () 
    {
	
	}


    void OnTriggerEnter(Collider col)
    {

        if (col.gameObject.name == "Ball")
        {
            fireWorks.gameObject.SetActive(true);
            youWinText.gameObject.SetActive(true);
        }

    }
}
