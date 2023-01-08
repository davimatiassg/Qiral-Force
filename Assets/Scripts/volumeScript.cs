using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class volumeScript : MonoBehaviour
{

    public Slider sliderval;

    public void changeVolume()
    {
        PlayerPrefs.SetFloat("volume", sliderval.value);
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
