using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePlayer : MonoBehaviour
{

    public GameObject MainCanvas;
    public GameObject GameOver;

    public float hp = 50f;
    
    public void takeDmg(float dmg,Vector2 knb)
    {
        hp -= dmg;
        GameObject.FindGameObjectWithTag("GameController").GetComponent<ControllerScript>().updatePlayerHp(hp);
    }

    // Update is called once per frame
    void Update()
    {
        if (hp <= 0f)
        {
            hp = 0f;
            GameObject.FindGameObjectWithTag("GameController").GetComponent<ControllerScript>().updatePlayerHp(hp);

            Time.timeScale = 0f;
            MainCanvas.SetActive(false);
            GameOver.SetActive(true);
            Cursor.visible = true;
        }
    }
}
