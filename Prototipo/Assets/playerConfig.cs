using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using NativeWebSocket;
using TMPro;
using UnityEngine.EventSystems;


public class playerConfig : MonoBehaviour, IClient
{
    private ConnectionManager cm;

    private string namePlayer;
    private string secret;
    public string reason;
    public User user;

    public TMP_Text txt_erro;
    public TMP_Text txt_aguarde;
    public TMP_InputField[] inputFields;
    public int InputSelected;


    //public InputField inputName;

    public Button btnEntrar;

    public Button btnConfig;
    private int interact = 1;

    public GameObject painelConexao;

    public GameObject FundoPainel;
    
    public void readName(string name){
        namePlayer = name;
        user.name = name;
        Debug.Log(namePlayer);
    }

    public void readTeam(string team){
        secret = team;
        Debug.Log(secret);
    }

//Envio da mensagem para servidor quando clicar no botao
    public void btnEntrarJogo(){

        var msg = new EntrarSessao("ENTRAR_SESSAO", this.user, this.secret);

        txt_erro.enabled = false;

        txt_aguarde.enabled = true;

        txt_aguarde.text = "Aguarde a autorização de sua entrada no grupo.";

        cm.send(msg);
        btnEntrar.interactable = false;
        interact = 0;
    }
    

     public void handle(string ms) {

        string messageType = JsonUtility.FromJson<ServerMessage>(ms).messageType;
        Debug.Log(messageType);

        if (messageType == "ACESSO_INVALIDO") 
        {
            interact = 1;
            btnEntrar.interactable = true;
            MSG_ACESSO(ms);
        }
        
        if (messageType == "ENTROU_SESSAO") 
        {
            MSG_ENTROU_SESSAO(ms);
        }

        // route message to handler based on message type
        Debug.Log(ms);
     }

    public void MSG_ENTROU_SESSAO(string msgJSON) 
    {
        msgENTROU_SESSAO_ALUNO message = JsonUtility.FromJson<msgENTROU_SESSAO_ALUNO>(msgJSON);

        Debug.Log(message.teamId);
        
        dadosTimes.player = message.user;
        Manager.teamId = message.teamId;

        Debug.Log(dadosTimes.player.name);
        Debug.Log(dadosTimes.player.id);

        SceneManager.LoadScene("alunoEspera");

    }


    public void MSG_ACESSO(string msgJSON){

        msgACESSO message = JsonUtility.FromJson<msgACESSO>(msgJSON);

    
        reason = message.reason;

        if(reason == "WRONG_PASSWORD")
        {
            
            txt_aguarde.enabled = false;

            txt_erro.enabled = true;

            txt_erro.text = "Senha incorreta. Tente novamente.";

            Invoke("DesativaERRO", 5f);
            
        }
        else if(reason == "EXCEEDED_MAXIMUM_NUMBER_PARTICIPANTS")
        {
        
            txt_aguarde.enabled = false;

            txt_erro.enabled = true;

            txt_erro.text = "A equipe atingiu o número máximo de participantes.\nTente novamente.";

            Invoke("DesativaERRO", 5f);


        }

    }

    public void connectionFail() {
        SceneManager.LoadScene("alunoEntrar");
    }

    void DesativaERRO()
    {
        txt_erro.enabled = false;
    }


    // Start is called before the first frame update
    void Start()
    {
        interact = 1;
        btnEntrar.interactable = true;
        // txt_erro.SetActive(false);
        txt_erro.enabled = false;
        txt_aguarde.enabled = false;


        carregaDados.Load();
        
        cm = ConnectionManager.getInstance();     
        cm.connect();
        Invoke("checkConnection", 2);   
        btnConfig.interactable = false;
    }

    void checkConnection() {
        if(!cm.isConnected()) {   
          //  cm.connect();
           // Invoke("checkConnection", 2.0f);
            painelConexao.gameObject.SetActive(true);
            FundoPainel.SetActive(true);
        }
        else{
            painelConexao.gameObject.SetActive(false);
            btnConfig.interactable = true;
        }    
    }


    // Update is called once per frame
    void Update()
    {
    
        cm.retrieveMessages(this);
        if (interact == 1)
        {
                bool allInputs = true;

                if (inputFields != null)
                {
                    foreach (TMP_InputField inputField in inputFields)
                    {
                        if (inputField != null && string.IsNullOrEmpty(inputField.text))
                        {
                            allInputs = false;
                            break;
                        }
                    }
                }
            

                if (btnEntrar != null)
                {
                    btnEntrar.interactable = allInputs;
                }
        }

        if (Input.GetKeyDown(KeyCode.Tab))
        {
            Selectable currentInput = EventSystem.current.currentSelectedGameObject.GetComponent<Selectable>();

            int currentIndex = System.Array.IndexOf(inputFields, currentInput);

            if (currentIndex >= 0)
            {
                int nextIndex = (currentIndex + 1) % inputFields.Length;
                Selectable nextInput = inputFields[nextIndex];
                nextInput.Select();
            }
            else
            {
                // Se nenhum campo de entrada estiver selecionado, selecione o primeiro.
                inputFields[0].Select();
            }
        }


    }

}

[System.Serializable] 
public class msgENTROU_SESSAO_ALUNO
{
    public string messageType;
    public User user;
    public int teamId;
    public int sessionId;
    public int gameId;
}

[System.Serializable] 
public class msgACESSO
{
    public string messageType;
    public string reason;
}