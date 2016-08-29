using UnityEngine;
using System.Collections;
using Leap;
using UnityEngine.UI;


public class WeaponMovement : MonoBehaviour {

    // for animation 
    public GameObject WeaponAnimation;
    public GameObject Bullet;
    public float ShootForce;
    public Transform ShootPosition;
    // ...

    public Text DisplayHandMovement;
    public GameObject CoordinationObject;

    Controller controller;

    void Start()
    {
        controller = new Controller();
    }


    void LateUpdate()
    {


        Frame frame = controller.Frame();

        HandList hands = frame.Hands;
        Hand WeaponHand = hands[0];
        Finger WeaponFinger = hands[0].Fingers[0];

        int LeftHandIndex = -1;
        int RightHandIndex = -1;

        for (int i = 0; i < hands.Count; i++)
        {
            if (hands[i].IsRight == true)
            {
                RightHandIndex = i;
            }
            else if (hands[i].IsLeft == true)
            {
                LeftHandIndex = i;
            }
        }




        int countL = 0;
        int flag = 0;
        int countR = 0;

        if (RightHandIndex != -1)
        {
            for (int i = 0; i < hands[RightHandIndex].Fingers.Count; i++)
            {
                if (hands[RightHandIndex].Fingers[i].IsExtended == true)
                {
                    countR++;
                }
            }
            if (countR == 1 && hands[RightHandIndex].Fingers[1].IsExtended == true)
            {
                WeaponHand = hands[RightHandIndex];
                WeaponFinger = WeaponHand.Fingers[1];
                flag = 1;

                FingerInfo(WeaponFinger);
            }
        }
        
        if (LeftHandIndex != -1)
        {
            for (int i = 0; i < hands[LeftHandIndex].Fingers.Count; i++)
            {
                if (hands[LeftHandIndex].Fingers[i].IsExtended == true)
                {
                    countL++;
                }

            }

            if (countL == 1 && hands[LeftHandIndex].Fingers[1].IsExtended == true)
            {
                WeaponHand = hands[LeftHandIndex];
                WeaponFinger = WeaponHand.Fingers[1];
                flag = 1;

                FingerInfo(WeaponFinger);
            }
        }



        float xDirection = WeaponFinger.Direction.x;

        if (flag == 1)
        {
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
        }




        if (WeaponFinger.Direction.y > 0.4 && !WeaponAnimation.GetComponent<Animation>().isPlaying && flag == 1)
        {
            WeaponAnimation.GetComponent<Animation>()["Shoot"].speed = 10;
            WeaponAnimation.GetComponent<Animation>().Play("Shoot");


            GameObject InstanceBullet = (GameObject)Instantiate(Bullet, ShootPosition.transform.position, ShootPosition.rotation);
            InstanceBullet.GetComponent<Rigidbody>().AddForce(ShootPosition.forward * ShootForce);
            Destroy(InstanceBullet, 2);
        }



    }

    private void HandInfo(Hand firstHand)
    {
        float pitch = firstHand.Direction.Pitch;
        float yaw = firstHand.Direction.Yaw;
        float roll = firstHand.PalmNormal.Roll;

        DisplayHandMovement.text = "Pitch: " + pitch.ToString() + "\n";
        DisplayHandMovement.text += "Yaw: " + yaw.ToString() + "\n";
        DisplayHandMovement.text += "Roll: " + roll.ToString();
    }

    private void FingerInfo(Finger Finger)
    {
        float x = Finger.Direction.x;
        float y = Finger.Direction.y;
        float z = Finger.Direction.z;
        float Yaw = Finger.Direction.Yaw;

        DisplayHandMovement.text = "x: " + x.ToString() + "\n";
        DisplayHandMovement.text += "y: " + y.ToString() + "\n";
        DisplayHandMovement.text += "z: " + z.ToString() + "\n";
        DisplayHandMovement.text += "Yaw: " + Yaw.ToString();
    }
}
