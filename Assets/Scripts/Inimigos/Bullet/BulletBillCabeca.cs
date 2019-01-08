using UnityEngine;
using System.Collections;

public class BulletBillCabeca : MonoBehaviour
{
    public BulletBill bullet;
    public Vector2 impulso_forca_pulo;
    void OnTriggerEnter2D(Collider2D colisor)
    {
        if (colisor.gameObject.transform.tag.StartsWith("Player") && bullet.Vivo)
        {
            MovementController vivojog = colisor.gameObject.GetComponentInParent<MovementController>();
            if (vivojog.vivo)
            {
                Game_Player.instancia.Jogador.gameObject.GetComponent<Rigidbody2D>().AddForce(impulso_forca_pulo);
            }
        }
    }


}
