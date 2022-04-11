using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : Unit
{
    private static WaitForSeconds wait = new WaitForSeconds(3f);

    public GameObject parent;
    public GameObject Enemy;

    private bool boss = false;

    private List<GameObject> Enemies = new List<GameObject>();

    void Start()
    {
        for (int i = 0; i < 10; i++)
        {
            Pos = GameObject.Find("Player").transform.position;

            Enemies.Add(Instantiate(Enemy, Pos, Quaternion.identity));
            Enemies[i].transform.parent = parent.transform;
            Enemies[i].SetActive(false);

        }

        StartCoroutine(SpawnerUpdater());
    }

    private IEnumerator SpawnerUpdater()
    {
        while (!boss)
        {
            yield return wait;

            //RandX = Random.Range(0.5f, 5);
            //RandY = Random.Range(0.5f, 5);
            //Rand = Random.Range(-2, 1);

            //if (Rand == -2)
            //    Rand = -1;
            //if (Rand == 0)
            //    Rand = 1;

            //Pos = new Vector2(RandX * Rand, RandY * Rand);

            for(int i = 0; i < 10; i++)
            {
                if (!Enemies[i].activeSelf)
                {
                    Enemies[i].SetActive(true);
                    break;
                }
            }

            //enabled = true;

            //Instantiate(Enemy, Pos, Quaternion.identity).transform.parent = parent.transform; // transform.rotation);// );
            

        }
    }
}
