using UnityEngine;

public class PunchListener : MonoBehaviour
{
    public GameObject DebugPrefab;
    public OVRInput.Controller ControllerHandle;
    public Transform ForwardTransform;
    public Transform FistTransform;

    public event System.Action<Vector3> OnPunch;

    [Range(0.1f, 0.35f)]
    public float PunchStartRange;
    [Range(0.36f, 1f)]
    public float PunchEndRange;
    [Range(1f, 6f)]
    public float MinPunchSpeed;

    private Vector3 punchDirection;

    public bool IntendsToPunch { get; private set; }
    public Vector3 PunchStartPosition { get; private set; }

    public void AddDebugSphere(string message)
    {
        TextMesh debugMessage = Instantiate(DebugPrefab, FistTransform.position, FistTransform.rotation).GetComponentInChildren<TextMesh>();
        debugMessage.text = message;
    }

    void Start()
    {
        IntendsToPunch = false;
    }


    private void Update()
    {
        Vector3 fistVelocityVec = OVRInput.GetLocalControllerVelocity(ControllerHandle);

        Vector3 dir = fistVelocityVec.normalized;
        float fistVelocity = new Vector2(fistVelocityVec.x, fistVelocityVec.z).magnitude;
        float dist = Vector2.Distance(new Vector2(FistTransform.position.x, FistTransform.position.z), new Vector2(ForwardTransform.position.x, ForwardTransform.position.z));


        if (!IntendsToPunch)
        {
            if (dist < PunchStartRange && Vector3.Dot(dir, ForwardTransform.forward) > 0.3f && fistVelocity > MinPunchSpeed)
            {
                //AddDebugSphere("Intends to punch.");
                IntendsToPunch = true;
                punchDirection = dir;
                PunchStartPosition = FistTransform.position;
            }
        }
        else
        {
            if (Vector3.Dot(dir, punchDirection) > 0.7f && fistVelocity > MinPunchSpeed)
            {
                if (dist > PunchEndRange)
                {
                    //AddDebugSphere("Punch completed!");
                    IntendsToPunch = false;
                    OnPunch?.Invoke(fistVelocityVec);
                }
                else
                    punchDirection = dir;
            }
            else
            {
                //AddDebugSphere("Punch canceled.");
                IntendsToPunch = false;
            }
        }
    }

}
