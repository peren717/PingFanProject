using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudControl : MonoBehaviour
{
    public float couldSpeed = 1f;
    public float DamageTimer;
    float _DamageTimer;

    // Start is called before the first frame update
    void Start()
    {
        _DamageTimer = DamageTimer;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += new Vector3(0, 0, couldSpeed * Time.deltaTime);
        if (_DamageTimer >= 0)
            _DamageTimer -= Time.deltaTime;
    }

    //private void OnCollisionStay(Collision collision)
    //{
    //    GameObject other = collision.gameObject;
    //    if(other.tag == "Character")
    //    {
    //        if (_DamageTimer < 0)
    //        {
    //            other.GetComponent<Attributes>().HP = 0;
    //            _DamageTimer = DamageTimer;
    //        }
    //    }
    //}


}
