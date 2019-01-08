using UnityEngine;
using System.Collections;

public class InimigoTigre : Game_Inimigo
{

    private Gerenciador gerenciador;
    public float movimento;
    private bool movimento_invertido;
    public float VelocidadeMaxima;
    public Animator anim;
    public float tempo_inverter;
    private float contador_inverter;
    public float tempo_esperar;
    private float contador_esperar;
    private bool esperar;
    public float distancia;
    private bool atacar;
    private bool ja_atacou;
    private bool esperar_reatacar;
    public Vector2 forca_pulo;
    public float tempo_reataque_max;
    private float tempo_reataque;
    public Transform groundCheck;
    float groundRadius = 0.2f;
    public LayerMask whatIsGround;
    private bool grounded;
    public Vector2 impluso_no_pulo;

    public SpriteRenderer sprite;
    // Use this for initialization
    void Start()
    {
        anim = GetComponentInChildren<Animator>();
        gerenciador = GameObject.FindObjectOfType<Gerenciador>();
        FlipFacing();
    }
    void Awake()
    {
        base.Acordar();
    }
    void Update()
    {
        base.Atualizar();
    }
    void FixedUpdate()
    {
        if (parar_tudo)
        {
            

            Duracao_Paralisado -= Time.deltaTime;
            if (0f > Duracao_Paralisado)
            {
                parar_tudo = false;

            }
            else
            {
                return;
            }
            
        }
        if (vivo)
        {
            grounded = Physics2D.OverlapCircle(groundCheck.position, groundRadius, whatIsGround);

            if (atacar)
            {
                inteligenciadeataque();
            }
            else
            {
                inteligenciademovimento();
            }


        }
        else
        {
        }
    }
    private void verificar_reataque()
    {
        if (esperar_reatacar)
        {
            tempo_reataque += Time.deltaTime;
            if (tempo_reataque >= tempo_reataque_max)
            {
                tempo_reataque = 0;
                esperar_reatacar = false;

            }
        }
    }
    private void inteligenciademovimento()
    {

        verificar_reataque();
        verificar_ataque();
        float move = movimento;
        contador_inverter += Time.deltaTime;
        if (contador_inverter > tempo_inverter && !(anim.GetBool("Attack")))
        {
            if (esperar)
            {
                contador_esperar += Time.deltaTime;
                if (contador_esperar > tempo_esperar)
                {
                    contador_esperar = 0f;
                    esperar = false;
                }
                else
                {
                    anim.SetFloat("Speed", 0f);
                    return;
                }
            }
            contador_inverter = 0f;
            movimento_invertido = !movimento_invertido;
            esperar = true;
            FlipFacing();
        }
        if (movimento_invertido)
        {
            move *= -1;
        }
        anim.SetFloat("Speed", Mathf.Abs(move));
        GetComponent<Rigidbody2D>().velocity = new Vector2(move * VelocidadeMaxima, GetComponent<Rigidbody2D>().velocity.y);
    }
    private void inteligenciadeataque()
    {
        if (!ja_atacou)
        {
            float fx = forca_pulo.x;
            if (movimento_invertido)
            {
                fx *= -1;
            }
            GetComponent<Rigidbody2D>().AddForce(new Vector2(fx, forca_pulo.y), ForceMode2D.Impulse);
            anim.Play("Attacking");
            ja_atacou = true;
        }
        else if (!grounded)
        {
            if (!anim.GetBool("Attack") && !anim.GetBool("Attacking"))
            {
                anim.SetBool("Attacking", true);
            }
        }
        else
        {
            if (anim.GetBool("Attacking"))
            {
                anim.SetBool("Attacking", false);
                ja_atacou = false;
                atacar = false;
                esperar_reatacar = true;
            }
        }

    }
    private void verificar_ataque()
    {
        if (esperar_reatacar)
        {
            return;
        }
        float distanciatemp;
        if (transform.eulerAngles.y >= 180)
        {
            distanciatemp = distancia;
        }
        else
        {
            distanciatemp = -1 * distancia;
        }
        Vector2 VetorInicial = new Vector2(transform.position.x, transform.position.y);
        Vector2 VetorFinal = new Vector2(transform.position.x + distanciatemp, (transform.position.y));
        Debug.DrawLine(VetorInicial, VetorFinal, Color.cyan);
        if (Physics2D.Linecast(transform.position, VetorFinal,
            1 << LayerMask.NameToLayer("Jogador")))
        {
            atacar = true;
        }
        else
        {
            atacar = false;
        }
    }
    void FlipFacing()
    {

        transform.Rotate(Vector3.up, 180.0f, Space.World);
    }
    void OnCollisionEnter2D(Collision2D colisor)
    {
        if (parar_tudo)
        {
            return;
        }
        if (colisor.gameObject.tag.StartsWith("PlayerP") && vivo)
        {
            MovementController vivojog = colisor.gameObject.GetComponentInParent<MovementController>();
            if (vivojog.vivo && !vivojog.invencivel)
            {
                gerenciador.MatarPersonagem();
                Game_Player.instancia.MatarJogador();
                Physics2D.IgnoreCollision(this.GetComponent<Collider2D>(), colisor.collider);
            }

        };
    }
    public void Destruir()
    {
        base.Destruir();
        vivo = false;
        GetComponent<Collider2D>().enabled = false;
        GetComponent<Rigidbody2D>().isKinematic = true;
        desativar_colider();
        anim.Play("Morrendo");
        gerenciador.pontuacao_nivel += 10;
    }
    void OnTriggerEnter2D(Collider2D colisor)
    {
        
        if (base.VerificarMusica(colisor))
        {
            atacar = false;
            anim.SetBool("Attacking", false);
            anim.SetBool("Attack", false);
            anim.SetFloat("Speed", 0f);
            ja_atacou = false;
            esperar_reatacar = false;
            parar_tudo = true;
            Duracao_Paralisado = Game_Player.instancia.socolinha.duracao_paralisado;
            return;
        }
        if (colisor.gameObject.tag.StartsWith("ColiderP") && vivo && !anim.GetBool("Attack") && !anim.GetBool("Attacking"))
        {
            MovementController vivojog = colisor.gameObject.GetComponentInParent<MovementController>();
            if (vivojog.vivo)
            {
                parar_tudo = false;
                Destruir();
                Game_Player.instancia.Jogador.gameObject.GetComponent<Rigidbody2D>().AddForce(impluso_no_pulo);
            }
        }
    }
    void desativar_colider()
    {
        foreach (Transform filho in transform)
        {
            foreach (Collider2D item in filho.GetComponents<Collider2D>())
            {
                item.enabled = false;
            }
            
        }
    }
}
