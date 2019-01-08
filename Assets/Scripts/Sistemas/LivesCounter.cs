using UnityEngine;
using UnityEngine.UI;
using System.Collections;
public class LivesCounter : MonoBehaviour {

    
	void Update () {
        //Set the lives count text
        string texto = "";
        if (Game_Player.instancia.VidasTotais < 10)
        {
            texto = "0" + Game_Player.instancia.VidasTotais.ToString();   
        }else{
            texto = Game_Player.instancia.VidasTotais.ToString();
        }
        GetComponent  <Text>().text = texto;
	    
	}
}
