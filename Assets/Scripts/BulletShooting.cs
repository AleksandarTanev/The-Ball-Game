using UnityEngine;
using System.Collections;

public class BulletShooting : MonoBehaviour 
{

    public GameObject WeaponAnimation;
    public GameObject Bullet;
    public float ShootForce;
    public Transform ShootPosition;

	// Update is called once per frame
	void Update () 
    {
        if (Input.GetKey("w") &&  !WeaponAnimation.GetComponent<Animation>().isPlaying)
        {
            WeaponAnimation.GetComponent<Animation>()["Shoot"].speed = 10;
            WeaponAnimation.GetComponent<Animation>().Play("Shoot");


            GameObject InstanceBullet = (GameObject)Instantiate(Bullet, transform.position, ShootPosition.rotation);
            InstanceBullet.GetComponent<Rigidbody>().AddForce(ShootPosition.forward * ShootForce);
            Destroy(InstanceBullet, 2);
        }
	}
}
