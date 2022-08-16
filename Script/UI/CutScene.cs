using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CutScene : MonoBehaviour
{
    public Sprite[] sprite;

    private Image image;
    private int index;
    
    private void Start()
    {
        index = 0;
        image = GetComponent<Image>();
    }
    private void OnEnable()
    {
        index = 0;
        image = GetComponent<Image>();
    }

    void Update()
    { 
        image.sprite = sprite[index];


        
        if (Input.GetMouseButtonDown(0))
        {
            if (index == sprite.Length - 1)
            {
                LoadSceneController.LoadScene("GameHomeScene");
            }

            index++;
        }
    }
}
