using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    public Text myFruitCount;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    // Update is called once per frame
    void Update()
    {
        if (myFruitCount)
        {
            myFruitCount.text = "Fruits Collected = " + Counting.numFruitsCollected;
        }
    }

    public void QuitGameButton()
    {
        #if UNITY_EDITOR            UnityEditor.EditorApplication.isPlaying = false;        #else            Application.Quit();        #endif
    }

    public void FirstPersonButton()
    {
        SceneManager.LoadScene("FirstPerson");
    }
    public void ThirdPersonButton()
    {
        SceneManager.LoadScene("ThirdPerson");
    }

    public void RestartFirstPersonButton()
    {
        Counting.HP = 100;
        Counting.numFruitsCollected = 0;
        SceneManager.LoadScene("FirstPerson");
    }
    public void RestartThirdPersonButton()
    {
        Counting.HP = 100;
        Counting.numFruitsCollected = 0;
        SceneManager.LoadScene("ThirdPerson");
    }
}

