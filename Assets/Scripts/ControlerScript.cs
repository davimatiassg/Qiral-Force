using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControllerScript : MonoBehaviour
{
    private float playerHp;
    private float coreHp;
    public int coins = 0;
    public Text coinsUI;
    public Text coreUI;
    public Text playerUI;

    public void AddCoins(int qnt)
    {
        coins += qnt;
        coinsUI.text = "Coins: " + coins.ToString();
    }

    public void updateCoreHp(float hp)
    {
        coreUI.text = "CoreHp: " + ((int) hp).ToString();
    }

    public void updatePlayerHp(float hp)
    {
        playerUI.text = "playerHp: " + ((int) hp).ToString();
    }

    // Start is called before the first frame update
    void Start()
    {
        AddCoins(0);
        updateCoreHp(GameObject.FindGameObjectWithTag("Core").GetComponent<GameWeb>().hp);
        updatePlayerHp(GameObject.FindGameObjectWithTag("Player").GetComponent<GamePlayer>().hp);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
