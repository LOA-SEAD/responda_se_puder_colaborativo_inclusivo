using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.IO;

public class carregaDados : MonoBehaviour
{
    private static string arquivo = "DadosResponda.json";
    private static string path;

    private static string[] conteudoJSON;
    private static string[] conteudoURL;

    public static bool isLoadedJSON = false;
    public static bool isLoadedURL = false;
    
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

    //     public static void Load()
    //     { 
    //         BetterStreamingAssets.Initialize();
    //         listaDados.Clear();
    //         listaBase.Clear();
    //         var jsonText = BetterStreamingAssets.ReadAllLines(arquivo);

    //         foreach (var line in jsonText)
    //         {
    //             DadosJogo DadosJson = JsonUtility.FromJson<DadosJogo>(line);
    //             DadosJogo dados = new DadosJogo();
    //             dados.pergunta = DadosJson.pergunta;
    //             dados.resposta = DadosJson.resposta;
    //             dados.r2 = DadosJson.r2;
    //             dados.r3 = DadosJson.r3;
    //             dados.r4 = DadosJson.r4;
    //             dados.dica = DadosJson.dica;
    //             dados.nivel = DadosJson.nivel;
    //             if (dados.nivel == "facil") {
    //                 Manager.totalQuestoes++;
    //                 Manager.totalFacil++;
    //             }
    //             else if (dados.nivel == "medio") {
    //                 Manager.totalQuestoes++;
    //                 Manager.totalMedio++;
    //             }
    //             else {
    //                 Manager.totalQuestoes++;
    //                 Manager.totalDificil++;
    //             }

    //             listaDados.Add(dados);
    //             listaBase.Add(dados);
    //         }
    //     }
    // }

    public static void Load(MonoBehaviour behaviour)
    {

        if (!isLoadedJSON || !isLoadedURL)
        {
            Debug.Log("[CarregaDados] - Load() - Inicio");
            Debug.Log("[CarregaDados] - Limpando listaDados - Inicio");
            isLoadedJSON = isLoadedURL = false;
            listEasy.Clear();
            listMedium.Clear();
            listHard.Clear();
            Debug.Log("[CarregaDados] - Limpando listaDados - Fim");
            #if UNITY_WEBGL
                Debug.Log("[CarregaDados] - UNITY_WEBGL - Inicio");
                behaviour.StartCoroutine(GetByHTTP());
                behaviour.StartCoroutine(Processa());
                Debug.Log("[CarregaDados] - UNITY_WEBGL - Fim");        
            #else
                GetByBSA();
            #endif
        }
        
        Debug.Log("[CarregaDados] - Load() - Fim");
    }

    private static IEnumerator GetByHTTP()
    {
        Debug.Log("[CarregaDados] - GetByHTTP() - Inicio");
        
        Debug.Log(Application.streamingAssetsPath);
        
        string URL = Path.Combine(Application.streamingAssetsPath, "url.txt");
        Debug.Log(URL);
        
        using (UnityWebRequest www = UnityWebRequest.Get(URL))
        {
            yield return www.SendWebRequest();

            if (www.isNetworkError)
            {
                Debug.Log(www.error);
            }
            else
            {
                conteudoURL = www.downloadHandler.text.Split('\n');
                isLoadedURL = true;
            }
            www.Dispose();
        }
        
        URL = Path.Combine(Application.streamingAssetsPath, arquivo);
        Debug.Log(URL);

        using (UnityWebRequest www = UnityWebRequest.Get(URL))
        {
            yield return www.SendWebRequest();

            if (www.isNetworkError)
            {
                Debug.Log(www.error);
            }
            else
            {
                conteudoJSON = www.downloadHandler.text.Split('\n');
                isLoadedJSON = true;
            }
            www.Dispose();
        }

        Debug.Log("[CarregaDados] - GetByHTTP() - Fim");
    }
    
