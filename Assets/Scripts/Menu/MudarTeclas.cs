﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class MudarTeclas : MonoBehaviour {
    [SerializeField]
    private Text pulo, direita, esquerda, correr, habilidade, trocar, esc;

    private GameObject teclaAtual;
	// Use this for initialization
	void Start () {

        DefinirTeclas();
    }

    private void DefinirTeclas()
    {
        pulo.text = Game_Player.game_player.Teclas["pulo"].ToString();
        direita.text = Game_Player.game_player.Teclas["direita"].ToString();
        esquerda.text = Game_Player.game_player.Teclas["esquerda"].ToString();
        correr.text = Game_Player.game_player.Teclas["correr"].ToString();
        habilidade.text = Game_Player.game_player.Teclas["habilidade"].ToString();
        trocar.text = Game_Player.game_player.Teclas["troca"].ToString();
        esc.text = Game_Player.game_player.Teclas["esc"].ToString();
    }
	
	// Update is called once per frame
	void Update () {
        foreach (var tecla in Game_Player.game_player.Teclas.Values)
        {
            if (Input.GetKeyDown(tecla))
            {
            }
        }
	}
    void OnGUI() {
        if (teclaAtual != null)
        {
            Event e = Event.current;;
            if (e.isKey)
            {
                Game_Player.game_player.Teclas[teclaAtual.name.ToLower()] = e.keyCode;
                teclaAtual.transform.GetChild(0).GetComponent<Text>().text = e.keyCode.ToString();
                teclaAtual = null;
            }
        }
    }
    public void MudarTecla  (GameObject clicado)
    {
        if (clicado.transform.tag.Contains("Teclas"))
        {
            teclaAtual = clicado;
        }
    }
    public void Default()
    {
        Game_Player.game_player.DefinicoesTeclas();
        DefinirTeclas();
    }
}