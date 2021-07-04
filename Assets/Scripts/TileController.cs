using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileController : MonoBehaviour
{
    public int Sid;
    public bool SpawnNewTile;
    public List<GameObject> Minion_v1;
    public List<GameObject> Marksman_v1;
    public List<GameObject> Assasin_v1;
    public List<GameObject> Brute_v1;
    public GameObject Tile;
    public GameObject NextTile;
    public GameObject Boss_v1;
    public GameObject Camera;
    public GameObject LightPole;
    public List<GameObject> Crates;
    public List<GameObject> Obstacles_S;

    public float EnemySpawnInterval;
    public int Difficulty;
    public bool Active;
    public int gid;
    public bool bossRoom;
    public int MaxObstacle_SNum;
    public int MaxCrateNum;

    float _EnemySpawnInterval;
    List<string> EnemySet;
    List<GameObject> SpawnedObjects;
    float destoryTimer = 5f;
    bool setToDestroy = false;


    // Start is called before the first frame update
    void Start()
    {
        SpawnedObjects = new List<GameObject>();
        Active = false;
        SpawnObstacles();
        _EnemySpawnInterval = EnemySpawnInterval;
        EnemySet = new List<string> {
            "Minion Horde", //Lots of Minions
            "blitzkrieg",   //Minons in the middle, Assasins on sides
            "Superior Firepower",    //few Marksman
            "Combined Arms", //Brute in the middle, followed by marksman
            "Paratroopers",  //Randomly drops few assasin
            //TODO: add more variants.
        };


        //TODO: Change this
        Difficulty = 1;
        Sid=1;

        if (Sid == 1)
            SpawnLights();
    }


    // Update is called once per frame
    void Update()
    {
        if (SpawnNewTile)
        {
            NextTile = Instantiate(Tile, transform.position + new Vector3(0, 0, 200), Quaternion.identity);
            NextTile.GetComponent<TileController>().SpawnNewTile = false;
            NextTile.GetComponent<TileController>().gid = gid += 1;
            NextTile.name = "Ground" + (gid + 1).ToString();
            SpawnNewTile = false;
        }

        //removeWhenOutsideView();
        if (!Active && CameraWithinGround())
        {
            Active = true;
            LetNextTileSpawnNewTile();
        }
        if (Active && !CameraWithinGround())
        {
            Active = false;
            setToDestroy = true;
        }

        if (setToDestroy)
        {
            destoryTimer -= Time.deltaTime;
            if (destoryTimer <= 0)
                Destroy(gameObject);
        }

        void LetNextTileSpawnNewTile()
        {
            NextTile.GetComponent<TileController>().SpawnNewTile = true;
        }
        //TODO: fix this

        if (Active)
        {
            if (_EnemySpawnInterval > 0)
            {
                _EnemySpawnInterval -= Time.deltaTime;
            }

            if (_EnemySpawnInterval <= 0)
            {
                SpawnEnemy(Difficulty);
                _EnemySpawnInterval = EnemySpawnInterval;
            }

        }


    }
    bool CameraWithinGround()
    {
        Vector3 location = transform.position;
        return Camera.transform.position.z < location.z + 100 && Camera.transform.position.z > location.z - 100;
    }

    void SpawnEnemy(int difficulty)
    {
        int index = Random.Range(0, EnemySet.Count);
        string E_type = EnemySet[index];
        Debug.Log("Enemy Set to Spawn: " + E_type);
        switch (E_type)
        {
            case "Minion Horde":
                SpawnMinionHorde();
                break;
            case "blitzkrieg":
                SpawnBlitzkrieg();
                break;
            case "Superior Firepower":
                SpawnSuperiorFirepower();
                break;
            case "Combined Arms":
                SpawnCombinedArms();
                break;
            case "Paratroopers":
                SpawnParatroopers();
                break;

        }


    }

    void SpawnMinionHorde()
    {
        int index;
        GameObject Minion;
        Vector3 location = Camera.transform.position;

        for (int i = 0; i < 9; i++)
        {
            index = Random.Range(0, Minion_v1.Count);
            Minion = Minion_v1[index];
            Vector3 E_location = new Vector3(-40 + i * 10, 1f, location.z + 200);
            Instantiate(Minion, E_location, transform.rotation * Quaternion.Euler(0f, 180f, 0f));
        }
    }

    void SpawnBlitzkrieg()
    {
        int index = Random.Range(0, Assasin_v1.Count); ;
        GameObject Assasin = Assasin_v1[index];
        Vector3 location = Camera.transform.position;
        GameObject Minion;

        Instantiate(Assasin, new Vector3(-40f, 1f, location.z + 200), transform.rotation * Quaternion.Euler(0f, 180f, 0f));
        Instantiate(Assasin, new Vector3(40f, 1f, location.z + 200), transform.rotation * Quaternion.Euler(0f, 180f, 0f));

        for (int i = 0; i < 3; i++)
        {
            index = Random.Range(0, Minion_v1.Count);
            Minion = Minion_v1[index];
            Vector3 E_location = new Vector3(-10f + i * 10f, 1f, location.z + 200);
            Instantiate(Minion, E_location, transform.rotation * Quaternion.Euler(0f, 180f, 0f));
        }
    }

    void SpawnSuperiorFirepower()
    {
        int index;
        GameObject Marksman;
        Vector3 location = Camera.transform.position;

        for (int i = 0; i < 5; i++)
        {
            index = Random.Range(0, Marksman_v1.Count);
            Marksman = Marksman_v1[index];
            Vector3 E_location = new Vector3(-40 + i * 20, 1f, location.z + 200);
            Instantiate(Marksman, E_location, transform.rotation * Quaternion.Euler(0f, 180f, 0f));
        }
    }

    void SpawnCombinedArms()
    {
        GameObject Brute;
        GameObject Minion;
        int index;
        Vector3 location = Camera.transform.position;

        for (int i = 0; i < 3; i++)
        {
            Vector3 E_location = new Vector3(-30 + i * 30, 1.5f, location.z + 200);

            index = Random.Range(0, Brute_v1.Count);
            Brute = Brute_v1[index];
            Instantiate(Brute, E_location, transform.rotation * Quaternion.Euler(0f, 180f, 0f));

            index = Random.Range(0, Minion_v1.Count);
            Minion = Minion_v1[index];
            Instantiate(Minion, E_location + new Vector3(-2f, 0f, 2f), transform.rotation * Quaternion.Euler(0f, 180f, 0f));

            index = Random.Range(0, Minion_v1.Count);
            Minion = Minion_v1[index];
            Instantiate(Minion, E_location + new Vector3(2f, 0f, 2f), transform.rotation * Quaternion.Euler(0f, 180f, 0f));
        }
    }

    void SpawnParatroopers()
    {
        Vector3 location = Camera.transform.position;
        int index = Random.Range(0, Assasin_v1.Count);
        GameObject Assasin = Assasin_v1[index];
        Vector3 E_location = new Vector3(-30, 10f, location.z + 50);
        Instantiate(Assasin, E_location + new Vector3(2f, 0f, 2f), transform.rotation * Quaternion.Euler(0f, 180f, 0f));
        index = Random.Range(0, Assasin_v1.Count);
        Assasin = Assasin_v1[index];
        E_location = new Vector3(30, 10f, location.z + 50);
        Instantiate(Assasin, E_location + new Vector3(2f, 0f, 2f), transform.rotation * Quaternion.Euler(0f, 180f, 0f));
    }

    void SpawnObstacles()
    {
        Vector3 O_location;

        int index;

        for (int i = 0; i < MaxCrateNum; i++)
        {
            O_location = new Vector3(Random.Range(-50f, 50f), 0.5f, transform.position.z + Random.Range(-100f, 100f));
            index = Random.Range(0, Crates.Count);
            Instantiate(Crates[index], O_location, Quaternion.identity);
        }

        for (int i = 0; i < MaxObstacle_SNum; i++)
        {
            O_location = new Vector3(Random.Range(-50f, 50f), 0.5f, transform.position.z + Random.Range(-100f, 100f));
            index = Random.Range(0, Obstacles_S.Count);
            Instantiate(Obstacles_S[index], O_location, Quaternion.identity);
        }
    }

    void SpawnLights()
    {
        for(int i = 0; i < 6; i++)
        {
            Instantiate(LightPole, new Vector3(45f,3f,transform.position.z - 90f + 30*i), Quaternion.identity);
            Instantiate(LightPole, new Vector3(-45f, 3f, transform.position.z - 90f + 30 * i), Quaternion.identity);
        }
    }
}

