using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PcScript : MonoBehaviour
{
    public Image selected;
    public GameObject store;
    public GameObject store2;
    public Transform player;

    public static int LaserGunPrice = 499;
    public static int MoneyGunPrice = 379;
    public static int MissileLauncherPrice = 99;
    public static int MachineGunPrice = 79;

    public List<WeaponScriptable> Buyable = new List<WeaponScriptable>();

    public Text LaserGunPriceUI;
    public Text MoneyGunPriceUI;
    public Text MissileLauncherPriceUI;
    public Text MachineGunPriceUI;

    private int selectedID = 0;
    private float initialY;

    private bool lojaActive = false;

    public void buy(int id)
    {
        bool metaBuy(int price)
        {
            if (price > 0)
            {
                int moneyIHave = GameObject.FindGameObjectWithTag("GameController").GetComponent<ControllerScript>().coins;
                if (moneyIHave >= price)
                {
                    GameObject.FindGameObjectWithTag("GameController").GetComponent<ControllerScript>().AddCoins(-price);
                    return true;
                }
            }
            return false;
        }
        PlayerMov playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMov>();
        switch(id)
        {
            
            case 3:
                if (metaBuy(LaserGunPrice)) {LaserGunPrice = -1;Destroy(this.gameObject);playerScript.weapons.Add(Buyable[0]);}
                
            break;

            case 2:
                if (metaBuy(MoneyGunPrice)) {MoneyGunPrice = -1;Destroy(this.gameObject);playerScript.weapons.Add(Buyable[1]);}
                //get gun
            break;

            case 1:
                if (metaBuy(MissileLauncherPrice)) {MissileLauncherPrice = -1;Destroy(this.gameObject);playerScript.weapons.Add(Buyable[2]);}
                //get gun
            break;

            case 0:
                if (metaBuy(MachineGunPrice)) {MachineGunPrice = -1;Destroy(this.gameObject);playerScript.weapons.Add(Buyable[3]);}
                //get gun
            break;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        initialY = selected.transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector2.Distance(transform.position,player.position) <= 4)
        {   
            store2.SetActive(true);
            
            if (Input.GetKeyDown(KeyCode.E))
            {
                lojaActive = !lojaActive;
            }

            store.SetActive(lojaActive);

            if (lojaActive)
            {
                selected.transform.position = new Vector3(selected.transform.position.x,initialY + 40*selectedID,0f);

                if (LaserGunPrice > 0) {LaserGunPriceUI.text = LaserGunPrice.ToString();}else{LaserGunPriceUI.text = "---";}
                if (MoneyGunPrice > 0) {MoneyGunPriceUI.text = MoneyGunPrice.ToString();}else{MoneyGunPriceUI.text = "---";}
                if (MissileLauncherPrice > 0) {MissileLauncherPriceUI.text = MissileLauncherPrice.ToString();}else{MissileLauncherPriceUI.text = "---";}
                if (MachineGunPrice > 0) {MachineGunPriceUI.text = MachineGunPrice.ToString();}else{MachineGunPriceUI.text = "---";}

                if (Input.GetKeyDown(KeyCode.UpArrow))
                {
                    if (selectedID < 3) {selectedID ++;}else{selectedID = 0;}
                }
                if (Input.GetKeyDown(KeyCode.DownArrow))
                {
                    if (selectedID > 0) {selectedID --;}else{selectedID = 3;}
                }
                if (Input.GetKeyDown(KeyCode.Q))
                {
                    buy(selectedID);
                }
            }
        }
        else {
            lojaActive = false;
            store.SetActive(false);
            store2.SetActive(false);
        }
    }
}
