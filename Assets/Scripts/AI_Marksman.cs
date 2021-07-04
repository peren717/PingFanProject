using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_Marksman : MonoBehaviour
{
    GameObject Target;
    GameObject[] Players;
    int Speed;

    public GameObject Bullet;
    public GameObject EmptyTarget;
    GameObject _EmptyTarget;
    public float FireInverval;
    public float Accuracy;
    float _FireInverval;




    // Start is called before the first frame update
    void Start()
    {
        Attributes attr = GetComponent<Attributes>();
        Speed = attr.Speed;
        Players = GameObject.FindGameObjectsWithTag("Player");
        int index = Random.Range(0, Players.Length);
        Target = Players[index];
        _FireInverval = FireInverval +Random.Range(0,FireInverval);
        _EmptyTarget = Instantiate(EmptyTarget, Target.transform.position, Quaternion.identity);


    }

    // Update is called once per frame
    void Update()
    {
        transform.position += new Vector3(0, 0, -Speed * Time.deltaTime);
        _EmptyTarget.transform.position = Target.transform.position;
        transform.LookAt(_EmptyTarget.transform);

        if (_FireInverval >= 0)
        {
            _FireInverval -= Time.deltaTime;
        }
        else
        {
            _EmptyTarget.transform.position += new Vector3(Random.Range(-Accuracy, Accuracy), 0f, Random.Range(-Accuracy, Accuracy));
            transform.LookAt(_EmptyTarget.transform);
            Fire();
            _FireInverval = FireInverval;
        }
    }

    void Fire()
    {
        Vector3 fire1Offset = transform.forward * 3f + transform.right * 0.5f;
        Instantiate(Bullet, transform.position + fire1Offset, transform.rotation);

    }
}
