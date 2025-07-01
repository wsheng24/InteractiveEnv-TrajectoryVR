using UnityEngine;

public class MicrowaveDoorController : MonoBehaviour
{
    public ArticulationBody doorJoint;
    public float openAngle = 90f;
    public float closeAngle = 0f;
    public float speed = 30f;
    public float stiffness = 10000f;
    public float damping = 100f;
    public float forceLimit = 0.2f;
    public KeyCode toggleKey = KeyCode.M;

    private float target;

    void Start()
    {
        // Set fixed anchor values
        doorJoint.anchorPosition = new Vector3(0.263184f, -0.53176f, 1.136885f);
        doorJoint.anchorRotation = Quaternion.Euler(0f, 0f, 90f);


        // Set initial articulation drive settings
        ArticulationDrive drive = doorJoint.xDrive;
        drive.stiffness = stiffness;
        drive.damping = damping;
        drive.forceLimit = forceLimit;
        drive.target = closeAngle;
        doorJoint.xDrive = drive;

        target = closeAngle;
    }

    void Update()
    {
        if (Input.GetKeyDown(toggleKey))
        {
            float current = doorJoint.xDrive.target;
            target = Mathf.Approximately(current, closeAngle) ? openAngle : closeAngle;
        }

        ArticulationDrive drive = doorJoint.xDrive;
        drive.target = Mathf.MoveTowards(drive.target, target, speed * Time.deltaTime);
        doorJoint.xDrive = drive;
    }
}
