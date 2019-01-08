using UnityEngine;
using System.Collections;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Collections.Generic;

[Serializable]

public class Game_Item : MonoBehaviour
{
    public string nome;
    public int id;
    public String descricao;
    public int quantidade = 1;
    public AudioClip somColetarFruta;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
            
    }
    void OnTriggerEnter2D(Collider2D colisor)
    {
        if (IniciarColisaoJogador(colisor.gameObject))
        {
            AdicionarItem();
         //   GameObject texto = (GameObject)Instantiate(Resources.Load("PreFabs/Itens/Texto"));
          //  texto.GetComponent<TextoPopUp>().posicionar(transform);
          //  texto.GetComponent<TextoPopUp>().Texto_Item(nome);
            SoundManager.instance.PlaySingle(somColetarFruta);
            Destroy(this.gameObject, 0f);
        }
    }
    public Boolean IniciarColisaoJogador(GameObject jogador)
    {
        if (jogador.tag.StartsWith("ColiderP"))
        {
            return true;
        }
        return false;
    }
    protected virtual void AdicionarItem()
    {
        Game_Player.instancia.quantidade_item[id] += 1;
        Game_Player.instancia.QuantidadeItemFase++;
    }
}