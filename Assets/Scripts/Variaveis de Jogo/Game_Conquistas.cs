using UnityEngine;
using System.Collections;

public class Game_Conquistas : MonoBehaviour {
    public static Game_Conquistas instancia;
    public Conquista[] conquistas;
    private Variaveis_Conquista variaveisConquista;

    public Variaveis_Conquista VariaveisConquista
    {
        get { return variaveisConquista; }
        set { variaveisConquista = value; }
    }

	// Use this for initialization
	void Awake() {
        variaveisConquista = new Variaveis_Conquista();
        IniciarVariaveis();
        VerificarSeJaExiste();
	}
	private void IniciarVariaveis()
    {
        conquistas = new Conquista[30];
        //Morte
        criarConquista(1, "Atrasado", "Deixar o tempo acabar em uma fase.");
        criarConquista(2, "Paradoxo", "Deixar o tempo acabar 3 vezes.");
        criarConquista(3, "Não Alimente os Animais", "Morrer para qualquer animal de caça.");
        criarConquista(4, "Sem Repelente", "Morrer para os mosquitos.");
        criarConquista(5, "Carocha", "Morrer para os escaravelhos.");
        criarConquista(6, "Mumificado", "Deixar o tempo acabar na fase 2-2.");
        criarConquista(7, "Pitfall", "Cair em qualquer buraco.");
        criarConquista(8, "Sem Chão", "Cair 10 vezes em buracos.");
        criarConquista(9, "Tigre ou Eufrades?", "Cair em um buraco na fase 2-1.");
        //COLETÁVEIS
        criarConquista(10, "60/60", "Pegar todos os engrenológicos de uma fase.");
        criarConquista(11, "Magnata", "Coletar 300 engrenológicos no total.");
        criarConquista(12, "Cheio de Tempo", "Coletar 900 engrenológicos no total.");
        criarConquista(13, "Salada de Frutas", "Coletar todas as frutas de uma fase.");
        criarConquista(14, "Potássio", "Coletar 5 bananas.");
        criarConquista(15, "Dentistas recomendam", "Coletar 6 maçãs.");
        criarConquista(16, "Muitos Problemas", "Coletar 7 abacaxis.");
        criarConquista(17, "O que é que a baiana tem?", "Coletar 9 de cada fruta.");
        criarConquista(18, "Caçador", "Coletar 1  animal.");
        criarConquista(19, "Biodiversidade", "Coletar 4 animais diferentes.");
        criarConquista(20, "Jogo do Mico", "Coletar 2x 2 animais diferentes.");
        //ESTÁGIO
        criarConquista(21, "Olá Mundo", "Completar a primeira fase.");
        criarConquista(22, "A roda e o fogo", "Completar o primeiro mundo.");
        criarConquista(23, "Monotonia", "Completar 5x a primeira fase.");
        criarConquista(24, "Do Início", "Completar o segundo período.");
        criarConquista(25, "O Melhor", "Conseguir A em duas fases.");
        criarConquista(26, "Mestre da Pedra", "Conseguir A nas três primeiras fases.");
        criarConquista(27, "Mitológico", "Conseguir A nas três fases do segundo período.");
        criarConquista(28, "Nossa Hitória", "Completar qualquer Quizz.");
        criarConquista(29, "Sapiens", "Completar os quizzes do primeiro período.");
        criarConquista(30, "Historiador", "Completar os quizzes do segundo período.");

    }
	// Update is called once per frame
	void Update () {
	}
    private void criarConquista(int id, string nome, string descricao)
    {
        Conquista c  = new Conquista { Nome = nome, Id = id, Descricao = descricao, Completada = false, Anunciada = false };
        conquistas[id - 1] = c;
    }
    private void VerificarSeJaExiste()
    {
        if (instancia == null)
            instancia = this;
        else if (instancia != this)
            Destroy(gameObject);
        DontDestroyOnLoad(gameObject);
    }
    public void AdicionarConquistas(int id)
    {
        if (!conquistas[id-1].Completada)
        {
            var conquista = conquistas[id-1];
            conquista.Completada = true;
            conquista.Anunciada = false;
        }
    }

