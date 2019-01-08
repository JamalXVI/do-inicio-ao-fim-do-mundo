using UnityEngine;
using System.Collections;
using System;

public class MovementController : MonoBehaviour
{
    public float maxSpeed = 2.0f;
    public float maxSpeedY = 3.0f;
	private float velocidade_maxima;
    [SerializeField]
    private float velocidadeMaximaPulo;
    bool direita = true;
    public bool Direita
    {
        set { direita = value;}
        get { return direita; }
    }
    public bool grounded;
    public bool dubleJump;
    public float jumpSpeed = 1500.0f;
    Animator anim;
    float groundRadius = 2f;
    public bool ja_pulou_ground;
    public LayerMask whatIsGround;
    public float moveDirection;
    public bool vivo;
    public bool invencivel;
    public float tempoInvencivel;
    public float tempoPiscar;
    private float contadorInvencivel;
    private float contadorPiscar;
    private bool piscado;
    private Gerenciador gerenciador;
    private bool pulando = false;
    private bool ativo = true;
    static int troca_in_front = Animator.StringToHash("Base.Troca_in_back");
    static int troca_out_back = Animator.StringToHash("Base.Troca_out_front");
    static int idle = Animator.StringToHash("Base.Idle");
    static int idle_in_fim = Animator.StringToHash("Base.Idle_In_Fim");
    static int idle_out_fim = Animator.StringToHash("Base.Idle_Out_Fim");
    static int walking = Animator.StringToHash("Base.Walking");
    static int grito = Animator.StringToHash("Base.Grito");
    static int run = Animator.StringToHash("Base.Run");
    static int cavucar = Animator.StringToHash("Base.Cavucar");
    static int levantar = Animator.StringToHash("Base.Levantar");
    static int levantar2 = Animator.StringToHash("Base.Levantar2");
    static int levantar3 = Animator.StringToHash("Base.Levantar3");
    private bool ultima_velocidade_maior_zero;
    private bool iniciou_habilidades;
	private float contador_habilidades = 0f;
    private float contar_pausar = 0f;
	public float delay_cancelar_habilidade=0.4f;
    private float tempo_parado = 0f;
    public AudioClip somPulo;
    public AudioClip somArrastarCaixa;
    public AudioClip somTrocarPersonagem;
    public bool tocandoSomArraste = false;
    private float forcaExterna;
    private Caixa caixaLevantada;
    public float ForcaExterna
    {
        get { return forcaExterna; }
        set { forcaExterna = value; }
    }
    private float empurrar;

    public float Empurrar
    {
        get { return empurrar; }
        set { empurrar = value; }
    }

    private bool catapulta;

    public bool Catapulta
    {
        get { return catapulta; }
        set { catapulta = value; }
    }

    
   
    
    
    /*
            |||||||||||||||||||||||||||||||||
            |||   VARIAVEIS DE CORRIDAS   |||
            |||||||||||||||||||||||||||||||||
    */
    public int correr_tempo_teclas_frames = 2;
    private bool correr_inicou = false;
    private bool correr_correndo = false;
    private float correr_contador;
    private bool terminar_correr;

    

    private float correr_tempo_maximo;
    /*
                        ||||||||||||||
                        |||   FIM  |||
                        ||||||||||||||
    */

    public float Tempo_Parado{
		get {return tempo_parado;}
		set {tempo_parado = value;}
	}
    
