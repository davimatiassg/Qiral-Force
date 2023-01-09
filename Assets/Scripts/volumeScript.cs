using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class volumeScript : MonoBehaviour
{

    public Slider sliderval;

    public void changeVolumeEffects()
    {
        PlayerPrefs.SetFloat("EffectsVolume", sliderval.value);
    }

    public void changeVolumeMusic()
    {
        PlayerPrefs.SetFloat("MusicVolume", sliderval.value);
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
