using UnityEngine;
using System.Collections;

public class Pedra : Game_Inimigo {

    public PedraCorpo buletbill;
    public PedraCabeca bulletbillcabeca;
    void Awake()
    {
        base.Acordar();
    }
    void Update()
    {
        base.Atualizar();
    }

}
