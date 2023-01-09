using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuControllerScript : MonoBehaviour
{

    public GameObject OptionsMenu;
    public GameObject MainMenu;

    public void start()
    {
        //SceneManager.GetActiveScene().buildIndex + 1
        SceneManager.LoadScene("GameScene");
        Time.timeScale = 1f;
    }

    public void options()
    {
        OptionsMenu.SetActive(true);
        MainMenu.gameObject.SetActive(false);
    }

    public void exitOptions()
    {
        MainMenu.SetActive(true);
        OptionsMenu.gameObject.SetActive(false);
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
        PlayerPrefs.SetFloat("EffectsVolume", 0.5f);
        PlayerPrefs.SetFloat("MusicVolume", 0.5f);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            start();
        }
    }
}
