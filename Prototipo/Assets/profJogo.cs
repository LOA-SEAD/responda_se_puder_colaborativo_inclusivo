using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using NativeWebSocket;
using System.IO;
using TMPro;



public class profJogo : MonoBehaviour, IClient
{

    private ConnectionManager cm = ConnectionManager.getInstance();

    private Dictionary<int, List<msgCHAT_moderator>> msgTeams = new Dictionary<int, List<msgCHAT_moderator>>();

    // private float transparencia = 0.0f;
    // private float sem_transparencia = 1.0f;
    public Image btnExclamacao;
    public int id_team;
    public int questionAmount = Manager.nrEasy + Manager.nrMedium + Manager.nrHard;
    public int equipes_terminadas = 0;

    // //BTN
    public Button encerrarJogo;

    public GameObject quadroChat;
    public GameObject painelTexto;
    public GameObject painelChat;
    public GameObject prefab_equipeJogo;
    public GameObject painelEncerrar;


    public TMP_InputField chatBox;

    public TextMeshProUGUI messageText;
    // public int teamID;
    GameObject chat_moderator;

    [SerializeField]
    public List<msgCHAT_moderator> messageList = new List<msgCHAT_moderator>();

    public int chatMax = 25;

    public ScrollRect scrollRect_prof;

    public int chatAberto = 0;

    [SerializeField]
    public List<GameObject> notification = new List<GameObject>();
    

    // //Quadros em tela
    [SerializeField] private Transform ContentEquipes;
    [SerializeField] private GameObject prefabEquipes;
    [SerializeField] private int m_Itens;

    private List<GameObject> quadrosEquipe = new List<GameObject>();

    // public Vector3 escalaDesejada;

    // public GameObject painelChat;

    // public void MSG_CHAT(string msgJSON){

    //         Color cor;
    //         msgCHAT_moderator message = JsonUtility.FromJson<msgCHAT_moderator>(msgJSON);


    //         if(messageList.Count >= chatMax){
    //             // Destroy(messageList[0].painelTexto.gameObject);
    //             // messageList.Remove(messageList[0]);
    //         }

    //         msgCHAT_moderator textoChat = new msgCHAT_moderator();
    //         if(message.moderator)
    //             textoChat.texto = message.user.name + ": " + message.texto;//falou;
    //         else
    //             textoChat.texto = "Equipe " + message.teamId + " / " + message.user.name + ": " + message.texto;   

    //         GameObject novoChat = Instantiate(painelTexto, painelChat.transform);

    //         textoChat.painelTexto = novoChat.GetComponent<Text>();

    //         textoChat.painelTexto.text = textoChat.texto;

    //         if(message.moderator){
    //             ColorUtility.TryParseHtmlString("#f41004", out cor);
    //             textoChat.painelTexto.fontStyle = FontStyle.Bold;
    //         }
    //         else{
    //             //textoChat.painelTexto.fontStyle = FontStyle.Regular;
    //                 ColorUtility.TryParseHtmlString("#112A46", out cor);
    //         }

    //         textoChat.painelTexto.color = cor;
    //         messageList.Add(textoChat);

    //         Debug.Log(textoChat.texto);
    //     }
    
