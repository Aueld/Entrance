using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GUIMenu : MonoBehaviour
{
    private bool check = false;

    public GameObject canvas;

    void Start()
    {
        GameManager.Instance.InitGame();
        canvas.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !check)
        {
            canvas.SetActive(true);
            check = true;
            GameManager.Instance.PauseGame();
            
        } else if(Input.GetKeyDown(KeyCode.Escape) && check)
        {
            canvas.SetActive(false);
            check = false;
            GameManager.Instance.ContinueGame();
        }
    }
}
