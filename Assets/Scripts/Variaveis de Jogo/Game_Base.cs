using UnityEngine;
using System.Collections;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

[Serializable]
public class Game_Base : MonoBehaviour{
    public bool mudar_valores;
    public String Pegar_Tipo()
    {
        return this.GetType().Name;
    }
    public static void Save(Game_Base data)
    {
    }
    public static void Load(Game_Base data)
    {
    }
    public void Acordar()
    {

    }
    
}
