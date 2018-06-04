using UnityEngine;
using System.Collections;

public class Bullet : Game_Inimigo {

    public BulletBill buletbill;
    public BulletBillCabeca bulletbillcabeca;
    void Awake()
    {
        base.Acordar();
    }
    void Update()
    {
        base.Atualizar();
    }
}
