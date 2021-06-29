using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrenadeController : MonoBehaviour
{
    public float ForwardThrust;
    public float UpwardThrust;
    public float ExplosionTimer_def;
    float ExplosionTimer;
    bool toExplode;
    private Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.AddRelativeForce(Vector3.forward *ForwardThrust);
        rb.AddRelativeForce(Vector3.up * UpwardThrust);
        ExplosionTimer = ExplosionTimer_def;
        toExplode = false;

    }

    // Update is called once per frame
    void Update()
    {
        if (toExplode)
        {
            ExplosionTimer -= Time.deltaTime;
            if(ExplosionTimer <= 0)
            {
                Destroy(this.gameObject);
            }
        }


    }

    private void OnCollisionEnter(Collision collision)
    {
        toExplode = true;
    }
}