    public void MSG_CHAT(string msgJSON)
    {
        Color cor;
        msgCHAT_moderator message = JsonUtility.FromJson<msgCHAT_moderator>(msgJSON);

        if (!msgTeams.ContainsKey(message.teamId))
        {
            msgTeams[message.teamId] = new List<msgCHAT_moderator>();
        }

        msgCHAT_moderator textoChat = new msgCHAT_moderator();
        if (message.moderator)
        {
            textoChat.texto = "MODERADOR - " + message.user.name + ": " + message.texto; // Se é moderador
        }
        else
        {
            textoChat.texto = "Equipe " + message.teamId + " / " + message.user.name + ": " + message.texto; // Se não é moderador
        }
        GameObject novoChat = Instantiate(painelTexto, painelChat.transform);
        textoChat.painelTexto = novoChat.GetComponent<Text>();
        textoChat.painelTexto.text = textoChat.texto;        
        if(scrollRect_prof.normalizedPosition.y < 0.0001f){
            scrollRect_prof.velocity = new Vector2 (0f, 1000f);
        }

        textoChat.painelTexto.gameObject.SetActive(false);
        if (Manager.teamId == message.teamId) {
            textoChat.painelTexto.gameObject.SetActive(true);
        }

        if (chatAberto != message.teamId) 
            notification[message.teamId].gameObject.SetActive(true);
     
        if (message.moderator)
        {
            ColorUtility.TryParseHtmlString("#f41004", out cor);
            textoChat.painelTexto.fontStyle = FontStyle.Bold;
        }
        else
        {
            ColorUtility.TryParseHtmlString("#112A46", out cor);
        }
        textoChat.painelTexto.color = cor;
 
        msgTeams[message.teamId].Add(textoChat);

        Debug.Log(textoChat.texto);
    }

    public void btnTeamChat(int teamId)
    {
        Manager.teamId = teamId;
        Debug.Log(Manager.teamId);
        chatAberto = teamId;
        notification[teamId].gameObject.SetActive(false);
        if (msgTeams.ContainsKey(teamId))
        {
            List<msgCHAT_moderator> mensagensDoTime = msgTeams[teamId];
            
            exibir(mensagensDoTime);
        }
        else
        {
            Debug.Log("Não há mensagens no time " + teamId);
        }
    }


    private void exibir(List<msgCHAT_moderator> mensagens)
    {
        foreach (Transform child in painelChat.transform)
        {
            Destroy(child.gameObject);
        }

        foreach (msgCHAT_moderator mensagem in mensagens)
        {
            GameObject novoChat = Instantiate(painelTexto, painelChat.transform);
            Text texto = novoChat.GetComponentInChildren<Text>();
            texto.text = mensagem.texto;
            texto.color = mensagem.moderator ? Color.red : Color.black;
        }
    }

    public void encerrar(){
        var msg = new EncerrarJogo("ENCERRAR_JOGO", Manager.sessionId, Manager.gameId);

        cm.send(msg);

        painelEncerrar.SetActive(true);

    }

    void MSG_DUVIDA(string msgJSON)
    {
        msgDuvida msg_duvida = JsonUtility.FromJson<msgDuvida>(msgJSON);
        int id = msg_duvida.teamId;

        for (int i = 0; i < quadrosEquipe.Count; i++)
        {
            GameObject equipe = quadrosEquipe[i];

            if (id ==  equipe.GetComponent<id_equipejogo>().id_equipe)
            {
                Debug.Log("entrou");
                GameObject imageObject = equipe.transform.Find("btnDuvida").gameObject;
                imageObject.SetActive(true);
            }
        }
    }

    public void removeExcl()
    {
        GameObject imageObject = prefab_equipeJogo.transform.Find("btnDuvida").gameObject;
        imageObject.SetActive(false);
    }


    public void MSG_QUESTAO(string msgJSON)
    {
        msgQuestao_equipe message = JsonUtility.FromJson<msgQuestao_equipe>(msgJSON);

        int id = message.teamId;

        for (int i = 0; i < quadrosEquipe.Count; i++)
        {
            GameObject equipe = quadrosEquipe[i];

            if (id == equipe.GetComponent<id_equipejogo>().id_equipe)
            {   
                Transform txt_qst = equipe.transform.Find("txt_qst_respondidas");
                TMP_Text tmpText_qst = txt_qst.GetComponent<TMP_Text>();
                equipe.GetComponent<id_equipejogo>().qst += 1;
                tmpText_qst.text = equipe.GetComponent<id_equipejogo>().qst+"/"+questionAmount;
                
                break;
            }
        }
    }

    public void MSG_CLASSF_FINAL(string msgJSON)
    {
        gera_arquivo(msgJSON);
        equipes_terminadas += 1;
        if (equipes_terminadas == Manager.nrTeam)
        {
            Debug.Log("Todas as equipes terminaram de jogar.");
            SceneManager.LoadScene("profFim");    

        }
    }
    
