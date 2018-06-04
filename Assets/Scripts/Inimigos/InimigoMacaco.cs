using UnityEngine;
using System.Collections;

public class InimigoMacaco : Game_Inimigo
{
    private Animator anim;
    private Transform groundCheck;
    public LayerMask oQueEhChao;
    public float distancia;
    public bool grounded;
    private bool pulando;
    private float raioGround =2f;
    public float forca_pulo;
    private Gerenciador gerenciador;
    private bool ja_pulou;
    private bool ja_pulou_ground;
    [SerializeField]
    private float velocidadeMaximaPulo;
    private int idItem = ConstantesDoSistema.IdMacaco;
    // Use this for initialization
    void Start () {
        anim = GetComponent<Animator>();
        pulando = false;
        gerenciador = FindObjectOfType<Gerenciador>();
        foreach (Transform filho in transform)
        {
            if (filho.tag.ToLower().Contains("chao"))
            {
                groundCheck = filho;
            }
        }
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        if (parar_tudo)
        {
            /*
            Duracao_Paralisado -= Time.deltaTime;
            if (0 >= Duracao_Paralisado)
            {
                anim.SetBool("Dancando", false);
                parar_tudo = false;
            }   
             * */
            return;
        }
        if (!grounded)
        {
            PuloAr();
        }
        else
        {
            InteligenciaPular();
        }   
        AnularPulo();

    }
    private void Pulo()
    {
        if (anim.GetBool("Pulando") && !pulando)
        {
            anim.SetBool("Pulando", false);
            anim.CrossFade("Idle", 0f);

        }
        if (!anim.GetBool("Pular")) {
            pulando = false;
        }
    }
    public void AcaoPular()
    {
        //anim.CrossFade("Jumping", 0f);
        anim.SetTrigger("Pular");//Play("Jump");
        ja_pulou_ground = true;
        pulando = true;
    }
    private void PuloAr()
    {
        if (GetComponent<Rigidbody2D>().velocity.y > velocidadeMaximaPulo)
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(GetComponent<Rigidbody2D>().velocity.x, velocidadeMaximaPulo);
        }
        if (!anim.GetBool("Pular"))
        {
            anim.SetBool("Pulando", true);
            pulando = false;
        }
    }
    private void AnularPulo()
    {
        if (grounded && ja_pulou_ground)
        {
            grounded = false;
            ja_pulou_ground = false;
        }
    }  
   
    private void InteligenciaPular()
    {
        Pulo();
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
        Debug.DrawLine(VetorInicial, VetorFinal, Color.yellow);
        VerificarLayerJogador(VetorFinal);
    }

    private void VerificarLayerJogador(Vector2 VetorFinal)
    {

       // if (Physics2D.Linecast(transform.position, VetorFinal,
       //            1 << LayerMask.NameToLayer("Jogador")))
       // {
            VerificarPuloPersonagem();
       // }
    }

    private void VerificarPuloPersonagem()
    {
        MovementController movimentos_personagem = Game_Player.game_player.Jogador.GetComponent<MovementController>();
        if (!pulando && movimentos_personagem.DeuPulo())
        {
            AcaoPular();
            ForcaPular();
        }
    }

    private void ForcaPular()
    {

        GetComponent<Rigidbody2D>().AddForce(new Vector2(0, forca_pulo));
    }
    void OnCollisionEnter2D(Collision2D colisor)
    {
        if (parar_tudo)
        {
            VirarItem(colisor);
            return;
        }
        if (base.IniciarColisaoJogador(colisor.gameObject))
        {
            MovementController vivojog = colisor.gameObject.GetComponentInParent<MovementController>();
            if (vivojog.vivo && !vivojog.invencivel)
            {
                gerenciador.MatarPersonagem();
                Game_Player.game_player.MatarJogador();
                Game_Conquistas.instancia.AdicionarMortePorAnimais("macaco");
                Physics2D.IgnoreCollision(this.GetComponent<Collider2D>(), colisor.collider);
            }

        }
        else {
            //Verificar Colisão com o Chão
            VerificarGround(colisor);
        };
    }

    private void VirarItem(Collision2D colisor)
    {
        if (base.IniciarColisaoJogador(colisor.gameObject))
        {
            SoundManager.instance.PlaySingle(somMorrer);
            AdicionarItem();
            Game_Conquistas.instancia.AdicionarAnimal("macaco");
            Destroy(this.gameObject, 0f);

        }
    }
    void AdicionarItem()
    {
        if (Game_Player.game_player.quantidade_item[idItem] <= 0)
        {
            Game_Player.game_player.quantidade_item[idItem] += 1;    
        }
        Game_Player.game_player.QuantidadeInimigosFase++;
    }
    void VerificarGround(Collision2D col)
    {
        if (estaColisao(col.gameObject, oQueEhChao))
        {
            foreach (ContactPoint2D c in col.contacts)
            {

                if (c.otherCollider is PolygonCollider2D &&
                    c.otherCollider.transform.position.y > c.collider.transform.position.y + c.collider.bounds.size.y / 2)
                {
                    grounded = true;
                }

            }
        }
    }
    private bool estaColisao(GameObject obj, LayerMask mask)
    {
        return ((mask.value & (1 << obj.layer)) > 0);
    }
    /*
    void Mudar_Coliders(bool ativar)
    {
        Transform miniPe = null;
        foreach (Transform filho in transform)
        {
            if (filho.name.ToLower().Contains("coliders"))
            {
                filho.gameObject.SetActive(ativar);
            }else if (filho.name.ToLower().Contains("minipe"))
            {
                miniPe = filho;
            }
        }
        if (miniPe != null)
        {
            miniPe.gameObject.SetActive(!ativar);
        }
    }
    */
    void OnTriggerEnter2D(Collider2D colisor)
    {
        if (base.VerificarMusica(colisor))
        {
            anim.SetTrigger("Som");
            //Mudar_Coliders(false);
            parar_tudo = true;
            Duracao_Paralisado = Game_Player.game_player.socolinha.duracao_paralisado;
            return;
        }
    }

    public override void TocandoViolao()
    {
        parar_tudo = true;
    }
}
