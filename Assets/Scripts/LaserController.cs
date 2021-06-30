using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserController : MonoBehaviour
{
    public float LaserSpeed;
    public float DestoryTimer;
    // Start is called before the first frame update
    void Start()
    {
        transform.eulerAngles = new Vector3(
        0f,
        transform.eulerAngles.y,
        transform.eulerAngles.z
        );
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += transform.TransformDirection(Vector3.forward) * LaserSpeed *Time.deltaTime;
        if (DestoryTimer >= 0)
            DestoryTimer -= Time.deltaTime;
        else
            Destroy(this.gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        Destroy(this.gameObject);
        if(collision.gameObject.tag == "Character")
        {
            Destroy(collision.gameObject);
        }
    }
}