    void gera_arquivo(string json)
    {
             
        string executablePath = Application.dataPath;
        string directoryPath = Directory.GetParent(executablePath).FullName;
        string filePath = Path.Combine(directoryPath, "resultados.txt");

        // Escreve o JSON no arquivo
        try
        {
            File.WriteAllText(filePath, json);
            Debug.Log("JSON salvo com sucesso em: " + filePath);
        }
        catch (System.Exception e)
        {
            Debug.LogError("Erro ao salvar JSON no arquivo: " + e.Message);
        }
    }

    public void handle(string ms){
        //string messageType = ms.messageType;
        Debug.Log(ms);

        //executa JSON->messageType dentro do handle
        string messageType = JsonUtility.FromJson<ServerMessage>(ms).messageType;
        Debug.Log(messageType);


        // route message to handler based on message type

        if (messageType == "MENSAGEM_CHAT")
        {
            MSG_CHAT(ms);
        }
        else if(messageType == "DUVIDA")
        {
            MSG_DUVIDA(ms);
        }
        else if(messageType == "FINAL_QUESTAO")
        {
            MSG_QUESTAO(ms);
        }
        else if(messageType == "CLASSIFICACAO_FINAL_MODERADOR")
        {
            MSG_CLASSF_FINAL(ms);
        }

    }
    
    // // Start is called before the first frame update
    void Start()
    {
        // Manager.moderator.id = 1;
        Manager.teamId = 0;

        chat_moderator = GameObject.FindGameObjectWithTag("chat_moderator");

        m_Itens = Manager.nrTeam;
        for (int i = 0; i < 6; i++)
        {
            GameObject novaEquipe = Instantiate(prefabEquipes, transform.position, Quaternion.identity);
            novaEquipe.transform.SetParent(ContentEquipes, false);
            novaEquipe.transform.localScale = new Vector3(1.894364f, 0.179433f, 0.23102f);
            notification.Add(novaEquipe.transform.Find("notification").gameObject);
           // notification[i].SetActive(true);
            quadrosEquipe.Add(novaEquipe);

            int teamId = i+1;
            Button btn = novaEquipe.GetComponentInChildren<Button>();
            btn.onClick.AddListener(() => btnTeamChat(teamId));
        }

        for (int i = 0; i < quadrosEquipe.Count; i++)
        {
            GameObject equipe = quadrosEquipe[i];
            TextMeshProUGUI[] textFields = equipe.GetComponentsInChildren<TextMeshProUGUI>();

            foreach (TextMeshProUGUI textField in textFields)
            {
                if (textField.CompareTag("txt_equipe_jogo"))
                {
                    if (dadosTimes.listaTimes.Count > i)
                    {
                        textField.text = "Equipe " + dadosTimes.listaTimes[i].id;
                        equipe.GetComponent<id_equipejogo>().id_equipe = dadosTimes.listaTimes[i].id;
                        Transform txt_qst = equipe.transform.Find("txt_qst_respondidas");
                        TMP_Text tmpText_qst = txt_qst.GetComponent<TMP_Text>();
                        questionAmount = Manager.nrEasy + Manager.nrMedium + Manager.nrHard;
                        tmpText_qst.text = "0/"+questionAmount;
                    }
                    break;
                }
            }
        }
           scrollRect_prof.GetComponent<ScrollRect> ();
        
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
public class msgQuestao_equipe
{
    public string message_type;
    public int teamId;
    public string sessionId;
    public int gameId;
    public string finalAnswer;
    public int correct;
    public int interaction;

}

[System.Serializable]
public class msgCHAT_moderator
{
    public string message_type;
    public string texto;

    public Text painelTexto;
    public User user;

    public int teamId;
    public string sessionId;
    public int gameId;
    public bool moderator;

}

[System.Serializable]
public class msgDuvida
{
    public string message_type;
    public int teamId;
    public string sessionId;
    public int gameId;
}