using UnityEngine;
using Leap;
using UnityEngine.UI;
using System.Collections.Generic;

public class Ball : MonoBehaviour
{
    public Text DisplayHandMovement;
    public Text DisplayBallMovement;

    public GameObject Controller_Handle;

    public GameObject CoordinationObject; 

    private Controller controller;

    private void Start()
    {
        controller = new Controller();
    }

    private void FixedUpdate()
    {
        Frame frame = controller.Frame();

        if (frame.Hands.Count == 0)
        {
            return;
        }

        List<Hand> hands = frame.Hands;
        Hand ControlerHand = hands[0];

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

        int countR = 0;
        int countL = 0;

        if (LeftHandIndex != -1)
        {
            for (int i = 0; i < hands[LeftHandIndex].Fingers.Count; i++)
            {
                if (hands[LeftHandIndex].Fingers[i].IsExtended == true)
                {
                    countL++;
                }
                else
                {
                    break;
                }
            }

            if (countL == 5)
            {
                ControlerHand = hands[LeftHandIndex];
            }
        }
        
        if (RightHandIndex != -1)
        {
            for (int i = 0; i < hands[RightHandIndex].Fingers.Count; i++)
            {
                if (hands[RightHandIndex].Fingers[i].IsExtended == true)
                {
                    countR++;
                }
                else
                {
                    break;
                }
            }

            if (countR == 5)
            {
                ControlerHand = hands[RightHandIndex];
            }
        }

        float pitch = ControlerHand.Direction.Pitch;
        float yaw = ControlerHand.Direction.Yaw;
        float roll = ControlerHand.PalmNormal.Roll;

        float x = 0;
        float y = 0;
        float z = 0;

        if (countR == 5 || countL == 5)
        {
            // Move forward
            if (pitch > 0.3)
            {
                if (GetComponent<Rigidbody>().velocity.z < 10)
                {
                    GetComponent<Rigidbody>().AddForce(CoordinationObject.transform.forward * 15f);

                }
                x = 30.0f;
            }
           
            // Move backward
            else if (pitch < -0.3)
            {
                if (GetComponent<Rigidbody>().velocity.z > -10)
                {
                    GetComponent<Rigidbody>().AddForce(-CoordinationObject.transform.forward * 15f);

                }
                x = -30.0f;
            }
            
            // Move left
            if (yaw < -0.4)
            {
                if (GetComponent<Rigidbody>().velocity.x > -10)
                {
                    GetComponent<Rigidbody>().AddForce(CoordinationObject.transform.right * -15f);
                }
                z = 30.0f;
            }
            
            // Move right
            if (yaw > 0.4  )
            {
                if (GetComponent<Rigidbody>().velocity.x < 10)
                {
                    GetComponent<Rigidbody>().AddForce(CoordinationObject.transform.right * 15f);
                }
                z = -30.0f;
            }
            
            // Rotate left
            if (roll > 0.8)
            {
                CoordinationObject.transform.Rotate(0, Time.deltaTime * -50f, 0);
            }

            // Rotate right
            if (roll < -0.8)
            {
                CoordinationObject.transform.Rotate(0, Time.deltaTime * 50f, 0);
            }     
        }
            
        Controller_Handle.transform.localRotation = Quaternion.Euler(x, y, z);

        //InfoOnScreenController.PostHandInfo(ControlerHand);
        InfoOnScreenController.PostBallInfo(this.GetComponent<Rigidbody>());
        
        /*
        // Jump space
        if (Input.GetButtonDown("Jump") && transform.position.y < 2)
        {
            transform.rigidbody.AddForce(Vector3.up * jumpSpeed);
        }*/
    }
}
