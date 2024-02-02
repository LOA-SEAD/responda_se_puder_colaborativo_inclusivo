using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using NativeWebSocket;
using TMPro;



public class profJogo : MonoBehaviour, IClient
{

    private ConnectionManager cm = ConnectionManager.getInstance();

    // //BTN
    public Button encerrarJogo;

    // public GameObject quadroChat;
    // public GameObject painelTexto;
    // public GameObject painelChat;

    // public InputField chatBox;

    // [SerializeField]
    // public List<msgCHAT> messageList = new List<msgCHAT>();

    // public int chatMax = 25;


    // //Quadros em tela
    [SerializeField] private Transform ContentEquipes;
    [SerializeField] private GameObject prefabEquipes;
    [SerializeField] private int m_Itens;

    private List<GameObject> quadrosEquipe = new List<GameObject>();

    // public Vector3 escalaDesejada;

    public GameObject painelChat;

    public void AtivaDesativaChat()
    {
        painelChat.SetActive(!painelChat.activeSelf);
    }


    // public void btnComecarJogo(){

    //     var msg = new ComecarJogo("COMECAR_JOGO", Manager.sessionId, Manager.gameId);

    //     cm.send(msg);

    //     SceneManager.LoadScene("profJogo");

    // }

    // public void MSG_CHAT(string msgJSON){
    //         Color cor;
    //         msgCHAT message = JsonUtility.FromJson<msgCHAT>(msgJSON);

    //         if(messageList.Count >= chatMax){
    //             Destroy(messageList[0].painelTexto.gameObject);
    //             messageList.Remove(messageList[0]);
    //         }

    //         msgCHAT textoChat = new msgCHAT();
    //         if(message.moderator)
    //             textoChat.texto = message.user.name + ":" + message.texto;//falou;
    //         else
    //             textoChat.texto = "Equipe " + message.teamId + " / " + message.user.name + ":" + message.texto;   

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

    public void MSG_CHAT(string msgJSON) {
        Debug.Log("Recebeu mensagem enviada no chat !!");
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

        // if (messageType == "SESSAO_CRIADA") 
        // {
        //     MSG_SESSAO_CRIADA(ms);
        // }
        // else if(messageType == "ENTROU_SESSAO")
        // {
        //     MSG_ENTROU_SESSAO(ms);
        // }
        // else if (messageType == "MENSAGEM_CHAT")
        // {
        //     MSG_CHAT(ms);
        // }

    }
    
    // // Start is called before the first frame update
    void Start()
    {
        m_Itens = Manager.nrTeam;
        for (int i = 0; i < m_Itens; i++)
        {

            GameObject novaEquipe = Instantiate(prefabEquipes, transform.position, Quaternion.identity);
            novaEquipe.transform.SetParent(ContentEquipes);
            novaEquipe.transform.localScale = new Vector3(1.894364f, 0.179433f, 0.23102f);
        
            quadrosEquipe.Add(novaEquipe);
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
                    }
                    break;
                }
            }
        }
        
    }

    // Update is called once per frame
    void Update()
    {
       
        cm.retrieveMessages(this);

        //  if(chatBox.text != "")
        // {
        //     if(Input.GetKeyDown(KeyCode.Return)){
               
        //         var msg = new mensagemChat("MENSAGEM_CHAT", Manager.moderator, Manager.teamId, Manager.sessionId, Manager.gameId, chatBox.text, true);
        //         //var msg = new mensagemChat("MENSAGEM_CHAT", dadosTimes.player, Manager.teamId, Manager.sessionId, Manager.gameId, chatBox.text, Manager.moderator);
        //         cm.send(msg);
        //        // readChat(chatBox.text);
        //         chatBox.text = "";
        //     }
        // }
        
    }
        
}
