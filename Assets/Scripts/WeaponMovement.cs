using UnityEngine;
using System.Collections.Generic;
using Leap;
using UnityEngine.UI;

public class WeaponMovement : MonoBehaviour
{
    public GameObject WeaponAnimation;
    public GameObject Bullet;
    public float ShootForce;
    public Transform ShootPosition;

    public Text DisplayHandMovement;
    public GameObject CoordinationObject;

    private Controller controller;

    private Frame frame;
    private List<Hand> hands;
    private Hand weaponHand;
    private Finger weaponFinger;

    private void Start()
    {
        controller = new Controller();
    }

    private void FixedUpdate()
    {
        frame = controller.Frame();
        hands = frame.Hands;

        if (hands.Count == 0)
        {
            return;
        }

        weaponHand = null;
        weaponFinger = null;

        int leftHandIndex = GetLeftHandIndex(hands);
        int rightHandIndex = GetRightHandIndex(hands);

        int countR = 0;
        if (rightHandIndex != -1)
        {
            for (int i = 0; i < hands[rightHandIndex].Fingers.Count; i++)
            {
                if (hands[rightHandIndex].Fingers[i].IsExtended == true)
                {
                    countR++;
                }
            }
            if (countR == 1 && hands[rightHandIndex].Fingers[1].IsExtended == true)
            {
                weaponHand = hands[rightHandIndex];
                weaponFinger = weaponHand.Fingers[1];

                InfoOnScreenController.PostFingerInfo(weaponFinger);
            }
        }

        int countL = 0;
        if (leftHandIndex != -1)
        {
            for (int i = 0; i < hands[leftHandIndex].Fingers.Count; i++)
            {
                if (hands[leftHandIndex].Fingers[i].IsExtended == true)
                {
                    countL++;
                }
            }

            if (countL == 1 && hands[leftHandIndex].Fingers[1].IsExtended == true)
            {
                weaponHand = hands[leftHandIndex];
                weaponFinger = weaponHand.Fingers[1];

                InfoOnScreenController.PostFingerInfo(weaponFinger);
            }
        }

        if (weaponHand != null)
        {
            float xDirection = weaponFinger.Direction.x;

            if (xDirection < -0.45)
            {
                CoordinationObject.transform.localEulerAngles = new Vector3(0, -45, 0);
            }
            else if (xDirection > 0.45)
            {
                CoordinationObject.transform.localEulerAngles = new Vector3(0, 45, 0);
            }
            else
            {
                CoordinationObject.transform.localEulerAngles = new Vector3(0, xDirection * 100, 0);
            }

            if (weaponFinger.Direction.y > 0.4 && !WeaponAnimation.GetComponent<Animation>().isPlaying)
            {
                WeaponAnimation.GetComponent<Animation>()["Shoot"].speed = 10;
                WeaponAnimation.GetComponent<Animation>().Play("Shoot");

                GameObject InstanceBullet = (GameObject)Instantiate(Bullet, ShootPosition.transform.position, ShootPosition.rotation);
                InstanceBullet.GetComponent<Rigidbody>().AddForce(ShootPosition.forward * ShootForce);
                Destroy(InstanceBullet, 2);
            }
        }
    }
    
    private int GetLeftHandIndex(List<Hand> hands)
    {
        int index = -1;

        for (int i = 0; i < hands.Count; i++)
        {
            if (hands[i].IsLeft)
            {
                index = i;
            }
        }

        return index;
    }

    private int GetRightHandIndex(List<Hand> hands)
    {
        int index = -1;

        for (int i = 0; i < hands.Count; i++)
        {
            if (hands[i].IsRight)
            {
                index = i;
            }
        }

        return index;
    }
}
