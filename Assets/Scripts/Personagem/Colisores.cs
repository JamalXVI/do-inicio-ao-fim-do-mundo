using UnityEngine;
using System.Collections;

public class Colisores : MonoBehaviour {

    private bool deu_colisao;

    public bool Deu_colisao
    {
        get { return deu_colisao; }
        set { deu_colisao = value; }
    }

    void OnCollisionEnter(Collision col)
    {
    }
}
