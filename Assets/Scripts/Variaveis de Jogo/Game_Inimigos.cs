using UnityEngine;
using System.Collections;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Collections.Generic;

[Serializable]
public class Game_Inimigos : Game_Base {
    public static Game_Inimigos  game_inimigos;
    public Transform[] inimigos_trans;
    public String[] inimigos_tipos;
    public Game_Inimigo[] inimigos;
    public int ult_id;
    void Awake()
    {

        if (Game_Inimigos.game_inimigos == null)
        {
            DontDestroyOnLoad(gameObject);
            Game_Inimigos.game_inimigos = this;
        }
        else if (Game_Inimigos.game_inimigos != this)
        {

            Destroy(gameObject);
        }
        Acordar();
        Adicionar_Inimigos();
    }
    public void Adicionar_Inimigos()
    {
        ult_id = 0;
        List<Game_Inimigo> ids = new List<Game_Inimigo>();
        Game_Inimigo[] inims = GameObject.FindObjectsOfType<Game_Inimigo>();
        foreach (Game_Inimigo ini in inims)
        {
            if (ini.id > ult_id)
	        {
		        ult_id = ini.id;
	        }
            if (Game_Inimigos.game_inimigos.inimigos[ini.id] &&
                ids.Contains(ini))
            {
                print(Game_Inimigos.game_inimigos.inimigos_trans[ini.id].name);
                print(ini.name);
                print("ERRO! Inigo com o ID " + ini.id + ", já existe por favor corrigir!");
                Adicionar_Inimigo(ini);
                continue;
            }
            Game_Inimigos.game_inimigos.inimigos[ini.id] = ini;
            Game_Inimigos.game_inimigos.inimigos_tipos[ini.id] = ini.tag;
            Game_Inimigos.game_inimigos.inimigos_trans[ini.id] = ini.transform;
            ids.Add(ini);
        }

    }
    public void Adicionar_Inimigo(Game_Inimigo inimigo)
    {
        ult_id++;
        inimigo.id = ult_id;
        Game_Inimigos.game_inimigos.inimigos[inimigo.id] = inimigo;
        Game_Inimigos.game_inimigos.inimigos_tipos[inimigo.id] = inimigo.tag;
        Game_Inimigos.game_inimigos.inimigos_trans[inimigo.id] = inimigo.transform;

    }
    public void Carregar_Inimigos()
    {
        Game_Inimigo[] inims = GameObject.FindObjectsOfType<Game_Inimigo>();
        foreach (Game_Inimigo ini in inims)
        {
            if (Game_Inimigos.game_inimigos.inimigos[ini.id] != null)
            {
                if (Game_Inimigos.game_inimigos.inimigos[ini.id].vivo)
                {
                     ini.transform.position = Game_Inimigos.game_inimigos.inimigos_trans[ini.id].position;
                     Game_Inimigos.game_inimigos.inimigos[ini.id] = ini;
                     Game_Inimigos.game_inimigos.inimigos_trans[ini.id] = ini.GetComponent<Transform>();
                     Game_Inimigos.game_inimigos.inimigos_tipos[ini.id] = ini.gameObject.tag;
                }
                else
                {
                    ini.Destruido = true;
                }

            }
            else
            {
                ini.Destruido = true;
            }

        }
        foreach (Game_Inimigo ini in Game_Inimigos.game_inimigos.inimigos)
        {
            if (ini.criado && ini.vivo)
            {
                Instanciar_Inimigo(ini);
            }
        }

    }
    void Instanciar_Inimigo(Game_Inimigo ini)
    {
        switch (Game_Inimigos.game_inimigos.inimigos_tipos[ini.id])
        {
            case "Bullet":
                GameObject bala = (GameObject)Instantiate(Resources.Load("/Prefabs/Mapa_Teste/Corpo"),
                    Game_Inimigos.game_inimigos.inimigos_trans[ini.id].position, Game_Inimigos.game_inimigos.inimigos_trans[ini.id].rotation);
                Game_Inimigos.game_inimigos.inimigos[ini.id] = bala.GetComponent<Game_Inimigo>();
                Game_Inimigos.game_inimigos.inimigos_trans[ini.id] = bala.transform;
                Game_Inimigos.game_inimigos.inimigos_tipos[ini.id] = bala.tag;

                break;
            default:
                break;
        }
    }
    void Acordar()
    {
        Game_Inimigos.game_inimigos.inimigos = new Game_Inimigo[999];
        Game_Inimigos.game_inimigos.inimigos_tipos = new String[999];
        Game_Inimigos.game_inimigos.inimigos_trans = new Transform[999];
    }
    public static void Save()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/" + Game_Inimigos.game_inimigos.GetType().Name + ".dat");
        bf.Serialize(file, Game_Inimigos.game_inimigos);
        file.Close();
    }
    public static void Load()
    {
        if (File.Exists(Application.persistentDataPath + "/" + Game_Inimigos.game_inimigos.Pegar_Tipo() + ".dat"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/" + Game_Inimigos.game_inimigos.Pegar_Tipo() + ".dat", FileMode.Open);
            Game_Inimigos.game_inimigos = (Game_Inimigos)bf.Deserialize(file);
        }
    }


}
