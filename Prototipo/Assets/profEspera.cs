using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using NativeWebSocket;
using TMPro;



public class profEspera : MonoBehaviour, IClient
{

    private ConnectionManager cm = ConnectionManager.getInstance();

    //Botões
    public Button btnIniciarSessao;


    //Quadros em tela
    [SerializeField] private Transform ContentQuadros;
    [SerializeField] private GameObject prefabQuadros;
    [SerializeField] private Transform ContentCodigos;
    [SerializeField] private GameObject prefabCodigos;
    [SerializeField] private int m_ItemsToGenerate;

    private List<QuadroEquipe> quadrosEquipe = new List<QuadroEquipe>();
    private List<GameObject> quadrosCodigo = new List<GameObject>();

    public Vector3 escalaDesejada;


    //messagetype: SESSAO_CRIADA
    public void MSG_SESSAO_CRIADA(string msgJSON)
    {
        //Classe para tratar a mensagem
        msgSESSAO_CRIADA message = JsonUtility.FromJson<msgSESSAO_CRIADA>(msgJSON);

        //Salva os dados que precisam ser permanentes da mensagem
        dadosTimes.listaTimes = message.teams;
        Manager.sessionId = message.sessionId;

        // //Teste
        // Debug.Log("Message Type: " + messageType);
        // Debug.Log("Session ID: " + sessionId);
        // foreach (Team team in teams) 
        // {
        //     Debug.Log("Team ID: " + team.id);
        //     Debug.Log("Team Secret: " + team.secret);
        // }

        for (int i = 0; i < quadrosEquipe.Count; i++)
        {


            GameObject quadroEquipe = quadrosEquipe[i].obj;
            quadrosEquipe[i].id = dadosTimes.listaTimes[i].id;
            TextMeshProUGUI[] textFieldsQ = quadroEquipe.GetComponentsInChildren<TextMeshProUGUI>();

            GameObject quadroCodigo = quadrosCodigo[i];
            TextMeshProUGUI[] textFieldsC = quadroCodigo.GetComponentsInChildren<TextMeshProUGUI>();

            foreach (TextMeshProUGUI textFieldQ in textFieldsQ)
            {
                if (textFieldQ.CompareTag("txt_equipe"))
                {
                    if (dadosTimes.listaTimes.Count > i)
                    {
                        textFieldQ.text = "Equipe " + quadrosEquipe[i].id + " " + Manager.sessionId + "@" + dadosTimes.listaTimes[i].secret;
                    }
                    break;
                }
            }

            foreach (TextMeshProUGUI textFieldC in textFieldsC)
            {
                if (textFieldC.CompareTag("txt_codigo"))
                {
                    if (dadosTimes.listaTimes.Count > i)
                    {
                        textFieldC.text = "Equipe " + dadosTimes.listaTimes[i].id + " " + Manager.sessionId + "@" + dadosTimes.listaTimes[i].secret;
                    }
                    break;
                }
            }



        }
    }


    public void MSG_ENTROU_SESSAO(string msgJSON) 
    {

        msgENTROU_SESSAO message = JsonUtility.FromJson<msgENTROU_SESSAO>(msgJSON);

        dadosTimes.addPlayer(message.user, message.teamId);

        for (int i = 0; i < quadrosEquipe.Count; i++)
        {
            if (quadrosEquipe[i].id == message.teamId)
            {
                GameObject quadroEquipe = quadrosEquipe[i].obj;
                TextMeshProUGUI[] textFieldsQ = quadroEquipe.GetComponentsInChildren<TextMeshProUGUI>();

                foreach (TextMeshProUGUI textFieldQ in textFieldsQ)
                {
                    if (textFieldQ.CompareTag("txt_players"))
                    {
                        if (dadosTimes.listaTimes.Count > i)
                        {
                            if (textFieldQ.text != "")
                            {
                                textFieldQ.text += "\n";
                            }
                            textFieldQ.text += message.user.name;
                        }
                        break;
                    }
                }
            }
        }

    }

    public void btnComecarJogo(){

        var msg = new ComecarJogo("COMECAR_JOGO", Manager.sessionId, Manager.gameId);

        cm.send(msg);

        // SceneManager.LoadScene("");
    }




    public void handle(string ms){
        //string messageType = ms.messageType;

        //executa JSON->messageType dentro do handle
        string messageType = JsonUtility.FromJson<ServerMessage>(ms).messageType;


        // route message to handler based on message type
        Debug.Log(ms);

        if (messageType == "SESSAO_CRIADA") 
        {
            MSG_SESSAO_CRIADA(ms);
        }
        else if(messageType == "ENTROU_SESSAO")
        {
            MSG_ENTROU_SESSAO(ms);
        }

    }
    
    // Start is called before the first frame update
    void Start()
    {
        m_ItemsToGenerate = Manager.nrTeam;
        for (int i = 0; i < m_ItemsToGenerate; i++)
        {
            GameObject novoQuadro = Instantiate(prefabQuadros, transform.position, Quaternion.identity);
            novoQuadro.transform.SetParent(ContentQuadros);
            novoQuadro.transform.localScale = new Vector3(0.75f, 0.75f, 0.75f);

            QuadroEquipe novo = new QuadroEquipe(0, novoQuadro);
        
            quadrosEquipe.Add(novo);

            GameObject novoCodigo = Instantiate(prefabCodigos, transform.position, Quaternion.identity);
            novoCodigo.transform.SetParent(ContentCodigos);
            novoCodigo.transform.localScale = new Vector3(1.894364f, 0.179433f, 0.23102f);
        
            quadrosCodigo.Add(novoCodigo);
        }
        
    }

    // Update is called once per frame
    void Update()
    {

/*
        bool isFieldEmpty = string.IsNullOrEmpty(inputModerator.text);
        btnCadastrar.interactable = !isFieldEmpty;
*/
        cm.retrieveMessages(this);

        
    }
        
}

[System.Serializable] 
public class msgSESSAO_CRIADA
{
    public string messageType;
    public List<Team> teams;
    public int sessionId;
}

[System.Serializable] 
public class msgENTROU_SESSAO
{
    public string messageType;
    public User user;
    public int teamId;
    public int sessionId;
    public int gameId;
}

[System.Serializable]
public class QuadroEquipe
{
    public int id;
    public GameObject obj;

    public QuadroEquipe(int n, GameObject o)
    {
        id = n;
        obj = o;
    }
}