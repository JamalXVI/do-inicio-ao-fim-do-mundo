using UnityEngine;
using System.Collections;

public class InimigoCerebro : Game_Inimigo
{
    private Gerenciador gerenciador;
    public float gravidade_decair;
    public Vector2 forca;
    public Vector2 puloForca;
    public float movimentoTempoMax;
    private float contadorTempo = 0f;
    private float contar_destruir_tempo;
    public float contar_destruir;
    private bool direcao = false;
    public float numeroPulos;
    private float contadorPulos;
    // Use this for initialization
    void Start()
    {
        gerenciador = GameObject.FindObjectOfType<Gerenciador>();
    }
    void Awake()
    {
        base.Acordar();
    }
    void Update()
    {
        if (vivo)
        {


            contadorTempo += Time.deltaTime;
            if (contadorTempo >= movimentoTempoMax)
            {
                if (contadorPulos == 0)
                {
                    FlipFacing();
                    puloForca.x = -puloForca.x;
                }
                contadorTempo = 0f;
                contadorPulos++; ;
                contadorTempo = 0f;
                GetComponent<Rigidbody2D>().AddForce(puloForca);
                GetComponent<Animator>().SetTrigger("Pular");
                if (contadorPulos >= numeroPulos)
                {
                    contadorPulos = 0f;
                    direcao = !direcao;

                }
            }

        }
        else
        {
            contar_destruir_tempo += Time.deltaTime;
            if (contar_destruir_tempo >= contar_destruir)
            {
                Destroy(gameObject);
            }
        }
    }
    void FlipFacing()
    {

        transform.Rotate(Vector3.up, 180.0f, Space.World);
    }
    void OnCollisionEnter2D(Collision2D colisor)
    {
        if (colisor.gameObject.tag.StartsWith("Player") && vivo)
        {
            MovementController vivojog = colisor.gameObject.GetComponentInParent<MovementController>();
            if (vivojog.vivo && !vivojog.invencivel)
            {
                gerenciador.MatarPersonagem();
                Physics2D.IgnoreCollision(this.GetComponent<Collider2D>(), colisor.collider);
            }

        };
    }
    public void Destruir()
    {
        base.Destruir();
        vivo = false;
        GetComponent<Collider2D>().enabled = false;
        GetComponent<Rigidbody2D>().gravityScale = gravidade_decair;
        GetComponent<SpriteRenderer>().sortingLayerName = "Foreground";
        GetComponent<Rigidbody2D>().AddForce(forca);
        gerenciador.pontuacao_nivel += 10;
    }
    void OnTriggerEnter2D(Collider2D colisor)
    {

        if (colisor.gameObject.tag.StartsWith("Player") && vivo)
        {
            MovementController vivojog = colisor.gameObject.GetComponentInParent<MovementController>();
            if (vivojog.vivo)
            {
                
                Destruir();
            }


        }
    }

}
