using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class P_Controll : MonoBehaviour
{

    // Update is called once per frame
    public static (Vector2 movDir, Vector3 weapDir, bool AtkBtn, bool DefBtn, bool SklBtn, bool Pause) PassInput()

    {
    	Vector2 movDir = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;
    	Vector2 weapDir = (Camera.main.ScreenToViewportPoint(Input.mousePosition) - Vector3.one/2)*2;
    	bool AtkBtn = Input.GetButton("Fire1");
    	bool DefBtn = Input.GetButton("Fire2");
    	bool SklBtn = Input.GetButton("Fire3");
    	bool Pause = Input.GetButton("Submit");
    	return (movDir, weapDir, AtkBtn, DefBtn, SklBtn, Pause);
    }

}