    public bool Ativo
    {
        get { return ativo; }
        set { ativo = value; }
    }
    public bool DeuPulo()
    {
        if (pulando)
        {
            return true;
        }
        if (anim.GetBool("Jumping"))
        {
            return true;
        }
        return !grounded;
        
    }
    void Start()
    {
		velocidade_maxima = maxSpeed;
        anim = GetComponent<Animator>();
        gerenciador = GameObject.FindObjectOfType<Gerenciador>();
    }

   
    public void DeixarInvencivel()
    {
        invencivel = true;
        contadorInvencivel = 0f;
        contadorPiscar = tempoPiscar * Time.deltaTime;
        piscado = false;
        grounded = false;
    }
    public void acabouInvencivel()
    {
        invencivel = false;
        animarInvencivel(false);
    }
	void EnquantoUsaHabilidade()
	{
        //Input.GetAxis ("Fire1") > 0
        if (!Game_Player.instancia.Habilidade_Atual.nome.Equals("Mover"))
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(0f, GetComponent<Rigidbody2D>().velocity.y);
            anim.SetFloat("Speed", 0f);
        }
        if (Input.GetKeyUp(Game_Player.instancia.Teclas["habilidade"])) {
			if(contador_habilidades > delay_cancelar_habilidade)
			{
				contador_habilidades = 0f;
                if (!Game_Player.instancia.Habilidade_Atual.nome.Equals("Descer"))
                {
				    Game_Player.instancia.PararHabilidades();
                    
                }
			}
		}
        Game_Player.instancia.GerarHabilidade();
        
	}
	
    /// <summary>
    ///||||||||||||||||||||||||||
    ///|||| Início do Update ||||
    ///||||||||||||||||||||||||||
    /// </summary>
    /// 
    private bool EstaColisao(GameObject obj, LayerMask mask)
    {
        return ((mask.value & (1 << obj.layer)) > 0);
    }
    /*
         |||||||||||||||||||||||||
         |||      COLISÃO      |||
         |||||||||||||||||||||||||
    */
    
    public void ColisaoPersonagem(bool principal)
    {
        GameObject colider_personagem = null;
        foreach (Transform tfilho in this.transform)
        {
            if (tfilho.tag.Equals("ColiderP"))
            {
                colider_personagem = tfilho.gameObject;
            }
        }
        GameObject[] coliders = GameObject.FindGameObjectsWithTag("ColiderP");
        foreach (GameObject i in coliders)
        {
            if (i != colider_personagem)
            {
                IgnorarColisao(i, colider_personagem, true);
            }
        }

    }
    private void IgnorarColisao(GameObject objeto, GameObject corpo_pesonagem, bool valor)
    {
        foreach (Collider2D i in objeto.GetComponents<Collider2D>())
        {
            foreach (Collider2D j in corpo_pesonagem.GetComponents<Collider2D>())
            {

                Physics2D.IgnoreCollision(i, j, valor);
            }   
        }
    }
    
    void VerificarGround(Collision2D col)
    {
        if (EstaColisao(col.gameObject, whatIsGround))
        {
            foreach (ContactPoint2D c in col.contacts)
            {

                if (c.otherCollider is CircleCollider2D &&
                    c.otherCollider.transform.position.y > c.collider.transform.position.y + c.collider.bounds.size.y / 2)
                {
                    grounded = true;
                    if (catapulta)
                    {
                        Empurrar = 0;
                        catapulta = false;
                    }
                }

            }
        }
    }
    void OnCollisionEnter2D(Collision2D col)
    {

        VerificarGround(col);
        VerificarArvore(col);
    }
    private void VerificarArvore(Collision2D col)
    {
        
        if (col.gameObject.GetComponent<Arvore_Cair>() != null)
        {
            foreach (ContactPoint2D c in col.contacts)
            {

                if (c.otherCollider is BoxCollider2D && 
                    c.collider.transform.position.y > c.otherCollider.transform.position.y + c.otherCollider.bounds.size.y/5
                    && VerificarPosicaoXArvore(c, col))
                {
                    if (GetComponent<MovementController>() != Game_Player.instancia.Movimento_Atual)
                    {
                        GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
                        Physics2D.IgnoreCollision(this.GetComponent<Collider2D>(), col.collider);
                    }
                    else
                    {
                        if (vivo && !invencivel)
                        {
                            gerenciador.MatarPersonagem();
                            Game_Player.instancia.MatarJogador();
                            Physics2D.IgnoreCollision(this.GetComponent<Collider2D>(), col.collider);
                        }
                    }
                }
            }
        }
    }

    private bool VerificarPosicaoXArvore(ContactPoint2D c, Collision2D col)
    {
        bool a_direita = col.gameObject.GetComponent<Arvore_Cair>().direita;
        if (!a_direita)
        {
            if (c.collider.transform.position.x + c.collider.bounds.size.x / 3 > c.otherCollider.transform.position.x)
            {
                return true;
            }
        }
        else
        {
            if (c.collider.transform.position.x - c.collider.bounds.size.x / 3 < c.otherCollider.transform.position.x)
            {

                return true;
            }
        }
        return false;
    }
    void OnCollisionStay2D(Collision2D col)
    {

        VerificarGround(col);
        /*
        if (estaColisao(col.gameObject, whatIsGround))
        {
            foreach (ContactPoint2D c in col.contacts)
            {
                if (c.otherCollider is CircleCollider2D &&
                    c.otherCollider.transform.position.y > c.collider.transform.position.y + c.collider.bounds.size.y/2)
                {
                    grounded = true;
                }
            }
        }
         * */
    }
    void OnCollisionExit2D(Collision2D col)
    {
        /*
        if (estaColisao(col.gameObject, whatIsGround))
        {
            foreach (ContactPoint2D c in col.contacts)
            {
                if (c.otherCollider is CircleCollider2D &&
                    c.otherCollider.transform.position.y > c.collider.transform.position.y + c.collider.bounds.size.y/2)
                {
                    grounded = false;
                }
            }
        }
         * */
    }
    // Update is called once per frame
    void Update()
    {
        if (invencivel)
        {
            contadorInvencivel += Time.deltaTime;
            contadorPiscar += Time.deltaTime;
            if (contadorPiscar >= tempoPiscar)
            {
                piscado = !piscado;
                contadorPiscar = 0f;
                animarInvencivel(piscado);
            }
            if (contadorInvencivel >= tempoInvencivel)
            {
                acabouInvencivel();
            }
        }
        if (!vivo)
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(0, GetComponent<Rigidbody2D>().velocity.y);
        }
    }
    void FixedUpdate()  
    {
        if (VerifcarHabilidadesEspeciais())
        {
            HabilidadesEspeciais();
            return;
        }
        if (ConsiderarPausa())
        {
            return;
        }

        VerificarPausarJogo();
        ContarTempoJogadorParado();
        Corrida();
        float movimento = DefinirMovimento();

        if (grounded)
        {

            VerificarPuloChao();
            VerificarTrocaPersonagensChao(movimento);
            VerificarHabilidadePersonagemChao(movimento);
        }
        else
        {
            PuloAr();
        }
        VerificarDirecao(movimento);

    }

    private void HabilidadesEspeciais()
    {
        if (Game_Player.instancia.Movimento_Atual != this) {
            return;
          };
        Game_Player.instancia.GerarHabilidade();
        //Input.GetAxis("Fire2"
        if (Input.GetKey(Game_Player.instancia.Teclas["habilidade"]))
        {
            FinalizarHabilidadeLevantar();
        }
        if (Input.GetKey(Game_Player.instancia.Teclas["troca"]))
        {
            if (Game_Player.instancia.Parar_Seguir && Game_Player.instancia.PodeTrocar() &&
               !Game_Player.instancia.apertou_botao_troca)
            {
                SoundManager.instance.PlayPersonagens(somTrocarPersonagem);
                Game_Player.instancia.finalizando_troca();
                return;
            }
        }
    }

    private void FinalizarHabilidadeLevantar()
    {
        caixaLevantada.FinalizarHabilidadeLevantar();
    }

    private bool VerifcarHabilidadesEspeciais()
    {
        if (VerificarLevantado())
        {
            return true;
        }
        return false;
    }
    private float DefinirMovimento()
    {
        if (VerificarUsandoHabilidade())
        {
            anim.SetFloat("Speed", 0);
            GetComponent<Rigidbody2D>().velocity = new Vector2(0f, GetComponent<Rigidbody2D>().velocity.y);
            return 0;
        }
        if (catapulta)
        {
            return 0;
        }
        float move = 0;
        if (Input.GetKey(Game_Player.instancia.Teclas["direita"]))
        {
            move = 1;
        }
        if (Input.GetKey(Game_Player.instancia.Teclas["esquerda"]))
        {
            move = -1;
        }
        //float move = Input.GetAxis("Horizontal");
        anim.SetFloat("Speed", Mathf.Abs(move));
        /*
        bool chao01 = (Physics2D.OverlapCircleAll(groundCheck01.position, groundRadius, whatIsGround).Length > 0);
        bool chao02 = Physics2D.OverlapCircleAll(groundCheck02.position, groundRadius, whatIsGround).Length > 0;
        bool chao03 = Physics2D.OverlapCircleAll(groundCheck02.position, groundRadius, whatIsGround).Length > 0;
        grounded = (chao01 || chao02 || chao03 ? true : false);
         */
        
        if (grounded && ja_pulou_ground)
        {
            grounded = false;
            ja_pulou_ground = false;
        }
        return move;
    }
    private void VerificarPuloChao()
    {
        if (anim.GetBool("OnAir") && !pulando)
        {
            anim.SetBool("OnAir", false);
            anim.CrossFade("Idle", 0f);

        }
        if (!anim.GetBool("Jumping"))
        {
            pulando = false;
        }

        //if (Input.GetAxisRaw("Jump") != 0)
        if (Input.GetKey(Game_Player.instancia.Teclas["pulo"]))
        {
            ForcaPular();
            AcaoPular();
        }
    }
    private void ForcaPular()
    {

        GetComponent<Rigidbody2D>().AddForce(new Vector2(0, jumpSpeed));
    }
    public void AcaoPular()
    {
        //anim.CrossFade("Jumping", 0f);
        anim.SetTrigger("Jumping");//Play("Jump");
        SoundManager.instance.PlayPersonagens(somPulo);
        ja_pulou_ground = true;
        pulando = true;
    }
    private void VerificarDirecao(float move)
    {
        if (!vivo)
        {
            return;
        }
       // float forcasDemais = Math.Max(0, GetComponent<Rigidbody2D>().velocity.x - 1 * velocidade_maxima - empurrar);
        GetComponent<Rigidbody2D>().velocity = new Vector2(empurrar +
        move * velocidade_maxima - forcaExterna, GetComponent<Rigidbody2D>().velocity.y);
        if (move > 0 && !direita)
        {
            FlipFacing();
        }
        else if (move < 0 && direita)
        {
            FlipFacing();

        }
    }
    private void PuloAr()
    {
        if (GetComponent<Rigidbody2D>().velocity.y > velocidadeMaximaPulo)
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(GetComponent<Rigidbody2D>().velocity.x, velocidadeMaximaPulo);
        }
        if (!anim.GetBool("Jumping"))
        {
            anim.SetBool("OnAir", true);
            pulando = false;
        }
    }
    private void VerificarTrocaPersonagensChao(float move)
    {
        //Input.GetAxis("Fire2"
        if (Input.GetKey(Game_Player.instancia.Teclas["troca"])
        && anim.GetCurrentAnimatorStateInfo(0).nameHash == idle && move == 0)
        {

            if (Game_Player.instancia.Parar_Seguir && Game_Player.instancia.PodeTrocar() &&
               !Game_Player.instancia.apertou_botao_troca)
            {
                SoundManager.instance.PlayPersonagens(somTrocarPersonagem);
                Game_Player.instancia.finalizando_troca();
                return;
            }
            else if (Game_Player.instancia.pode_Parar_Seguir() && Game_Player.instancia.Parar_Seguir)
            {
                Game_Player.instancia.Parar_Seguir = false;
            }
            else if (Game_Player.instancia.PodeTrocar() && !Game_Player.instancia.Parar_Seguir)
            {
                Game_Player.instancia.trocar_personagens();
                return;
            }
        }
        //0 >= Input.GetAxis("Fire2")
        else if (!Input.GetKey(Game_Player.instancia.Teclas["troca"]) && Game_Player.instancia.apertou_botao_troca)
        {
            Game_Player.instancia.apertou_botao_troca = false;
        }
    }
    private void VerificarHabilidadePersonagemChao(float move)
    {
        //Input.GetAxis("Fire1") > 0
        if (Input.GetKey(Game_Player.instancia.Teclas["habilidade"])) //&& move == 0)//anim.GetCurrentAnimatorStateInfo(0).nameHash == idle
        {
            PararAndar();
            IniciarHabilidades();
        }
    }

    private void PararAndar()
    {
        FinalizarCorrer();
        GetComponent<Rigidbody2D>().velocity = new Vector2(0, GetComponent<Rigidbody2D>().velocity.y);
        anim.SetFloat("Speed", 0f); 
    }
    /*
        |||||||||||||||||||||||||||||||||||||||||||||||||||||||||||
        ||||         C O R R I D A  P E R S O N A G E M        ||||
        |||||||||||||||||||||||||||||||||||||||||||||||||||||||||||
    */

    private void FinalizarCorrer()
    {
        anim.SetBool("Correndo", false);
        velocidade_maxima = maxSpeed;
        Game_Player.instancia.Correr = false;
        correr_correndo = false;
        terminar_correr = false;
    }
    private void IniciarCorrer()
    {
        anim.SetBool("Correndo", true);
        Game_Player.instancia.Correr = true;
        velocidade_maxima = (float)1.5 * maxSpeed;
        correr_inicou = false;
        correr_correndo = true;
        terminar_correr = true;
    }
    private void Corrida()
    {
        correr_tempo_maximo = correr_tempo_teclas_frames;

        if (Game_Player.instancia.Correr)
        {
            IniciarCorrer();
        }
        //Input.GetButtonDown("Horizontal")
        //if ((Input.GetKeyDown(Game_Player.game_player.Teclas["direita"]) ||
        //Input.GetKeyDown(Game_Player.game_player.Teclas["esquerda"])) && !correr_correndo)
        //{
        //    if (correr_inicou)
        //    {
        //        /*
        //        correr_contador += Time.deltaTime;
        //        if ( correr_contador >= correr_tempo_maximo)
        //        {
        //            correr_contador = 0f;
        //            iniciar_correr();
        //        }
        //         */
        //        iniciar_correr();
        //    }
        //    else {
        //        correr_contador = 0f;
        //        correr_inicou = true;
        //    }
        //}
        //else if (correr_inicou)
        //{
        //    correr_contador += Time.deltaTime;
        //    if (correr_contador > correr_tempo_maximo)
        //    {
        //        correr_contador = 0f;
        //        correr_inicou = false;
        //    }
        //}
        if (Input.GetKeyDown(Game_Player.instancia.Teclas["correr"]) || Input.GetKey(Game_Player.instancia.Teclas["correr"]))
        {
            IniciarCorrer();
        }
        else if(Input.GetKeyUp(Game_Player.instancia.Teclas["correr"]))
        {
            FinalizarCorrer();
        }
        //Mathf.Abs(Input.GetAxis("Horizontal")) < 0.5
        //Mathf.Abs(Input.GetAxis("Horizontal"))  <= 0
        if (!(Input.GetKey(Game_Player.instancia.Teclas["direita"]) || Input.GetKey(Game_Player.instancia.Teclas["esquerda"]))
         && anim.GetBool("Correndo") &&
        correr_correndo == true)
        {
            anim.SetBool("Correndo", false);
            FinalizarCorrer();
        }
    }
    /*
            |||||||||||||||||||||||||||||||||||||||||||||||||||
            |||||                 F I M                    ||||
            |||||||||||||||||||||||||||||||||||||||||||||||||||
    */
    private void ContarTempoJogadorParado()
    {
        //0 >= Input.GetAxis("Fire2") && 0 >= Input.GetAxis("Fire1")
        if (anim.GetCurrentAnimatorStateInfo(0).nameHash == idle &&
            !Input.GetKey(Game_Player.instancia.Teclas["habilidade"]) &&
            !Input.GetKey(Game_Player.instancia.Teclas["troca"])
            )
        {
            tempo_parado += Time.deltaTime;
        }
        else {
            tempo_parado = 0f;
        }
    }
    private void VerificarPausarJogo()
    {
        if (Input.GetKey(Game_Player.instancia.Teclas["esc"]))
        {
            if ((Game_Player.instancia.Jogador == this.transform))
            {
                if (contar_pausar <= 0)
                {
                    contar_pausar = Time.deltaTime;
                    gerenciador.Pausar_Jogo = !gerenciador.Pausar_Jogo;
                }
                else
                {
                    contar_pausar--;
                }

            }

        }
    }
    private bool ConsiderarPausa()
    {
        contador_habilidades += Time.deltaTime;
        if (VerificarLevantado() && Game_Player.instancia.Movimento_Atual == this)
        {
            return true;
        }
        if (VerificarUsandoHabilidade())
        {
            EnquantoUsaHabilidade();
            return true;
        }
        if (Game_Player.instancia.Pausar_jogadores)
        {
            return true;
        }
        if (EmTroca())
        {
            return true;
        }
        if (VerificarPararJogador())
        {
            return true;
        }
        return false;
    }
    private bool VerificarUsandoHabilidade()
    {
        if (Game_Player.instancia.Movimento_Atual != this)
        {
            return false;
        }
        return iniciou_habilidades;
    }

    private bool VerificarLevantado()
    {
        
        return anim.GetCurrentAnimatorStateInfo(0).nameHash == levantar3;
    }
    public bool VerificarPararJogador()
    {
        if (gerenciador.parar_jogador || Game_Player.instancia.Parar_Jogador ||
            (Game_Player.instancia.esperar_segunda_distancia && (Game_Player.instancia.Jogador != this.transform))
           || (Game_Player.instancia.Parar_Seguir && (Game_Player.instancia.Jogador != this.transform)))
        {
            if (anim.GetFloat("Speed") > 0)
            {
                anim.SetFloat("Speed", 0f);

            }
            GetComponent<Rigidbody2D>().velocity = new Vector2(- forcaExterna,
                GetComponent<Rigidbody2D>().velocity.y);
            return true;
        }
        return false;
    }
    public bool EmTroca()
    {
        if (anim.GetBool("TrocaIn") || anim.GetBool("TrocaOut"))
        {
            if (anim.GetCurrentAnimatorStateInfo(0).nameHash == troca_in_front ||
                anim.GetCurrentAnimatorStateInfo(0).nameHash == troca_out_back)
            {
                Game_Player.instancia.finalizar_troca();
            }
            return true;

        }
        return false;
    }
    /// <summary>
    ///||||||||||||||||||||||||||
    ///|||| Fim do Update |||||||
    ///||||||||||||||||||||||||||
    /// </summary>
    private void IniciarHabilidades()
    {
        if (!Game_Player.instancia.Habilidade_Ativa)
        {
            return;
        }
        if (Game_Player.instancia.pode_atirar(this.gameObject) && contador_habilidades > delay_cancelar_habilidade)
        {
            iniciou_habilidades = true;
            contador_habilidades = 0;
            anim.SetBool("Ataque01", true);
            Game_Player.instancia.Pausar_jogadores = true;
            Game_Player.instancia.IniciarHabilidades();
        }
    }
    
    public void DarSlide()
    {
        float move = 1f;
        if (!direita)
        {
            move *= -1;
        }
        GetComponent<Rigidbody2D>().velocity = new Vector2(empurrar + move * maxSpeed,
            Mathf.Min(GetComponent<Rigidbody2D>().velocity.y, maxSpeedY));

    }
    public void IniciarSlide()
    {

        if (!anim.GetBool("Slide"))
        {
            anim.SetTrigger("Slide");
        }
    }
    void FlipFacing()
    {
        direita = !direita;
        transform.Rotate(Vector3.up, 180.0f, Space.World);
    }
    public Transform PegarChao()
    {
        return PegarChao(transform);
    }
    public Transform PegarChao(Transform transformar)
    {
        foreach (Transform child in transformar)
        {
            Transform filho_trans = PegarChao(child);
            if (filho_trans != null)
            {
                return filho_trans;
            }
            if (child.gameObject.tag == "Chao")
            {
                return child.gameObject.transform;
            }
        }
        return null;
    }
    public void SetAllCollidersStatus(bool active)
    {
        SetAllCollidersStatus(active, transform);
    }
    public void SetAllCollidersStatus(bool active, Transform transformar)
    {
        foreach (Transform child in transformar)
        {
            SetAllCollidersStatus(active, child);
            foreach (Collider2D c in child.GetComponents<Collider2D>())
            {
                if (child.gameObject.tag != "Chao")
                {
                    c.isTrigger = !active;
                }
            }
        }
    }
    public void animarInvencivel(bool ativo)
    {
        animarInvencivel(ativo, transform);
    }
    public void animarInvencivel(bool ativo, Transform transformar)
    {
        foreach (Transform filho in transformar)
        {
            animarInvencivel(ativo, filho);
            foreach (SpriteRenderer sprite in filho.GetComponents<SpriteRenderer>())
            {
                sprite.enabled = !ativo;

            }
        }

    }
    /*
    public void mudar_Colliders(bool principal)
    {
        mudar_Colliders(principal, transform);
    }
    public void mudar_Colliders(bool principal, Transform transformar)
    {
        foreach (Transform filho in transformar)
        {
            mudar_Colliders(ativo, filho);
            if (principal)
            {
                foreach (SpriteRenderer sprite in filho.GetComponents<SpriteRenderer>())
                    {
                        sprite.sortingLayerName = "Player";

                    }
                if (filho.name == "Colliders")
                {
                    filho.tag = "ColiderP";
                    filho.gameObject.SetActive(true);
                }else if (filho.name == "MiniPe")
                {
                    filho.gameObject.SetActive(false);
                }
            }
            else
            {
                foreach (SpriteRenderer sprite in filho.GetComponents<SpriteRenderer>())
                {
                    sprite.sortingLayerName = "Player2";

                }
                if (filho.name == "Colliders")
                {
                    filho.tag = "ColiderS";
                    filho.gameObject.SetActive(false);
                }
                else if (filho.name == "MiniPe")
                {
                    filho.gameObject.SetActive(true);
                }
            }
        }

    }
     */
    public void DeixarTrigger(bool ativo)
    {
        DeixarTrigger(ativo, transform);
    }
    public void DeixarTrigger(bool ativo, Transform transformar)
    {
        foreach (Transform filho in transformar)
        {
            DeixarTrigger(ativo, filho);
            if (filho.name == "Colliders")
            {
                foreach (BoxCollider2D box in filho.GetComponents<BoxCollider2D>())
                {
                    box.isTrigger = ativo;
                }
                foreach (CircleCollider2D box in filho.GetComponents<CircleCollider2D>())
                {
                    box.isTrigger = ativo;
                }
            }
        }

    }

    

    /*
        void OnCollisionEnter2D(Collision2D colisor)
        {
            if (!ativo && !colisor.collider.tag.Contains("Player"))
            {
                deixarTrigger(false);
            }
        }
        void OnTriggerExit2D(Collider2D colisor)
        {
            if (!ativo && !colisor.tag.Contains("Player"))
            {
                deixarTrigger(true);
            }
        }
        */

    internal void mudar_Colliders()
    {
        throw new System.NotImplementedException();
    }
    public void FinalizarHabilidades()
    {
        iniciou_habilidades = false;
        anim.SetBool("Ataque01", false);
    }

    public bool PodeTerminarTroca()
    {
        if ((anim.GetCurrentAnimatorStateInfo(0).nameHash == idle ||
                anim.GetCurrentAnimatorStateInfo(0).nameHash == walking))
        {
            return true;
        }
        return false;
    }
    public bool FimIdleAnimacao()
    {
        if ((anim.GetCurrentAnimatorStateInfo(0).nameHash == idle_in_fim ||
                anim.GetCurrentAnimatorStateInfo(0).nameHash == idle_out_fim))
        {
            return true;
        }
        return false;
    }
    public Transform ResetarPosicao()
    {
        foreach (Transform filho in transform)
        {
            DeixarTrigger(ativo, filho);
            if (filho.name.ToLower().Contains("body"))
            {
                return filho.transform;
            }
        }
        return transform;
    }
    public void ResetarPosicaoBody()
    {
        foreach (Transform filho in transform)
        {
            if (filho.name.ToLower().Contains("body"))
            {
                filho.localPosition = Vector2.zero;
            }
        }
    }


    public bool EstaMovimentando()
    {
        if (anim.GetFloat("Speed") > 0f)
        {
            if (grounded)
            {
                return true;
            }
        }
        return false;
    }
    public void mudar_face()
    {
        FlipFacing();
    }


