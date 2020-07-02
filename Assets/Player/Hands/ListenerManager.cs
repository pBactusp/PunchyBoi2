using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ListenerManager : MonoBehaviour
{
    public GameObject DebugPrefab;

    public GameObject FistPrefab_L;
    public GameObject FistPrefab_R;

    public GameObject EnemyObject;

    public PunchListener Listener_L;
    public PunchListener Listener_R;

    public TimeSlower TimeSlower;

    public float FistSpeedFactor;
    [Range(0f, 1f)]
    public float FistSpawnOffset;

    void Start()
    {
        Listener_L.OnPunch += Punched_L;
        Listener_R.OnPunch += Punched_R;
    }

    void Update()
    {
        if (OVRInput.Get(OVRInput.Button.PrimaryIndexTrigger) &&
            OVRInput.Get(OVRInput.Button.PrimaryHandTrigger) &&
            OVRInput.Get(OVRInput.RawButton.RIndexTrigger) &&
            OVRInput.Get(OVRInput.RawButton.RHandTrigger))
            TimeSlower.StartSlow();
        else
            TimeSlower.StopSlow();

        if (OVRInput.GetDown(OVRInput.Button.One))
        {
            EnemyObject.transform.position = Listener_L.ForwardTransform.position + Listener_L.ForwardTransform.forward * 2;
            EnemyObject.transform.LookAt(Listener_L.ForwardTransform, Vector3.up);
            EnemyObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
        }
    }


    private void Punched_L(Vector3 fistVelocityVec)
    {
        SpawnPunch(Listener_L, FistPrefab_L, fistVelocityVec);
    }
    private void Punched_R(Vector3 fistVelocityVec)
    {
        SpawnPunch(Listener_R, FistPrefab_R, fistVelocityVec);
    }

    private void SpawnPunch(PunchListener listener, GameObject fistPrefab, Vector3 fistVelocityVec)
    {
        Vector3 fistSpawnPos = Vector3.Lerp(listener.PunchStartPosition, listener.FistTransform.position, 1f - FistSpawnOffset);

        Rigidbody fistRB = Instantiate(fistPrefab, fistSpawnPos, Quaternion.LookRotation(fistVelocityVec.normalized, listener.FistTransform.up)).GetComponent<Rigidbody>();
        fistRB.velocity = fistVelocityVec * FistSpeedFactor;
    }

}
