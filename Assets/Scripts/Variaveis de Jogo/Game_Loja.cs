using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Game_Loja : MonoBehaviour {
    public static Game_Loja instancia;
    public ItemLoja[] itemsDaLoja;
	void Awake () {
        VerificarSeJaExiste();
        InicializarItensLoja();
	}

    private void InicializarItensLoja()
    {
        itemsDaLoja = new ItemLoja[23];
        //ARTE
        criarItemDaLoja(0, "Telas Iniciais do Jogo",
        "Ideias descartadas da Tela de Abertura do Jogo, desenhado por Victor.", ConstantesDoSistema.Arte, true, 40);
        criarItemDaLoja(1, "Conceito da Arte do Estilo",
        "Conceitos Iniciais sobre o estilo visual do Jogo, desenhado por Victor.", ConstantesDoSistema.Arte, false, 60);
        criarItemDaLoja(2, "Sprites dos Personagens",
        "Sprites dos Personagens, desenhado por Victor.", ConstantesDoSistema.Arte, false, 80);
        criarItemDaLoja(3, "Sprites dos Animais",
        "Sprites dos Animais, desenhado por Victor.", ConstantesDoSistema.Arte, false, 80);
        criarItemDaLoja(4, "Skecthes de Animais",
        "Skecthes de Animais, desenhados por Yasmin.", ConstantesDoSistema.Arte, false, 60);
        criarItemDaLoja(5, "Trocas de Fases",
        "Ideias descartadas sobre as cenas de troca de fases desenhadas por Victor.", ConstantesDoSistema.Arte, true, 40);
        criarItemDaLoja(6, "Conceito da Arte de Fases ",
        "Conceitos Iniciais sobre o estilo das fases, desenhado por Victor", ConstantesDoSistema.Arte, false, 60);
        criarItemDaLoja(7, "Conceito da Arte da Mesopotâmia ",
        "Conceitos Iniciais sobre o estilo das fases do mundo da Mesopotâmia, desenhado por Victor", ConstantesDoSistema.Arte, false, 60);
            criarItemDaLoja(8, "Desenho Inicial de Como Seria o Jogo",
            "Conceitos Iniciais sobre como seria o jogo, desenhado por Victor", ConstantesDoSistema.Arte, false, 60);
        //MUSICAS
        criarItemDaLoja(9, "Música Título", "Música disponível em www.bensound.com", ConstantesDoSistema.Musica, true, 15);
        criarItemDaLoja(10, "Música Tema Fase 1-1", "Música disponível em www.bensound.com", ConstantesDoSistema.Musica, false, 20);
        criarItemDaLoja(11, "Música Tema Fase 1-2", "Música disponível em www.bensound.com", ConstantesDoSistema.Musica, false, 20);
        criarItemDaLoja(12, "Música Tema Fase 1-3", "Música disponível em www.bensound.com", ConstantesDoSistema.Musica, false, 20);
        criarItemDaLoja(13, "Música Tema Fase 2-1", "Música disponível em www.bensound.com", ConstantesDoSistema.Musica, false, 20);
        criarItemDaLoja(14, "Música Tema Fase 2-2", "Música disponível em www.bensound.com", ConstantesDoSistema.Musica, false, 20);
        criarItemDaLoja(15, "Música Tema Fase 2-3", "Música disponível em www.bensound.com", ConstantesDoSistema.Musica, false, 20);
        criarItemDaLoja(16, "Música Tema Seleção de Fase", "Música disponível em www.bensound.com", ConstantesDoSistema.Musica, true, 20);
        criarItemDaLoja(17, "Música Tema Cena de Opções", "Música disponível em www.bensound.com", ConstantesDoSistema.Musica, true, 20);
        //HISTORIAS
        criarItemDaLoja(18, "Prólogo", "História Inicial do Jogo", ConstantesDoSistema.Historia, false, 0);
        criarItemDaLoja(19, "História da Agricultura", "", ConstantesDoSistema.Historia, false, 0);
        criarItemDaLoja(20, "História do Fogo", "", ConstantesDoSistema.Historia, false, 0);
        criarItemDaLoja(21, "História da Domesticação", "", ConstantesDoSistema.Historia, false, 0);
        criarItemDaLoja(22, "História do Epílogo", "", ConstantesDoSistema.Historia, false, 0);
        //EXTRAS
                                           
    }
    private void criarItemDaLoja(int id, string nome, string descricao, int tipo, bool liberado, int preco)
    {
        ItemLoja itemLoja = new ItemLoja();
        itemLoja.Id = id;
        itemLoja.Nome = nome;
        itemLoja.Descricao = descricao;
        itemLoja.Tipo = tipo;
        itemLoja.Liberado = liberado;
        itemLoja.Preco = preco;
        itemLoja.Comprado = false;
        itemsDaLoja[id] = itemLoja;
    }
    private void VerificarSeJaExiste()
    {
        if (instancia == null)
            instancia = this;
        else if (instancia != this)
            Destroy(gameObject);
        DontDestroyOnLoad(gameObject);
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
