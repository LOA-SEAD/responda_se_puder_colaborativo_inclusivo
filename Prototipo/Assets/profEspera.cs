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

    //BTN
    public Button btnIniciarSessao;

    public GameObject quadroChat;
    public GameObject painelTexto;
    public GameObject painelChat;

    public InputField chatBox;

    [SerializeField]
    public List<msgCHAT> messageList = new List<msgCHAT>();

    public int chatMax = 25;


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
    //cria os quadros que serão apresentados em tela com o nome dos jogadores
    public void MSG_SESSAO_CRIADA(string msgJSON)
    {
        //Classe para tratar a mensagem
        msgSESSAO_CRIADA message = JsonUtility.FromJson<msgSESSAO_CRIADA>(msgJSON);

        //Salva os dados que precisam ser permanentes da mensagem
        dadosTimes.listaTimes = message.teams;
        Manager.sessionId = message.sessionId;
        Manager.moderator.name = message.user.name;
        Manager.moderator.id = message.user.id;

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

            // Debug.Log("Quadro ID: " + quadrosEquipe[i].id);
            // Debug.Log("Time ID: " + dadosTimes.listaTimes[i].id);
            

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

    //messageType: ENTROU_SESSAO
    //coloca no quadro da equipe correta o nome do aluno que acessou o jogo e adiciona o aluno no time correto na base de dadosTimes
    public void MSG_ENTROU_SESSAO(string msgJSON) 
    {
        msgENTROU_SESSAO message = JsonUtility.FromJson<msgENTROU_SESSAO>(msgJSON);

        Debug.Log(message.teamId);
        
        dadosTimes.addPlayer(message.user, message.teamId);
        
        foreach (var quadroEquipe in quadrosEquipe)
        {
            if (quadroEquipe.id == message.teamId)
            {
                GameObject quadroObj = quadroEquipe.obj;
                TextMeshProUGUI[] textFieldsQ = quadroObj.GetComponentsInChildren<TextMeshProUGUI>();

                foreach (TextMeshProUGUI textFieldQ in textFieldsQ)
                {
                    if (textFieldQ.CompareTag("txt_players"))
                    {
                        int teamIndex = quadrosEquipe.FindIndex(quadro => quadro.id == message.teamId);
                        if (teamIndex >= 0 && teamIndex < dadosTimes.listaTimes.Count)
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

        SceneManager.LoadScene("profJogo");

    }

    // public void btnEncerrarJogo(){
        
    //     var msg = new EncerrarJogo("ENCERRAR_JOGO", Manager.moderator, Manager.sessionId, Manager.gameId);

    //     cm.send(msg);

    //     SceneManager.LoadScene("Menu Professor");

    // }

    public void MSG_CHAT(string msgJSON){
            Color cor;
            msgCHAT message = JsonUtility.FromJson<msgCHAT>(msgJSON);

            if(messageList.Count >= chatMax){
                Destroy(messageList[0].painelTexto.gameObject);
                messageList.Remove(messageList[0]);
            }

            msgCHAT textoChat = new msgCHAT();
            if(message.moderator)
                textoChat.texto = message.user.name + ":" + message.texto;//falou;
            else
                textoChat.texto = "Equipe " + message.teamId + " / " + message.user.name + ":" + message.texto;   

            GameObject novoChat = Instantiate(painelTexto, painelChat.transform);

            textoChat.painelTexto = novoChat.GetComponent<Text>();

            textoChat.painelTexto.text = textoChat.texto;

            if(message.moderator){
                ColorUtility.TryParseHtmlString("#f41004", out cor);
                textoChat.painelTexto.fontStyle = FontStyle.Bold;
            }
            else{
                //textoChat.painelTexto.fontStyle = FontStyle.Regular;
                    ColorUtility.TryParseHtmlString("#112A46", out cor);
            }

            textoChat.painelTexto.color = cor;
            messageList.Add(textoChat);

            Debug.Log(textoChat.texto);
        }





    public void handle(string ms){
        //string messageType = ms.messageType;

        //executa JSON->messageType dentro do handle
        string messageType = JsonUtility.FromJson<ServerMessage>(ms).messageType;
        Debug.Log(messageType);


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
        else if (messageType == "MENSAGEM_CHAT")
        {
            MSG_CHAT(ms);
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
       
        cm.retrieveMessages(this);

         if(chatBox.text != "")
        {
            if(Input.GetKeyDown(KeyCode.Return)){
               
                var msg = new mensagemChat("MENSAGEM_CHAT", Manager.moderator, Manager.teamId, Manager.sessionId, Manager.gameId, chatBox.text, true);
                //var msg = new mensagemChat("MENSAGEM_CHAT", dadosTimes.player, Manager.teamId, Manager.sessionId, Manager.gameId, chatBox.text, Manager.moderator);
                cm.send(msg);
               // readChat(chatBox.text);
                chatBox.text = "";
            }
        }
        
    }
        
}


[System.Serializable] 
public class msgSESSAO_CRIADA
{
    public string messageType;
    public List<Team> teams;
    public int sessionId;
    public User user;
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