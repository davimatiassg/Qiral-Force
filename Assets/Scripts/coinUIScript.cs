using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class coinUIScript : MonoBehaviour
{

    private float rCoin = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Coin spinning
        transform.localScale = new Vector3(Mathf.Sin(rCoin)*0.35f,0.35f,1);
        if (rCoin >= Mathf.PI*2) {rCoin = 0;}
        rCoin += 4*Time.deltaTime;
    }
}