public void IniciarEmpurrando()
    {
        gerenciador.corpo_habilidade.GetComponent<Caixa>().IniciarCaixaEmpurrar(this);
        if (anim.GetBool("Ataque01"))
    {
    anim.SetBool("Empurrando", true);
    anim.SetBool("EmpurrandoF", false);
    anim.SetBool("EmpurrandoT", false);
    anim.Play("Push_Idle");
    gerenciador.corpo_habilidade.GetComponent<Caixa>().InanimarPedra(true);
    }
}
private void Desempurrar(Transform objeto)
{
    anim.SetBool("EmpurrandoF", false);
    anim.SetBool("EmpurrandoT", false);
    if (tocandoSomArraste)
    {
        tocandoSomArraste = false;
        SoundManager.instance.StopLoop();
    }
    objeto.GetComponent<Rigidbody2D>().velocity = new Vector2(0,
       objeto.GetComponent<Rigidbody2D>().velocity.y);
    GetComponent<Rigidbody2D>().velocity = new Vector2(0, GetComponent<Rigidbody2D>().velocity.y);
    
    /*
    if (Game_Player.game_player.Jogador_S)
    {
            MovementController movimentos_sec = Game_Player.game_player.Jogador_S.GetComponent<MovementController>();
            movimentos_sec.anim.SetBool("EmpurrandoF", false);
            movimentos_sec.anim.SetBool("EmpurrandoT", false);
    }
     * */

}
public void MoverEmpurrando()
    {
        
            Empurrarando();
    }

    private void Empurrarando()
    {
        //Input.GetAxis("Horizontal")
        float move = 0;
        if (Input.GetKey(Game_Player.instancia.Teclas["direita"]))
        {
            move = 1;
        }
        if (Input.GetKey(Game_Player.instancia.Teclas["esquerda"]))
        {
            move = -1;
        }
        Transform objeto = gerenciador.corpo_habilidade;
        objeto.GetComponent<Rigidbody2D>().velocity = new Vector2(GetComponent<Rigidbody2D>().velocity.x,
            objeto.GetComponent<Rigidbody2D>().velocity.y);
        /*
        if (Game_Player.game_player.Jogador_S)
        {
        Game_Player.game_player.Jogador_S.GetComponent<Rigidbody2D>().velocity = new Vector2(
            GetComponent<Rigidbody2D>().velocity.x, Game_Player.game_player.Jogador_S.GetComponent<Rigidbody2D>().velocity.y);

        }
         * */
        Caixa caixa = gerenciador.corpo_habilidade.GetComponent<Caixa>();
        var velCaixa = gerenciador.corpo_habilidade.GetComponent<Rigidbody2D>().velocity.x;
        var velPersonagem = GetComponent<Rigidbody2D>().velocity.x;
        if (Mathf.Abs(move) < 0.1f)
        {
            Desempurrar(objeto);
            //Game_Player.game_player.PararHabilidades();
            return;
        }
        if(Mathf.Abs(velCaixa) < 0.1f && Mathf.Abs(velPersonagem) > 0.1f
        || caixa.VerificarDistanciaEntreCaixaEPersonagem(this))
        {
            Desempurrar(objeto);
            return;
        }
        else if (gerenciador.corpo_habilidade.GetComponent<Caixa>().CorpoParado())
        {
            gerenciador.corpo_habilidade.GetComponent<Caixa>().InanimarPedra(true);
        }
        if (move < 0)
        {
            if (objeto.position.x < this.gameObject.transform.position.x)
            {
                InverterEmpurrada("T", "F");

            }
            else
            {
                InverterEmpurrada("F", "T");
            }
        }
        else
        {
            if (objeto.position.x < this.gameObject.transform.position.x)
            {
                InverterEmpurrada("F", "T");

            }
            else
            {
                InverterEmpurrada("T", "F");
            }
        }
        if (!tocandoSomArraste)
        {
            SoundManager.instance.PlayLoop(somArrastarCaixa);
            tocandoSomArraste = true;
        }

        Vector2 velocidade = new Vector2(move * (velocidade_maxima / 2), GetComponent<Rigidbody2D>().velocity.y);
        GetComponent<Rigidbody2D>().velocity = velocidade;
        objeto.GetComponent<Rigidbody2D>().velocity = new Vector2(GetComponent<Rigidbody2D>().velocity.x,
            objeto.GetComponent<Rigidbody2D>().velocity.y);
        /*
        Game_Player.game_player.Jogador_S.GetComponent<Rigidbody2D>().velocity = 
        new Vector2(GetComponent<Rigidbody2D>().velocity.x,
        Game_Player.game_player.Jogador_S.GetComponent<Rigidbody2D>().velocity.y);
         */
    }


    private void InverterEmpurrada(string suffixo, string novo_suffixo)
{
    Transform objeto = gerenciador.corpo_habilidade;
    if (anim.GetBool("Empurrando"+suffixo))
    {
        Desempurrar(objeto);
    }
    anim.SetBool("Empurrando" + novo_suffixo, true);
    
    /*
    MovementController movimentos_sec = Game_Player.game_player.Jogador_S.GetComponent<MovementController>();
    movimentos_sec.anim.SetBool("Empurrando" + novo_suffixo, true);
     */
}

