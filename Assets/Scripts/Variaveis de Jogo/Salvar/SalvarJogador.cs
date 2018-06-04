using UnityEngine;
using System.Collections;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System;
using System.Collections.Generic;

[Serializable]
public class SalvarJogador{
	public SalvarSocolinha socolinha;
	public SalvarPrimitivo primitivo;
	public int[] ordem_fases;
	public bool[] fases_completas;
	public int vidas;
	public int vidas_extras;
	public int vidas_totais;
    public int moedas;
    public int moedas_gastas;
	public Nivel[] niveis;
    public Conquista[] conquistas;
    public ItemLoja[] itemsDaLoja;
    public bool[] historias;
    public bool[] id_moedas_quizz;
    public int[] quantidade_item;
    public int moedasQuizz;
    public bool fornecerInformacao;
    public bool jaFoiInformado;
    public Variaveis_Conquista variaveisConquistas;
    private Dictionary<string, KeyCode> teclas;
    public Nivel_Quizz[] nivelQuizz;
    public bool[] falasNoJogo;
    public static float VERSAO_ATUAL = 1.00057f;
    private float versaoAtual = VERSAO_ATUAL;

    public float VersaoAtual
    {
        get { return versaoAtual; }
        set { versaoAtual = value; }
    }


    /*
	SalvarJogador(int vidas, int vidas_extras, int vidas_totais, bool[] fases_completas, int[] ordem_fases, Primitivo primitivo, Socolinha socolinha)
	{
		this.vidas = vidas;
		this.vidas_extras = vidas_extras;
		this.vidas_totais = vidas_totais;
		this.fases_completas = fases_completas;
		this.ordem_fases = ordem_fases;
		this.primitivo = primitivo;
		this.socolinha = socolinha;
	}
	*/
    public static bool compararVersao(SalvarJogador sv)
    {
        if (sv.versaoAtual == VERSAO_ATUAL)
        {
            return true;
        }
        return false;
    }
}
