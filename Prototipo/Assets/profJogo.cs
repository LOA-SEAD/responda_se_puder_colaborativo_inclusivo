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

     [SerializeField]
    public List<int> qualPergunta = new List<int>();

    public TMP_Text equipeSelecionada;

    // //BTN
    public Button encerrarJogo;

    public GameObject quadroChat;
    public GameObject painelTexto;
    public GameObject painelChat;
    public GameObject prefab_equipeJogo;
    public GameObject painelEncerrar;
    [SerializeField]
    public List<GameObject> painelQuestoesTime = new List<GameObject>();
    public TMP_InputField chatBox;
    public TextMeshProUGUI messageText;
    // public int teamID;
    GameObject chat_moderator;
    public ScrollRect scrollRect_prof;
    public int chatAberto = 0;
    private int totalQuestoes;
    public int[] alt;
    [SerializeField]
    public List<GameObject> questao = new List<GameObject>();
    [SerializeField]
    public List<GameObject> alternativa1 = new List<GameObject>();
    [SerializeField]
    public List<GameObject> alternativa2 = new List<GameObject>();
    [SerializeField]
    public List<GameObject> alternativa3 = new List<GameObject>();
    [SerializeField]
    public List<GameObject> alternativa4 = new List<GameObject>();
    
    [SerializeField]
    public List<GameObject> notification = new List<GameObject>();
    [SerializeField]
    public List<Outline> outlineComponent = new List<Outline>();
    

    // //Quadros em tela
    [SerializeField] private Transform ContentEquipes;
    [SerializeField] private Transform ContentQuestoes;
    [SerializeField] private GameObject prefabEquipes;
    [SerializeField] private GameObject prefabQuestoesTime;
    [SerializeField] private int m_Itens;

    private List<GameObject> quadrosEquipe = new List<GameObject>();
    
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
        if(chatAberto != 0){
            painelQuestoesTime[chatAberto].gameObject.SetActive(false);
            outlineComponent[chatAberto-1].effectColor = Color.black; 
        }
        Manager.teamId = teamId;
        Debug.Log(Manager.teamId);
        chatAberto = teamId;
        notification[teamId].gameObject.SetActive(false);
        outlineComponent[teamId-1].effectColor = Color.red; 
        painelQuestoesTime[teamId].gameObject.SetActive(true);
        equipeSelecionada.text = "Equipe " + teamId;
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

    void CarregarPergunta(int[] alter,  int qualTime, bool pulou_na_fase)
    {  
        StartCoroutine(waiter());
        Questao perguntaAtual;
        if((qualPergunta[qualTime] == (Manager.nQ_easy) || qualPergunta[qualTime] == (Manager.nQ_easy + Manager.nQ_medium + 1))&& (!pulou_na_fase)){
            qualPergunta[qualTime]++;
        }
        
        if (carregaDados.listaDados.Count > 0)
        {
            Debug.Log("qualPergunta[qualTime] = " + qualPergunta[qualTime]);
            perguntaAtual = new Questao(qualTime, carregaDados.listaDados[qualPergunta[qualTime]]);
            qualPergunta[qualTime]++;
        }
        else
        {
            Debug.Log("(" + qualTime + ") Entrou no if perguntaAtual = null");
            perguntaAtual = null;
        }

        TMP_Text questaoTexto = questao[qualTime].GetComponent<TMP_Text>();
        TMP_Text alternativa1Texto = alternativa1[qualTime].GetComponent<TMP_Text>();
        TMP_Text alternativa2Texto = alternativa2[qualTime].GetComponent<TMP_Text>();
        TMP_Text alternativa3Texto = alternativa3[qualTime].GetComponent<TMP_Text>();
        TMP_Text alternativa4Texto = alternativa4[qualTime].GetComponent<TMP_Text>();
            
        if (perguntaAtual != null)
        {
            questaoTexto.text = perguntaAtual.pergunta;
      
            Debug.Log(alter[0] + "," + alter[1]  + ","  + alter[2] + ","  + alter[3] + " time " + qualTime);
            Debug.Log(perguntaAtual);
            perguntaAtual.Shuffle(alter);
            Debug.Log(perguntaAtual);

            alternativa1Texto.text = "A. " + ObterAlternativa( perguntaAtual, 0);
            alternativa2Texto.text = "B. " + ObterAlternativa( perguntaAtual, 1);
            alternativa3Texto.text = "C. " + ObterAlternativa( perguntaAtual, 2);
            alternativa4Texto.text = "D. " + ObterAlternativa( perguntaAtual, 3);
    
        }

        
    }

    string ObterAlternativa(Questao dados, int index)
    {
        switch (index)
        {
            case 0:
                return dados.resposta;
            case 1:
                return dados.r2;
            case 2:
                return dados.r3;
            case 3:
                return dados.r4;
            default:
                return "";
        }
    }

     void PrimeiraQuestao()
    {
        MSG_NOVA_QUESTAO(Manager.msgPrimeiraQuestao);
    }

    IEnumerator waiter()
{

    //Wait for 2 seconds
    yield return new WaitForSeconds(2);

  
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

    public void MSG_NOVA_QUESTAO(string msgJSON) 
    {
        msgNOVA_QUESTAO_MODERADOR message = JsonUtility.FromJson<msgNOVA_QUESTAO_MODERADOR>(msgJSON);
        
        Manager.leaderId = message.leaderId;
        alt = message.alternativas;
        Manager.teamId = message.teamId;
        CarregarPergunta(alt, message.teamId - 1, message.pulou_na_fase);
       /* if (qst_respondidas != Manager.nQ_easy && qst_respondidas != Manager.nQ_easy + Manager.nQ_medium && qst_respondidas != Manager.nQ_easy + Manager.nQ_medium + Manager.nQ_hard)
        {
            ProximaQuestao();
        }
*/
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

    /*public void MSG_NOVA_FASE(string msgJSON)
    {

        msgPROX_FASE_prof message = JsonUtility.FromJson<msgPROX_FASE_prof>(msgJSON);
        Debug.Log("time " + message.teamId + "entrou proxima fase");
        Debug.Log("pergunta " + qualPergunta[message.teamId-1]); 
        Debug.Log(Manager.nQ_easy);
        Debug.Log(Manager.nQ_medium);
        if((qualPergunta[message.teamId-1] == (Manager.nQ_easy)+1 || qualPergunta[message.teamId-1] == (Manager.nQ_easy + Manager.nQ_medium + 1))){
            qualPergunta[message.teamId-1]++;
        }

    }*/
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
        else if (messageType == "NOVA_QUESTAO") 
        {
            MSG_NOVA_QUESTAO(ms);
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
      /*   else if (messageType == "INICIA_NOVA_FASE"){
            MSG_NOVA_FASE(ms);
        } */
    }
    
    // // Start is called before the first frame update
    void Start()
    {
        carregaDados.Load();
        carregaDados.Select();
        
        // Manager.moderator.id = 1;
        Manager.teamId = 0;

        chat_moderator = GameObject.FindGameObjectWithTag("chat_moderator");

        m_Itens = Manager.nrTeam;
        for (int i = 0; i < m_Itens; i++)
        {
            //adicionando botão de cada time na tela do professor
            GameObject novaEquipe = Instantiate(prefabEquipes, transform.position, Quaternion.identity);
            novaEquipe.transform.SetParent(ContentEquipes, false);
            novaEquipe.transform.localScale = new Vector3(1.894364f, 0.179433f, 0.23102f);
            notification.Add(novaEquipe.transform.Find("notification").gameObject);
            quadrosEquipe.Add(novaEquipe);
            outlineComponent.Add(novaEquipe.GetComponent<Outline>());
            int teamId = i+1;
            Button btn = novaEquipe.GetComponentInChildren<Button>();
            btn.onClick.AddListener(() => btnTeamChat(teamId));
            //Adicionando perguntas para cada time
            GameObject questoesTime = Instantiate(prefabQuestoesTime, transform.position, Quaternion.identity);
            questoesTime.transform.SetParent(ContentQuestoes, false);
            questao.Add(questoesTime.transform.Find("questao").gameObject);
            alternativa1.Add(questoesTime.transform.Find("alternativa1").gameObject);
            alternativa2.Add(questoesTime.transform.Find("alternativa2").gameObject);
            alternativa3.Add(questoesTime.transform.Find("alternativa3").gameObject);
            alternativa4.Add(questoesTime.transform.Find("alternativa4").gameObject);
            questoesTime.gameObject.SetActive(false);
            painelQuestoesTime.Add(questoesTime);

            qualPergunta.Add(0);
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

        Manager.totalQuestoes = 0;
        Manager.totalFacil = 0;
        Manager.totalMedio = 0;
        Manager.totalDificil = 0;
        Manager.countQuestoesJogo();
        //carregaDados.Load();
        //carregaDados.Select();
        totalQuestoes = carregaDados.listaDados.Count;

        PrimeiraQuestao();
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

/*[System.Serializable]
public class msgPROX_FASE_prof
{
    public string message_type;
    public int teamId;
    public int leaderId;
    public int sessionId;
    public int gameId;
}*/

[System.Serializable]
public class QuestionProf
{
    public int[] easy;
    public int[] medium;
    public int[] hard;
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
public class msgNOVA_QUESTAO_MODERADOR
{
    public string message_type;
    public int[] alternativas;
    public int teamId;
    public int leaderId;
    public int sessionId;
    public int gameId;
    public bool pulou_na_fase;
    public bool entrou_nova_fase;
}


[System.Serializable]
public class msgDuvida
{
    public string message_type;
    public int teamId;
    public string sessionId;
    public int gameId;
}