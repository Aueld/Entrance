using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStartPoint : MonoBehaviour
{
    private GameObject player;
    
    private void Start()
    {
        player = GameObject.Find("Player");

        player.transform.position = gameObject.transform.position;
    }

}
