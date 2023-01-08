using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MidGameExitScript : MonoBehaviour
{

    public void TryAgain()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("GameScene");
        Time.timeScale = 1f;
    }

    public void exitToMenu()
    {
        SceneManager.LoadScene("MenuScene");
        Time.timeScale = 1f;
    }

    public void exit()
    {
        //Weird code goes brrrrrrr
        #if UNITY_STANDALONE
            Application.Quit();
        #endif
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #endif
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
