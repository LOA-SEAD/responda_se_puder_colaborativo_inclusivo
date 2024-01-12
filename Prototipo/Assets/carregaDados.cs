using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using System.IO;

public class carregaDados : MonoBehaviour
{
    private static string arquivo = "DadosResponda.json";
    private static string path;

    public static List<DadosJogo> listEasy = new List<DadosJogo>();
    public static List<DadosJogo> listMedium = new List<DadosJogo>();
    public static List<DadosJogo> listHard = new List<DadosJogo>();




    public static List<DadosJogo> listaDados = new List<DadosJogo>();

    public static List<DadosJogo> listaBase = new List<DadosJogo>();

    // Start is called before the first frame update
    void Start()
    {
        //path = Application.persistentDataPath + '/' + arquivo;
        //path = Application.streamingAssetsPath + '/' + arquivo;
        //Debug.Log("Caminho: " + path);
        //Load();
    }

    public static void LoadURL()
    {
        BetterStreamingAssets.Initialize();

        Manager.serverURL = BetterStreamingAssets.ReadAllLines("url.txt")[0];


    }

    public static void Load()
    { 
        BetterStreamingAssets.Initialize();
        listaDados.Clear();
        listaBase.Clear();
        var jsonText = BetterStreamingAssets.ReadAllLines(arquivo);
        
        for (int i = 0; i < jsonText.Length; i++)
        {
            DadosJogo DadosJson = JsonUtility.FromJson<DadosJogo>(jsonText[i]);
            DadosJogo dados = new DadosJogo();
            dados.pergunta = DadosJson.pergunta;
            dados.resposta = DadosJson.resposta;
            dados.r2 = DadosJson.r2;
            dados.r3 = DadosJson.r3;
            dados.r4 = DadosJson.r4;
            dados.dica = DadosJson.dica;
            dados.nivel = DadosJson.nivel;
            if (dados.nivel == "facil") {
                Manager.totalQuestoes++;
                Manager.totalFacil++;
                listEasy.Add(dados);
            }
            else if (dados.nivel == "medio") {
                Manager.totalQuestoes++;
                Manager.totalMedio++;
                listMedium.Add(dados);
            }
            else {
                Manager.totalQuestoes++;
                Manager.totalDificil++;
                listHard.Add(dados);
            }

        }


    }


    public static void Select()
    {
            foreach (int i in Manager.qEasy)
            {
                if (i >= 0 && i < listEasy.Count)
                {
                    listaDados.Add(listEasy[i]);
                    listaBase.Add(listEasy[i]);
                }
            }

            foreach (int i in Manager.qMedium)
            {
                if (i >= 0 && i < listMedium.Count)
                {
                    listaDados.Add(listMedium[i]);
                    listaBase.Add(listMedium[i]);
                }
            }

            foreach (int i in Manager.qHard)
            {

                if (i >= 0 && i < listHard.Count)
                {
                    listaDados.Add(listHard[i]);
                    listaBase.Add(listHard[i]);
                }
            }
    }

    public static void Shuffle(ref DadosJogo dados, int[] p)
    {
        string[] alt = {dados.resposta, dados.r2, dados.r3, dados.r4};
        List<string> novaOrdem = new List<string>();

        foreach (int i in p)
        {
            if (i >= 0 && i < alt.Length)
            {
                novaOrdem.Add(alt[i]);
            }
        }

        if (novaOrdem.Count == alt.Length)
        {
            dados.resposta = novaOrdem[0];
            dados.r2 = novaOrdem[1];
            dados.r3 = novaOrdem[2];
            dados.r4 = novaOrdem[3];
        }

        
    }
}

[System.Serializable]
public class DadosJogo
{
    public string pergunta;
    public string resposta;
    public string r2;
    public string r3;
    public string r4;
    public string dica;
    public string nivel;
        
}