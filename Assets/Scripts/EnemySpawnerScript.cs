using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnerScript : MonoBehaviour
{

    public GameObject Janinha;
    public GameObject Gafanhoto;
    public GameObject Abelha;
    public GameObject Besouro;
    public GameObject Braboleta;
    public GameObject Lagarta;

    [SerializeField] private float t = 0;
    private float t2 = 0;
    private int frequencyJaninha = 4;//em segundos
    private int frequencyGafanhoto = 6;//em segundos
    private int frequencyAbelha = 7;//em segundos
    private int frequencyBesouro = 10;//em segundos
    private int frequencyBraboleta = 8;//em segundos
    private int frequencyLagarta = 12;//em segundos

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

            if (t % frequencyBesouro == 0)
            {
                Vector2 rndPos = new Vector2(Random.Range(25,40)*rndSinal(),Random.Range(25,40)*rndSinal());
                Instantiate(Besouro,rndPos,transform.rotation);
            }

            if (t % frequencyBraboleta == 0)
            {
                Vector2 rndPos = new Vector2(Random.Range(25,40)*rndSinal(),Random.Range(25,40)*rndSinal());
                Instantiate(Braboleta,rndPos,transform.rotation);
            }

            if (t % frequencyLagarta == 0)
            {
                Vector2 rndPos = new Vector2(Random.Range(25,40)*rndSinal(),Random.Range(25,40)*rndSinal());
                Instantiate(Lagarta,rndPos,transform.rotation);
            }
        }
    }
}
