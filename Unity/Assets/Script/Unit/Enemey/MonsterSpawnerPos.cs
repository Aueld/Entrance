using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSpawnerPos : MonoBehaviour
{

    private GameObject monsterSpawner;

    private void Start()
    {
        monsterSpawner = GameObject.Find("Spawner");

        monsterSpawner.transform.position = gameObject.transform.position;
    }
}
