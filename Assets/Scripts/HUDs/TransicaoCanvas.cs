using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TransicaoCanvas : MonoBehaviour {

    static int black = Animator.StringToHash("Transicao.Black");
    public Animator transicao_anim;
    
	// Use this for initialization
	void Start () {
        SoundManager.instance.FadeOutMusica();
        Button[] botoes = GetComponents<Button>();
        foreach (Button btn in botoes)
        {
            btn.onClick.RemoveAllListeners();
        }
	}
	
	// Update is called once per frame
    void FixedUpdate()
    {
        if (transicao_anim.GetCurrentAnimatorStateInfo(0).nameHash == black)
        {
            if (!Game_Temp.instancia.EhHistoria)
            {
                Game_Player.instancia.UltimoNivel = Application.loadedLevel;

            }
            else
            {
                Game_Temp.instancia.EhHistoria = false;
            }
            Application.LoadLevel(Game_Player.instancia.carregando);

        }
	
	}
}
