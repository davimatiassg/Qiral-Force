using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class kk : MonoBehaviour
{

    public AudioSource audioPlayer;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        audioPlayer.volume = PlayerPrefs.GetFloat("MusicVolume");
    }
}
