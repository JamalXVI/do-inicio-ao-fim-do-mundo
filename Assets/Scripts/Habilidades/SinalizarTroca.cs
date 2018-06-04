using UnityEngine;
using System.Collections;

public class SinalizarTroca : MonoBehaviour {
    [SerializeField]
    private bool socolinha = false;
    [SerializeField]
    private bool primitivo = false;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    void OnCollisionEnter2D(Collision2D col)
    {
        GameObject objeto = col.gameObject;
        bool ativarSinalizar = true;
        VerificarSinalizar(objeto, ativarSinalizar);
    }
    void OnCollisionExit2D(Collision2D col)
    {
        GameObject objeto = col.gameObject;
        bool ativarSinalizar = false;
        VerificarSinalizar(objeto, ativarSinalizar);
    }
    private void VerificarSinalizar(GameObject objeto, bool ativarSinalizar)
    {
        if (VerificarJogador(objeto))
        {
            if (VerificarSocolinha())
            {
                Game_Temp.instancia.SinalizarSocolinha = ativarSinalizar;
            }
            else
            {
                Game_Temp.instancia.SinalizarSocolinha = false;

            }
            if (VerificarPrimitivo())
            {
                Game_Temp.instancia.SinalizarPrimitivo = ativarSinalizar;
            }
            else
            {
                Game_Temp.instancia.SinalizarPrimitivo = false;

            }
        }
    }
    private bool VerificarSocolinha()
    {
        if (Game_Player.game_player.Jogador.name.Contains("Socolinha") && socolinha)
        {
            return true;
        }
        return false;
    }
    private bool VerificarPrimitivo()
    {
        if (Game_Player.game_player.Jogador.name.Contains("Primitivo") && primitivo)
        {
            return true;
        }
        return false;
    }
    
    private bool VerificarJogador(GameObject objeto)
    {
        if (objeto == Game_Player.game_player.Jogador || objeto.transform.IsChildOf(Game_Player.game_player.Jogador.transform))
        {
            return true;
        }
        return false;
    }
}
