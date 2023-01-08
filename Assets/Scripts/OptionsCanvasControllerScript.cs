using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionsCanvasControllerScript : MonoBehaviour
{

    public GameObject OptionsCanvas;
    public GameObject MainCanvas;

    public void unPause()
    {
        Time.timeScale = 1f;
        MainCanvas.SetActive(true);
        OptionsCanvas.SetActive(false);
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            unPause();
        }
    }
}