public void FinalizarMover()
{
    anim.SetBool("Empurrando", false);
    anim.SetBool("EmpurrandoF", false);
    anim.SetBool("EmpurrandoT", false);
    if (tocandoSomArraste)
    {
        tocandoSomArraste = false;
        SoundManager.instance.StopLoop();
    }
    /*
    MovementController movimentos_sec = Game_Player.game_player.Jogador_S.GetComponent<MovementController>();
    movimentos_sec.anim.SetBool("EmpurrandoF", false);
    movimentos_sec.anim.SetBool("EmpurrandoT", false);
     */
    gerenciador.corpo_habilidade.GetComponent<Rigidbody2D>().velocity = new Vector2(0f,
        gerenciador.corpo_habilidade.GetComponent<Rigidbody2D>().velocity.y);
    if (!gerenciador.corpo_habilidade.GetComponent<Caixa>().caindo)
    {
        gerenciador.corpo_habilidade.GetComponent<Caixa>().InanimarPedra(false);
    }
    

}

public bool PoderPararMover()
{
        //float move = Input.GetAxis("Horizontal");
        //return Mathf.Abs(move) < 0.1f;
        return !(Input.GetKey(Game_Player.instancia.Teclas["direita"]) || Input.GetKey(Game_Player.instancia.Teclas["esquerda"]));
}

