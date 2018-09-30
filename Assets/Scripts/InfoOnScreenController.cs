using UnityEngine;
using UnityEngine.UI;
using Leap;

public class InfoOnScreenController : MonoBehaviour
{
    private static InfoOnScreenController instance;

    public GameObject objLeftInfo;
    public GameObject objRightInfo;

    public Text txtLeftInfo;
    public Text txtRightInfo;

    //private Controller controller;

    private void Start()
    {
        instance = this;
       // controller = new Controller();
    }

    private void Update () 
    {
        if (Input.GetKeyDown("1"))
        {
            objRightInfo.gameObject.SetActive(false);
            objLeftInfo.gameObject.SetActive(false);
        }
        else if (Input.GetKeyDown("2"))
        {
            objRightInfo.gameObject.SetActive(true);
            objLeftInfo.gameObject.SetActive(true);
        }
    }

    public static void PostFingerInfo(Finger Finger)
    {
        float x = Finger.Direction.x;
        float y = Finger.Direction.y;
        float z = Finger.Direction.z;
        float Yaw = Finger.Direction.Yaw;

        instance.txtLeftInfo.text = "x: " + x.ToString("F3") + "\n";
        instance.txtLeftInfo.text += "y: " + y.ToString("F3") + "\n";
        instance.txtLeftInfo.text += "z: " + z.ToString("F3") + "\n";
        instance.txtLeftInfo.text += "Yaw: " + Yaw.ToString("F3");
    }

    public static void PostHandInfo(Hand firstHand)
    {
        float pitch = firstHand.Direction.Pitch;
        float yaw = firstHand.Direction.Yaw;
        float roll = firstHand.PalmNormal.Roll;

        instance.txtLeftInfo.text = "Pitch: " + pitch.ToString("F3") + "\n";
        instance.txtLeftInfo.text += "Yaw: " + yaw.ToString("F3") + "\n";
        instance.txtLeftInfo.text += "Roll: " + roll.ToString("F3");
    }

    public static void PostBallInfo(Rigidbody rb)
    {
        instance.txtRightInfo.text = "Speed x: " + rb.velocity.x.ToString("F3") + "\n";
        instance.txtRightInfo.text += "Speed y: " + rb.velocity.y.ToString("F3") + "\n";
        instance.txtRightInfo.text += "Speed z: " + rb.velocity.z.ToString("F3");
    }
}
