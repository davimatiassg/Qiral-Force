using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControllerScript : MonoBehaviour
{

    public GameObject OptionsCanvas;
    public GameObject MainCanvas;

    private float playerHp;
    private float coreHp;
    private float playerHpMax;
    private float coreHpMax;
    public int coins = 0;
    public Text coinsUI;
    public Text coreUI;
    public Text playerUI;

    public Image coreHealthBar;
    public Image playerHealthBar;

    public AudioSource audioPlayer;
    public AudioClip coinGet;

    public void playAudio(string audio)
    {
        switch(audio)
        {
            case "coinGet": audioPlayer.clip = coinGet; break;
        }
        audioPlayer.Play(0);
    }

    public void AddCoins(int qnt)
    {
        coins += qnt;
        coinsUI.text = coins.ToString() + "                 C o i n s  ";
        if (qnt > 0) {playAudio("coinGet");}
    }

    public void updateCoreHp(float hp)
    {
        coreUI.text = "CoreHp: " + ((int) hp).ToString();
        coreHealthBar.fillAmount = hp/coreHpMax;
    }

    public void updatePlayerHp(float hp)
    {
        playerUI.text = "playerHp: " + ((int) hp).ToString();
        playerHealthBar.fillAmount = hp/playerHpMax;
    }

    // Start is called before the first frame update
    void Start()
    {
        audioPlayer.volume = PlayerPrefs.GetFloat("volume");

        AddCoins(0);
        coreHpMax = GameObject.FindGameObjectWithTag("Core").GetComponent<GameWeb>().hp;
        playerHpMax = GameObject.FindGameObjectWithTag("Player").GetComponent<GamePlayer>().hp;
        updateCoreHp(coreHpMax);
        updatePlayerHp(playerHpMax);
    }

    // Update is called once per frame
    void Update()
    {
        audioPlayer.volume = PlayerPrefs.GetFloat("volume");
        
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            MainCanvas.SetActive(false);
            OptionsCanvas.SetActive(true);
            Time.timeScale = 0f;
        }
    }
}