public void ignorar_colisao_personagens(GameObject objeto, bool valor)
{
    GameObject[] coliders = GameObject.FindGameObjectsWithTag("ColiderP");
    foreach (GameObject i in coliders)
    {
        IgnorarColisao(objeto, i, valor);
    }
}
public void HabilidadeDescer(bool iniciou)
{

   
   if (iniciou)
   {
       Game_Player.instancia.Jogador.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 30);
       gerenciador.corpo_habilidade.GetComponent<Plataforma_Descer>().SumirColider(this.transform.Find("Colliders").gameObject);
      // Game_Player.game_player.Jogador_S.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 30);
   }
   
}
public bool AlertaDeParadoY()
{
    return Mathf.Abs(GetComponent<Rigidbody2D>().velocity.y) < 0.1;
}
public bool AlertaDeParadoY_Alto()
{
    return Mathf.Abs(GetComponent<Rigidbody2D>().velocity.y) < 2;

}

public void iniciarCavucar()
{
        anim.SetTrigger("Cavucar");
        TerraFofaDireita terraFofa = gerenciador.corpo_habilidade.GetComponent<TerraFofaDireita>();
        terraFofa.FazerCavucar();
        //Game_Player.game_player.pararHabilidades();
}
public void IniciarFlorescer()
{
        anim.SetTrigger("Tocar");
        TerraFofaDireita terraFofa = gerenciador.corpo_habilidade.GetComponent<TerraFofaDireita>();
        terraFofa.FazerFlorescer();
}
public void IniciarSong()
{
        anim.SetTrigger("Tocar");
        Game_Inimigo inimigo = gerenciador.corpo_habilidade.GetComponent<TocarViolao>().RetornarInimigo();
        inimigo.TocandoViolao();
        
        inimigo.GetComponent<Animator>().SetTrigger("Som");
}

