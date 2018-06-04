using UnityEngine;
using System.Collections;
using System;
[Serializable]
public class Game_Temp : Game_Base
{
    public static Game_Temp instancia;
    private int faseTroca;
    private bool trocarFase;
    private bool semSalvar;
    private bool sinalizarSocolinha;
    private bool sinalizarPrimitivo;
    private bool notificaoLoja;
    private bool terminouFase;
    private int idMusicaLiberar;
    [SerializeField]
    private int[] historiasFases;
    [SerializeField]
    private int nivelPerguntas;

    public int NivelPerguntas
    {
        get { return nivelPerguntas; }
        set { nivelPerguntas = value; }
    }
    
    private bool ehHistoria;

    public bool EhHistoria
    {
        get { return ehHistoria; }
        set { ehHistoria = value; }
    }
    
    public int[] HistoriasFases
    {
        get { return historiasFases; }
        set { historiasFases = value; }
    }

    public int IdMusicaLiberar
    {
        get { return idMusicaLiberar; }
        set { idMusicaLiberar = value; }
    }

    public bool TerminouFase
    {
        get { return terminouFase; }
        set { terminouFase = value; }
    }
    
    public bool NotificacaoLoja
    {
        get { return notificaoLoja; }
        set { notificaoLoja = value; }
    }
    
    public bool SinalizarPrimitivo
    {
        get { return sinalizarPrimitivo; }
        set { sinalizarPrimitivo = value; }
    }
    
    public bool SinalizarSocolinha
    {
        get { return sinalizarSocolinha; }
        set { sinalizarSocolinha = value; }
    }
    
    public bool SemSalvar
    {
        get { return semSalvar; }
        set { semSalvar = value; }
    }
    

    public bool TrocarFase
    {
        get { return trocarFase; }
        set { trocarFase = value; }
    }
    
    public int FaseTroca
    {
        get { return faseTroca; }
        set { faseTroca = value; }
    }
    
    void Awake()
    {
        VerificarSeJaExiste();
    }
    private void VerificarSeJaExiste()
    {
        if (instancia == null)
            instancia = this;
        else if (instancia != this)
            Destroy(gameObject);
        DontDestroyOnLoad(gameObject);
    }
    public void ZerarValoresTemporarios()
    {
        sinalizarPrimitivo = false;
        sinalizarSocolinha = false;
        ehHistoria = false;
        terminouFase = false;
        idMusicaLiberar = 0;
    }
}
