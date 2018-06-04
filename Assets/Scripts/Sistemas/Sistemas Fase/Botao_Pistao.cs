using UnityEngine;
using System.Collections;

public class Botao_Pistao : MonoBehaviour {

    [SerializeField]
    private Animator pistao;
    private Vector2 posicaoIni;
    [SerializeField]
    private bool inverter = false;
	// Use this for initialization
	void Start () {
        posicaoIni = transform.position;
	
	}
	
	// Update is called once per frame
	void Update () {
        if (posicaoIni.y > transform.position.y + 1 || posicaoIni.x > transform.position.x + 1 ||
            posicaoIni.y < transform.position.y - 1 || posicaoIni.x < transform.position.x - 1)
        {
            if (!inverter)
            {
                DescerPistao();

            }
            else
            {
                SubirPistao();
            }
        }
        else if (posicaoIni.y >= transform.position.y || posicaoIni.y <= transform.position.y || posicaoIni.x >= transform.position.x ||
            posicaoIni.x <= transform.position.x)
        {
            if (!inverter)
            {
              SubirPistao();

            }
            else
            {
                DescerPistao();
            }
        }
	}
    /*
    void OnCollisionEnter2D(Collision2D col)
    {
        if (!col.transform.tag.Contains("Chao"))//(Game_Player.game_player.VerificarSeEhPrincipal(col.transform))
        {
            DescerPistao();
        }
    }
    void OnCollisionExit2D(Collision2D col)
    {
        if (!col.transform.tag.Contains("Chao"))//(Game_Player.game_player.VerificarSeEhPrincipal(col.transform))
        {
            SubirPistao();
        }
    }
     */
    private void DescerPistao()
    {
        pistao.SetBool("Botao", true);
    }
    private void SubirPistao()
    {
        pistao.SetBool("Botao", false);
    }
}
