using UnityEngine;
using System.Collections;

public class Fruta : Game_Item {
    // 0 = ABACAXI;
    // 1 = BANANA;
    // 2 = CAJU;
    // 3 = MAÇÃ;
    // 4 = MANGA;
    [SerializeField]
    private int tipoFruta;
	protected override void  AdicionarItem()
    {

        Gerenciador gerenciador = FindObjectOfType<Gerenciador>();
        gerenciador.AdicionarFruta(tipoFruta);
        Game_Conquistas.instancia.ColetarFruta(tipoFruta);
        FindObjectOfType<FrutaHUD>().AnimarFruta(tipoFruta);
        base.AdicionarItem();
    }

}
