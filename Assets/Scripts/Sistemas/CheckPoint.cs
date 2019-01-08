using UnityEngine;
using System.Collections;

public class CheckPoint : MonoBehaviour {

	// Use this for initialization
    public int numeracao;
    private Vector2 posicoes;
    public float velocidade_baixo;
    public float max_distbaixo;
    private bool reverter_movimento;
    private bool passou_jogador;
    public SpriteRenderer sprite;
    private Color cornormal;
    private Color coralterado;
    public float exergarJogadorDistancia;
    public bool inicial;
    public Transform posicao_despejo;
    [SerializeField]
    private AudioClip somPassar;
    private bool tocouSomCheck = false; 
    public bool PassouJogador
    {
        get { return passou_jogador; }
        set { passou_jogador = value; }
    }
	void Start () {
        posicoes = new Vector2(transform.position.x, transform.position.y);
        reverter_movimento = false;
        passou_jogador = false;
        cornormal = new Color32(248, 117, 66, 255);
        coralterado = new Color32(107, 255, 120, 255);
        if (inicial)
        {
            passou_jogador = true;

        }
	}
	public Vector2 retornar_posicoes()
    {
        if (posicao_despejo == null)
        { 
            return new Vector2(transform.position.x, transform.position.y);
        }
        return new Vector2(posicao_despejo.position.x, posicao_despejo.position.y);
    }
	// Update is called once per frame
    void Update()
    {
        verificar_se_passou_jogador();
        colorirSeta();
        if (!reverter_movimento)
        {
            

                if (passou_distancia(posicoes.y))
                {
                    reverter_movimento = !reverter_movimento;

                }
                else
                {
                    Vector3 movimento = new Vector3(0f, velocidade_baixo, 0f);
                    transform.position -= movimento;
                }
            }
            else
            {
                if (passou_distancia(max_distbaixo))
                {
                    Vector3 movimento = new Vector3(0f, velocidade_baixo, 0f);
                    transform.position += movimento;

                }
                else
                {
                    reverter_movimento = !reverter_movimento;
                }
            }
        }
    public void verificar_se_passou_jogador()
    {
        Vector2 VetorInicial = new Vector2(transform.position.x, transform.position.y);
        Vector2 VetorFinal = new Vector2(transform.position.x, (transform.position.y - exergarJogadorDistancia));
        Debug.DrawLine(VetorInicial, VetorFinal, Color.red);
        if (Physics2D.Linecast(transform.position, VetorFinal,
            1 << LayerMask.NameToLayer("Jogador")))
        {
            passou_jogador = true;
            if (!tocouSomCheck)
            {
                VerificarLevantandoPrimitivo();  
             Game_Player.instancia.resetar_jogador();
             SoundManager.instance.PlaySingle(somPassar);
             tocouSomCheck = true;
            }
        }
        
    }

    private static void VerificarLevantandoPrimitivo()
    {
        if (Game_Player.instancia.JogadorSecundario == null)
        {
            return;
        }
        if (Game_Player.instancia.JogadorSecundario.name.Contains("Primitivo"))
        {
            Game_Player.instancia.JogadorSecundario.GetComponent<MovementController>().VerSeTaLevantandoPedra();
        }
    }
    private void colorirSeta()
    {
        if (passou_jogador)
        {
            sprite.color = coralterado;
        }
        else
        {
            sprite.color = cornormal;
        }
    }
    public bool passou_distancia(float distancia)
    {
        float posicao = transform.position.y;
        if (distancia < 0)
        {
            if (distancia >= posicao)
                return true;
            else
                return false;
        }
        else
        {
            if (posicao > distancia)
                return true;
            else
                return false;
        }
    }

}