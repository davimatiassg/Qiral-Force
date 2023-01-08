using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyAudioScript : MonoBehaviour
{
    public AudioSource audioPlayer;

    public AudioClip coinDrop;

    public AudioClip janinhaAtk;
    public AudioClip abelhaFire;
    public AudioClip gafanhotoJump;

    public AudioClip janinhaDmg;
    public AudioClip abelhaDmg;
    public AudioClip gafanhotoDmg;
    public AudioClip besouroDmg;
    public AudioClip braboletaDmg;
    public AudioClip lagartaDmg;
    public AudioClip casuloDmg;
    public AudioClip demonioDmg;

    public AudioClip janinhaDie;
    public AudioClip abelhaDie;
    public AudioClip gafanhotoDie;
    public AudioClip besouroDie;
    public AudioClip braboletaDie;
    public AudioClip lagartaDie;
    public AudioClip casuloDie;
    public AudioClip demonioDie;

    public void playAudio(string audio)
    {
        
        switch(audio)
        {
            case "coinDrop": audioPlayer.clip = coinDrop; break;

            case "janinhaAtk": audioPlayer.clip = janinhaAtk; break;
            case "gafanhotoJump": audioPlayer.clip = gafanhotoJump; break;
            case "abelhaFire": audioPlayer.clip = abelhaFire; break;

            case "janinhaDmg": audioPlayer.clip = janinhaDmg; break;
            case "abelhaDmg": audioPlayer.clip = abelhaDmg; break;
            case "gafanhotoDmg": audioPlayer.clip = gafanhotoDmg; break;
            case "besouroDmg": audioPlayer.clip = besouroDmg; break;
            case "braboletaDmg": audioPlayer.clip = braboletaDmg; break;
            case "lagartaDmg": audioPlayer.clip = lagartaDmg; break;
            case "casuloDmg": audioPlayer.clip = casuloDmg; break;
            case "demonioDmg": audioPlayer.clip = demonioDmg; break;

            case "janinhaDie": audioPlayer.clip = janinhaDie; break;
            case "abelhaDie": audioPlayer.clip = abelhaDie; break;
            case "gafanhotoDie": audioPlayer.clip = gafanhotoDie; break;
            case "besouroDie": audioPlayer.clip = besouroDie; break;
            case "braboletaDie": audioPlayer.clip = braboletaDie; break;
            case "lagartaDie": audioPlayer.clip = lagartaDie; break;
            case "casuloDie": audioPlayer.clip = casuloDie; break;
            case "demonioDie": audioPlayer.clip = demonioDie; break;
        }
        audioPlayer.Play(0);
    }

    
    // Start is called before the first frame update
    void Start()
    {
        audioPlayer.volume = PlayerPrefs.GetFloat("volume");
    }

    // Update is called once per frame
    void Update()
    {
        audioPlayer.volume = PlayerPrefs.GetFloat("volume");
    }
}
