using UnityEngine;

public class ToiletDoorController : MonoBehaviour
{
    public ArticulationBody doorJoint;
    public float openAngle = -90f;
    public float closeAngle = 0f;
    public float speed = 30f;
    public float stiffness = 10000f;
    public float damping = 1000f;
    public float forceLimit = 0.2f;
    public KeyCode toggleKey = KeyCode.T;

    private float target;

    void Start()
    {
        // Set anchor position and rotation (from your screenshot)
        doorJoint.anchorPosition = new Vector3(0.29693f, 0.65544f, -0.34468f);
        doorJoint.anchorRotation = Quaternion.Euler(270f, 0f, 0f);

        // Apply articulation drive parameters
        ArticulationDrive drive = doorJoint.xDrive;
        drive.lowerLimit = openAngle;  // -90
        drive.upperLimit = closeAngle; // 0
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
