using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class exitPauseScript : MonoBehaviour
{

    public void unPause()
    {
        GameObject.FindGameObjectWithTag("OptionsCanvasController").GetComponent<OptionsCanvasControllerScript>().unPause();
    }

    /*
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    */
}
