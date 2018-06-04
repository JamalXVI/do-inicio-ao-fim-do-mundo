using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class Opcoes : MonoBehaviour
{
    private Animator anim;
    [SerializeField]
    private Dropdown dropdownResolucoes;
    [SerializeField]
    private Toggle fullscreen;
    [SerializeField]
    private Toggle toggleSom;
    [SerializeField]
    private Slider sliderSom;
    [SerializeField]
    private AudioClip somMudar;
    [SerializeField]
    private Toggle toggleMusica;
    [SerializeField]
    private Slider sliderMusica;
    private Boolean iniciouConfiguracao = false;
    public GameObject transcanvas;
    // Use this for initialization
    void Start()
    {
        anim = GetComponent<Animator>();
        PreencherDropDown();
        VerificarVolumeSomInicial();
        VerificarVolumeMusicaInicial();
        iniciouConfiguracao = true;
    }

    private void VerificarVolumeSomInicial()
    {
        if (PlayerPrefs.GetFloat("volumeSom") > 0f)
        {
            toggleSom.isOn = false;
            sliderSom.value = PlayerPrefs.GetFloat("volumeSom");
        }
        else
        {
            toggleSom.isOn = true;
            sliderSom.value = 0f;
        }
    }

    private void VerificarVolumeMusicaInicial()
    {
        if (PlayerPrefs.GetFloat("volumeMusica") > 0f)
        {
            toggleMusica.isOn = false;
            sliderMusica.value = PlayerPrefs.GetFloat("volumeMusica");
        }
        else
        {
            toggleMusica.isOn = true;
            sliderMusica.value = 0f;
        }
    }

    private void PreencherDropDown()
    {
        dropdownResolucoes.ClearOptions();
        foreach (Resolution resolucao in Screen.resolutions)
        {
            string opcao = resolucao.width + "x" + resolucao.height;
            dropdownResolucoes.options.Add(new Dropdown.OptionData() { text = opcao });
            if (PlayerPrefs.HasKey("resolucao") && PlayerPrefs.GetString("resolucao").Equals(opcao))
            {
                dropdownResolucoes.value = dropdownResolucoes.options.Count;
            }
        }
        fullscreen.isOn = PlayerPrefs.GetInt("fullscreen") == 1 ? true : false;
    }

    public void MudarResolucao(int i)
    {
        if (i > QualitySettings.GetQualityLevel())
        {
            QualitySettings.IncreaseLevel();
            MudarResolucao(i);
        }else if(i < QualitySettings.GetQualityLevel())
        {
            QualitySettings.DecreaseLevel();
            MudarResolucao(i);
        }
    }
    public void MudarResolucao()
    {
        if (!iniciouConfiguracao)
        {
            return;
        }
        string opcao = dropdownResolucoes.options[dropdownResolucoes.value].text;
        Screen.SetResolution(int.Parse(opcao.Split('x')[0]), int.Parse(opcao.Split('x')[1]), fullscreen.isOn);
        PlayerPrefs.SetString("resolucao", opcao);
        PlayerPrefs.SetInt("fullscreen", (fullscreen.isOn ? 1 : 0));
        PlayerPrefs.GetInt("fullscreen");
    }
    public void ZerarVolumeSom()
    {
        if (toggleSom.isOn)
        {
            sliderSom.value = 0f;
            PlayerPrefs.SetFloat("volumeSom", sliderSom.value);

        }
    }
    public void ZerarVolumeMusica()
    {
        if (toggleMusica.isOn)
        {
            sliderMusica.value = 0f;
            PlayerPrefs.SetFloat("volumeMusica", sliderMusica.value);

        }
    }
    public void MudarVolumeSom()
    {
        if (!iniciouConfiguracao)
        {
            return;
        }
        if (sliderSom.value > 0f)
        {
            toggleSom.isOn = false;
            PlayerPrefs.SetFloat("volumeSom", sliderSom.value);
            SoundManager.instance.PlaySingle(somMudar);
        }
        else
        {
            toggleSom.isOn = true;

            PlayerPrefs.SetFloat("volumeSom", 0f);
        }

    }
    public void MudarVolumeMusica()
    {
        if (!iniciouConfiguracao)
        {
            return;
        }
        if (sliderMusica.value > 0f)
        {
            toggleMusica.isOn = false;
            PlayerPrefs.SetFloat("volumeMusica", sliderMusica.value);
        }
        else
        {
            toggleMusica.isOn = true;

            PlayerPrefs.SetFloat("volumeMusica", 0f);
        }

    }
    // Update is called once per frame
    void Update()
    {

    }
    public void ClicarControles()
    {
        anim.SetBool("Controle", true);
    }
    public void ClicarOk()
    {
        if (anim.GetBool("Controle"))
        {
            Game_Input.TranscreverControles();
        }
        else
        {

        }
        CarregarFase(Game_Player.game_player.troca_fases);
    }
    private void CarregarFase(int i)
    {
        Game_Temp.instancia.TrocarFase = true;
        Game_Temp.instancia.FaseTroca = i;
        Game_Temp.instancia.SemSalvar = true;
        transcanvas.SetActive(true);
    }
    public void ClicarVoltar()
    {
        if (anim.GetBool("Controle"))
        {
            anim.SetBool("Controle", false);
        }
        else
        {
            Application.LoadLevel(Game_Player.game_player.troca_fases);
        }
    } 

}
