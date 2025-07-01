using UnityEngine;

public class RefrigeratorDoorController : MonoBehaviour
{
    public ArticulationBody doorJoint;
    public float openAngle = 90f;
    public float closeAngle = 0f;
    public float speed = 30f;
    public float stiffness = 10000f;
    public float damping = 1000f;
    public float forceLimit = 10f;
    public KeyCode toggleKey = KeyCode.R;

    private float target;

    void Start()
    {
        // Set anchor position and rotation
        doorJoint.anchorPosition = new Vector3(0.321490f, -0.45086f, -0.03219f);
        doorJoint.anchorRotation = Quaternion.Euler(0f, 0f, 270f);

        // Set drive settings
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
