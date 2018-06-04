using UnityEngine;
using System.Collections;
using System.Collections;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Collections.Generic;

[Serializable]

public class Game_Itens : Game_Base {
    public Game_Item[] itens;
    public static Game_Itens game_itens;
	// Use this for initialization
	void Awake () {
        if (Game_Itens.game_itens == null)
        {
            DontDestroyOnLoad(gameObject);
            Game_Itens.game_itens = this;
        }
        else if (Game_Itens.game_itens != this)
        {

            Destroy(gameObject);
        }

        base.Acordar();

	}
/*
	// Update is called once per frame
	void Buscar_Itens () {
        Game_Itens.game_itens.itens = new Game_Item[999];
        //Android= "jar:file://" + Application.dataPath + "!/assets/";
        string[] arquivos = System.IO.Directory.GetFiles(Application.dataPath + "/Resources/Prefabs/Itens/", "*.Prefab*");
        if (arquivos.Length > 0)
        {
            foreach (string item in arquivos)
            {
                if (!item.Contains("meta"))
                {
                    string[] nomes = item.Split(new string[]{"Resources/Prefabs/Itens/"},StringSplitOptions.RemoveEmptyEntries);
                    string[] nome =  nomes[1].Split(new string[] { ".prefab" }, StringSplitOptions.RemoveEmptyEntries);

                    GameObject item_o = Resources.Load("PreFabs/Itens/" + nome[0]) as GameObject;
                    Game_Itens.game_itens.itens[item_o.GetComponent<Game_Item>().id] = item_o.GetComponent<Game_Item>();
                }
            }
        }
	}
 */
}
