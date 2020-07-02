using UnityEngine;

public class DespawnScript : MonoBehaviour
{
    public float DespawnDistance;
    private Vector3 startPos;

    void Start()
    {
        startPos = transform.position;
    }

    void Update()
    {
        if (Vector3.SqrMagnitude(transform.position - startPos) > DespawnDistance * DespawnDistance)
            Destroy(gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        Destroy(gameObject);
    }
}
