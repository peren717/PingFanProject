using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinionAI : MonoBehaviour
{
    int Speed;
    GameObject[] Players;
    public GameObject Target;
    bool move;
    float moveTimer;
    // Start is called before the first frame update
    void Start()
    {
        Attributes attr = GetComponent<Attributes>();
        Speed = attr.Speed;
        Players = GameObject.FindGameObjectsWithTag("Player");
        int index = Random.Range(0, Players.Length);
        Target = Players[index];
        move = true;
        moveTimer = 1.5f;
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(Target.transform);
        MoveToTarget();

    }

    void MoveToTarget()
    {
        if (move)
        {
            Vector3 targetLocation = Target.transform.position;
            transform.position = Vector3.MoveTowards(transform.position, targetLocation, Speed * Time.deltaTime);
        }
        else
        {
            if (moveTimer >= 0)
                moveTimer -= Time.deltaTime;
            else
            {
                move = true;
                moveTimer = 1.5f;
            }

        }
    }

    private void OnTriggerStay(Collider other)
    {
        GameObject _other = other.gameObject;
        if (_other.tag == "Cloud")
        {
            //if (_DamageTimer < 0)
            //{
            //    other.GetComponent<Attributes>().HP = 0;
            //    _DamageTimer = DamageTimer;
            //}
        }
    }


}