    public void AdicionarBuraco()
    {
        VariaveisConquista.CaidasNoBuraco++;
        if (VariaveisConquista.CaidasNoBuraco > 0)
        {
            AdicionarConquistas(7);
        }
        if (VariaveisConquista.CaidasNoBuraco > 10)
        {
            AdicionarConquistas(8);
        }
    }
    public void AdicionarPerdaTempo()
    {
        VariaveisConquista.PerdasPorTempo++;
        if (VariaveisConquista.PerdasPorTempo > 0)
        {
            AdicionarConquistas(1);
        }
        if (VariaveisConquista.PerdasPorTempo > 2)
        {
            AdicionarConquistas(2);
        }
    }
    public void AdicionarMortePorAnimais(string animal)
    {
        VariaveisConquista.PerdasPorAnimais++;
        if (VariaveisConquista.PerdasPorAnimais > 0)
        {
            AdicionarConquistas(3);
        }
    }
    public void SemRepelente()
    {
        AdicionarConquistas(4);
    }
    public void PassarDeFase(int nivel)
    {
        VariaveisConquista.PassouFase++;
        if (nivel == 0)
        {
            VariaveisConquista.PassouFaseUm++;
            if (VariaveisConquista.PassouFaseUm > 0)
            {
                AdicionarConquistas(21);
            }
            if (VariaveisConquista.PassouFaseUm > 4)
            {
                AdicionarConquistas(23);
            }
        }
    }
    public void ColetarFruta(int fruta)
    {
        VariaveisConquista.QuantidadeFrutas++;
        AdicionarFruta(fruta);
        if (VariaveisConquista.QuantidadeAbacaxis > 6)
        {
            AdicionarConquistas(16);
        }
        if (VariaveisConquista.QuantidadeBananas > 4)
        {
            AdicionarConquistas(14);
        }
        if (VariaveisConquista.QuantidadeMacas > 5)
        {
            AdicionarConquistas(15);
        }

        if(VariaveisConquista.QuantidadeAbacaxis > 8 &&
           VariaveisConquista.QuantidadeBananas > 8 &&
           VariaveisConquista.QuantidadeMacas > 8 &&
           VariaveisConquista.QuantidadeManga > 8 &&
           VariaveisConquista.QuantidaeCaju > 8)
        {
            AdicionarConquistas(17);
        }
    }
    public void ConquistaTodasAsFrutas()
    {
        AdicionarConquistas(13);
    }
    private void AdicionarFruta(int fruta)
    {
        switch (fruta)
        {
            case 1: //ABACAXI
                VariaveisConquista.QuantidadeAbacaxis++;
                break;
            case 2: //BANANA
                VariaveisConquista.QuantidadeBananas++;
                break;
            case 3: //CAJU
                VariaveisConquista.QuantidaeCaju++;
                break;
            case 4: //Maçã
                VariaveisConquista.QuantidadeMacas++;
                break;
            case 5: //MANGA
                VariaveisConquista.QuantidadeManga++;
                break;
            default:
                break;
        }
    }
    public void AdicionarConquistaPontuacao()
    {
        AdicionarConquistas(10);
    }
    public void AdicionarAnimal(string animal)
    {
        AdicionarVarAnimal(animal);
        if (variaveisConquista.Macaco > 0 || variaveisConquista.Cobra > 0 || variaveisConquista.Aguia > 0 || variaveisConquista.Bode > 0)
        {
            AdicionarConquistas(18);
        }
        if (VerificarJogoDoMico())
        {
            AdicionarConquistas(20);
        }
    }
    private bool VerificarJogoDoMico()
    {
        if (variaveisConquista.Macaco > 1)
        {
            if (variaveisConquista.Cobra > 1)
            {
                return true;
            }
            if (variaveisConquista.Aguia > 1)
            {
                return true;
            }
            if (variaveisConquista.Bode > 1)
            {
                return true;
            }
        }
        if (variaveisConquista.Cobra > 1)
        {
            if (variaveisConquista.Aguia > 1)
            {
                return true;
            }
            if (variaveisConquista.Bode > 1)
            {
                return true;
            }
        }
        if (variaveisConquista.Aguia > 1)
        {
            if (variaveisConquista.Bode > 1)
            {
                return true;
            }
        }
        return false;
    }
    private void AdicionarVarAnimal(string animal)
    {
        switch (animal)
        {
            case "macaco":
                variaveisConquista.Macaco++;
                break;
            case "aguia":
                variaveisConquista.Aguia++;
                break;
            case "cobra":
                variaveisConquista.Cobra++;
                break;
            case "bode":
                variaveisConquista.Bode++;
                break;
            default:
                break;
        }
    }

    public void VerificarLiberarConquistasMoedas(int numeroMoedas)
    {
        if (numeroMoedas >= 60)
        {
            AdicionarConquistaPontuacao();
        }
        if (Game_Player.game_player.Moedas_Jogo + Game_Player.game_player.MoedasGastas >= 300)
        {
            AdicionarConquistas(11);
        }
        if (Game_Player.game_player.Moedas_Jogo + Game_Player.game_player.MoedasGastas >= 900)
        {
            AdicionarConquistas(12);
        }
    }
}
    