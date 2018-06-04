using UnityEngine;
using System.Collections;

public class PedraCabeca : MonoBehaviour {

    public PedraCorpo bullet;
    public Vector2 impulso_forca_pulo;
    private bool ja_pulou = false;
    private float delay = 0.8f;
    private float contador = 0f;
    void Update()
    {
        if (ja_pulou)
        {
            contador += Time.deltaTime;
            if (contador >= delay)
            {
                contador = 0;
                ja_pulou = false;
            }
        }
    }
    void OnTriggerEnter2D(Collider2D colisor)
    {
        if (colisor.gameObject.transform.tag.StartsWith("ColiderP") && bullet.Vivo)
        {
            MovementController vivojog = colisor.gameObject.GetComponentInParent<MovementController>();
            if (vivojog.vivo && !ja_pulou)
            {
                contador = 0f;
                ja_pulou = true;
                //bullet.Destruir();
                Game_Player.game_player.Jogador.gameObject.GetComponent<Rigidbody2D>().AddForce(impulso_forca_pulo);
            }
        }
    }
}