    private static IEnumerator Processa()
    {
        Debug.Log("[CarregaDados] Processa() - Inicio");
        while (!isLoadedJSON || !isLoadedURL) {
            yield return new WaitForSeconds(0.1f);
        }
        
        foreach (string line in conteudoJSON)
        {
            DadosJogo DadosJson = JsonUtility.FromJson<DadosJogo>(line);
            DadosJogo dados = new DadosJogo();
            dados.pergunta = DadosJson.pergunta;
            dados.resposta = DadosJson.resposta;
            dados.r2 = DadosJson.r2;
            dados.r3 = DadosJson.r3;
            dados.r4 = DadosJson.r4;
            dados.dica = DadosJson.dica;
            dados.nivel = DadosJson.nivel;
            if (dados.nivel == "facil")
            {
                Manager.totalQuestoes++;
                Manager.totalFacil++;
                listEasy.Add(dados);
            }
            else if (dados.nivel == "medio")
            {
                Manager.totalQuestoes++;
                Manager.totalMedio++;
                listMedium.Add(dados);
            }
            else
            {
                Manager.totalQuestoes++;
                Manager.totalDificil++;
                listHard.Add(dados);
            }
        }
        Manager.serverURL = conteudoURL[0];
        Debug.Log("[CarregaDados] - Processa() - listEasy.Count = " + listEasy.Count);
        Debug.Log("[CarregaDados] - Processa() - listMedium.Count = " + listMedium.Count);
        Debug.Log("[CarregaDados] - Processa() - listHard.Count = " + listHard.Count);
        Debug.Log("[CarregaDados] - Processa() - Manager.serverURL = " + Manager.serverURL);
        Debug.Log("[CarregaDados] - Processa() - Fim");
    }

    private static void GetByBSA()
    {
        Debug.Log("[CarregaDados] - GetByBSA() - Inicio");
        BetterStreamingAssets.Initialize();
        var jsonText = BetterStreamingAssets.ReadAllLines(arquivo);
        foreach (var line in jsonText)
        {
            DadosJogo DadosJson = JsonUtility.FromJson<DadosJogo>(line);
            DadosJogo dados = new DadosJogo();
            dados.pergunta = DadosJson.pergunta;
            dados.resposta = DadosJson.resposta;
            dados.r2 = DadosJson.r2;
            dados.r3 = DadosJson.r3;
            dados.r4 = DadosJson.r4;
            dados.dica = DadosJson.dica;
            dados.nivel = DadosJson.nivel;

            if (dados.nivel == "facil")
            {
                Manager.totalQuestoes++;
                Manager.totalFacil++;
                listEasy.Add(dados);
            }
            else if (dados.nivel == "medio")
            {
                Manager.totalQuestoes++;
                Manager.totalMedio++;
                listMedium.Add(dados);
            }
            else
            {
                Manager.totalQuestoes++;
                Manager.totalDificil++;
                listHard.Add(dados);
            }
        }

        Manager.serverURL = BetterStreamingAssets.ReadAllLines("url.txt")[0];
        
        isLoadedJSON = isLoadedURL = true;
        
        Debug.Log("[CarregaDados] listEasy.Count = " + listEasy.Count);
        Debug.Log("[CarregaDados] listMedium.Count = " + listMedium.Count);
        Debug.Log("[CarregaDados] listHard.Count = " + listHard.Count);
        Debug.Log("[CarregaDados] Manager.serverURL = " + Manager.serverURL);
        Debug.Log("[CarregaDados] - GetByBSA() - Fim");
    }

    /* public static void Load()
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
            if (dados.nivel == "facil")
            {
                Manager.totalQuestoes++;
                Manager.totalFacil++;
                listEasy.Add(dados);
            }
            else if (dados.nivel == "medio")
            {
                Manager.totalQuestoes++;
                Manager.totalMedio++;
                listMedium.Add(dados);
            }
            else
            {
                Manager.totalQuestoes++;
                Manager.totalDificil++;
                listHard.Add(dados);
            }

            // listaDados.Add(dados);
            // listaBase.Add(dados);
        }

        Manager.serverURL = BetterStreamingAssets.ReadAllLines("url.txt")[0];
    }*/

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
        string[] alt = { dados.resposta, dados.r2, dados.r3, dados.r4 };
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


    // public string audio_pergunta;
    // public string audio_dica;
    // public string[] audio_alternativas = new string[4];
}