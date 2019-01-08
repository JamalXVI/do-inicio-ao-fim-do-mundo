using UnityEngine;
using System.Collections;

public class InimigoAguia : Game_Inimigo
{
    static int fimAtaque = Animator.StringToHash("Aguia.Atack toR - fim");
    static int fimAtaqueL = Animator.StringToHash("Aguia.Atack toL - fim");
    static int idle = Animator.StringToHash("Aguia.Idle toL");
    static int idleR = Animator.StringToHash("Aguia.Idle toR");
    [SerializeField]
    private GameObject tonta;
    [SerializeField]
    private GameObject normal;
    private Animator anim;
    [SerializeField]
    private float tempoEsperaAtaque;
    private float contadorTempoAtaque;
    private Gerenciador gerenciador;
    private int idItem = ConstantesDoSistema.IdAguia;
    private bool desceuAtaque = false;
    [SerializeField]
    private GameObject paiDaAguia;
	// Use this for initialization
	void Start () {
        anim = GetComponentInParent<Animator>();
        gerenciador = FindObjectOfType<Gerenciador>();
	}
	
	// Update is called once per frame
    void FixedUpdate()
    {
        if (parar_tudo)
        {
            if (!desceuAtaque)
	        {
                if (anim.GetCurrentAnimatorStateInfo(0).nameHash == fimAtaque ||
                    anim.GetCurrentAnimatorStateInfo(0).nameHash == fimAtaqueL)
                {
                    desceuAtaque = true;
                }
            }
            else
            {
                if (anim.GetCurrentAnimatorStateInfo(0).nameHash == idle ||
                    anim.GetCurrentAnimatorStateInfo(0).nameHash == idleR)
                {
                    desceuAtaque = false;
                    parar_tudo = false;
                    InverterEstados(false);
                }
            }
        }
        AnimarAtaqueAguia();
	}

    private void AnimarAtaqueAguia()
    {
        contadorTempoAtaque += Time.deltaTime;
        if (contadorTempoAtaque >= tempoEsperaAtaque)
        {
            contadorTempoAtaque = 0f;
            anim.SetTrigger("Attack");
        }
    }

    public override void TocandoViolao()
    {
        parar_tudo = true;
        desceuAtaque = false;
        InverterEstados(true);
    }
    private void InverterEstados(bool valor)
    {
        tonta.SetActive(valor);
        normal.SetActive(!valor);
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
                Game_Player.instancia.MatarJogador();
                Game_Conquistas.instancia.AdicionarMortePorAnimais("aguia");
              //      Physics2D.IgnoreCollision(this.GetComponent<Collider2D>(), colisor.collider);
            }

        }
    }

    private void VirarItem(Collision2D colisor)
    {
        if (base.IniciarColisaoJogador(colisor.gameObject))
        {
            SoundManager.instance.PlaySingle(somMorrer);
            AdicionarItem();
            Game_Conquistas.instancia.AdicionarAnimal("aguia");
            Destroy(paiDaAguia, 0f);

        }
    }
    void AdicionarItem()
    {
        if (Game_Player.instancia.quantidade_item[idItem] <= 0)
        {
            Game_Player.instancia.quantidade_item[idItem] += 1;
        }
        Game_Player.instancia.QuantidadeInimigosFase++;
    }
}
