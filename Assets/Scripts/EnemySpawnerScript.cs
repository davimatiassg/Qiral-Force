using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnerScript : MonoBehaviour
{

    public GameObject Janinha;
    public GameObject Gafanhoto;
    public GameObject Abelha;

    [SerializeField] private float t = 0;
    private float t2 = 0;
    public int frequencyJaninha = 3;//em segundos
    public int frequencyGafanhoto = 5;//em segundos
    public int frequencyAbelha = 4;//em segundos

    int rndSinal() {if (Random.Range(0,2) == 0) {return 1;}else{return -1;}}

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        t2 += Time.deltaTime;
        if (t2 > t)
        { //Updates each second
            t ++;

            if (t % frequencyJaninha == 0)
            {
                Vector2 rndPos = new Vector2(Random.Range(25,40)*rndSinal(),Random.Range(25,40)*rndSinal());
                Instantiate(Janinha,rndPos,transform.rotation);
            }

            if (t % frequencyGafanhoto == 0)
            {
                Vector2 rndPos = new Vector2(Random.Range(25,40)*rndSinal(),Random.Range(25,40)*rndSinal());
                Instantiate(Gafanhoto,rndPos,transform.rotation);
            }

            if (t % frequencyAbelha == 0)
            {
                Vector2 rndPos = new Vector2(Random.Range(25,40)*rndSinal(),Random.Range(25,40)*rndSinal());
                Instantiate(Abelha,rndPos,transform.rotation);
            }
        }
    }
}
