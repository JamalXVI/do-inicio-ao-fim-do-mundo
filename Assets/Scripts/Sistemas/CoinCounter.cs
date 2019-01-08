using UnityEngine;
using UnityEngine.UI;
using System.Collections;
public class CoinCounter : MonoBehaviour {

    
    void Update()
    {
        string texto = "";
        if (Game_Player.instancia.Moedas < 10)
        {
            texto = "0" + Game_Player.instancia.Moedas.ToString();
        }
        else
        {
            texto = Game_Player.instancia.Moedas.ToString();
        }
        GetComponent<Text>().text = texto;

    }
}
