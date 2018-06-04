using UnityEngine;
using System.Collections;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

[Serializable]
public class Game_Inimigo : MonoBehaviour {
    private Transform inimigo;
    public int id;
    public bool vivo;
    public bool criado;
    private bool destruido;
    public float tempo_vida;
    public bool parar_tudo = false;
    private float duracao_paralisado;
    public AudioClip somMorrer;
	public float Duracao_Paralisado
	{
		get { return duracao_paralisado;}
		set { duracao_paralisado = value;}
	}
	
    public void Acordar()
    {
        vivo = true;
    }
    public bool Destruido
    {
        get { return destruido; }
        set { destruido = value;
            if (destruido)
            {
                Destroy(gameObject);
            
            }
        }
    }

    public Boolean IniciarColisaoJogador(GameObject jogador)
    {
        if (jogador.tag.StartsWith("Player") && vivo)
        {
            return true;
        }
        return false;
    }
    public void Destruir()
    {
        Game_Player.game_player.QuantidadeInimigosFase++;
    }
    public void Atualizar()
    {
        if (tempo_vida > 0)
        {
            tempo_vida -= Time.deltaTime;
        }

    }


    public bool VerificarMusica(Collider2D colisor)
    {
        if (colisor.tag == "Musica")
        {
            return true;
        }
        return false;
    }
    public virtual void TocandoViolao()
    {

    }

}
