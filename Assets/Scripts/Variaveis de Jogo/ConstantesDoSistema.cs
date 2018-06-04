using UnityEngine;
using System.Collections;
using System.Net.NetworkInformation;

public class ConstantesDoSistema : MonoBehaviour {
    public static string Endereco = "http://diafdm.doinicioaofimdomundo.com/";
    public static string Caminho = Application.persistentDataPath;
    public static int PotuacaoMaximaFase01 = 3000;
    public static int PotuacaoMaximaFase02 = 3900;
    public static int PotuacaoMaximaFase03 = 3400;
    public static int PotuacaoMaximaFase04 = 3600;
    public static int PotuacaoMaximaFase05 = 3600;
    public static int PotuacaoMaximaFase06 = 4900;

    //ID DE ITENS FIXO
    public static int IdMacaco = 8;
    public static int IdCobra = 9;
    public static int IdAguia = 10;
    public static int IdCabra = 11;
    public static int IdPedra = 12;
    public static int IdEscaravelho = 13;

    //TIPOS DE  ITENS DA LOJA
    public static int Consumivel = 1;
    public static int Historia = 2;
    public static int Arte = 3;
    public static int Musica = 4;
    public static int FasesExtras = 5;
    public static int Codex = 6;
    public static string RetornarMac()
    {
        string info = "";
        IPGlobalProperties computerProperties = IPGlobalProperties.GetIPGlobalProperties();
        NetworkInterface[] nics = NetworkInterface.GetAllNetworkInterfaces();

        foreach (NetworkInterface adapter in nics)
        {
            PhysicalAddress address = adapter.GetPhysicalAddress();
            byte[] bytes = address.GetAddressBytes();
            string mac = null;
            for (int i = 0; i < bytes.Length; i++)
            {
                mac = string.Concat(mac + (string.Format("{0}", bytes[i].ToString("X2"))));
                if (i != bytes.Length - 1)
                {
                    mac = string.Concat(mac + "");
                }
            }
            info += mac + "";
        }
        return info;
    }
}