public void IniciarGrito()
{
    anim.SetTrigger("Grito");
}
public bool CondicaoPararGrito()
{
    return anim.GetCurrentAnimatorStateInfo(0).nameHash != grito;
}
public void ForcarPararGrito()
{
    
}

public void PararGrito()
{
    gerenciador.corpo_habilidade.GetComponent<Grito>().FinalizarGrito();
}
public bool VerificarFimCavucar()
{
        return anim.GetCurrentAnimatorStateInfo(0).nameHash != cavucar;
 }

internal void IniciarLevantar()
{
    anim.SetTrigger("Levantar");
    caixaLevantada =  gerenciador.corpo_habilidade.GetComponent<Caixa>();
        caixaLevantada.PrimitivoSegurandoCaixa(this);
}

public bool CondicaoPararLevantar()
{
    return anim.GetCurrentAnimatorStateInfo(0).nameHash == levantar3;
}

public bool CondicaoLevantando()
{
    return anim.GetCurrentAnimatorStateInfo(0).nameHash == levantar2;
}
    public void VerSeTaLevantandoPedra()
    {
        if (CondicaoPararLevantar())
        {
            caixaLevantada.FinalizarHabilidadeLevantar();
        }
    }
    internal void MoverCaixa()
    {
        GameObject mao = GameObject.Find("Primitivo_HandL_Idle");
        BoxCollider2D cxa = gerenciador.corpo_habilidade.GetComponent<BoxCollider2D>();
        float calcDistx = (Direita ? 2.5f : -1 * (cxa.size.x/2 + 2.5f));
        //gerenciador.corpo_habilidade.GetComponent<Rigidbody2D>().isKinematic = true;
        gerenciador.corpo_habilidade.position = new Vector3(mao.transform.position.x + calcDistx,
        mao.transform.position.y + 4.98f,
        gerenciador.corpo_habilidade.position.z);
}
}
