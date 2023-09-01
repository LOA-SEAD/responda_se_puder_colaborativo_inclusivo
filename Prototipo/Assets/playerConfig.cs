using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using NativeWebSocket;
using TMPro;


public class playerConfig : MonoBehaviour, IClient
{
    private ConnectionManager cm;

    private string namePlayer;
    private string secret;
    public string reason;
    public User user;

    public TMP_Text txt_erro;


    //public InputField inputName;

    public Button btnEntrar;
    
    public void readName(string name){
        //this.playerName = inputName.GetComponent<Text>().text;
        namePlayer = name;
        user.name = name;
        // dadosTimes.player.name = name;
        Debug.Log(namePlayer);
    }

    public void readTeam(string team){
        secret = team;
        Debug.Log(secret);
    }

//Envio da mensagem para servidor quando clicar no botao
    public void btnEntrarJogo(string sceneName){

        //PASSAR sessionID@secret
 

        var msg = new EntrarSessao("ENTRAR_SESSAO", this.user, this.secret);

 

        cm.send(msg);
        SceneManager.LoadScene(sceneName);
    }
    

     public void handle(string ms) {
        //string messageType = ms.messageType;
        string messageType = JsonUtility.FromJson<ServerMessage>(ms).messageType;
        Debug.Log(messageType);
        //executa JSON->messageType dentro do handle
        //string messageType = JsonUtility.FromJson<ServerMessage>(messageJSON).messageType;
        if (messageType == "ACESSO_INVALIDO") 
        {
            MSG_ACESSO(ms);
        }

        // route message to handler based on message type
        Debug.Log(ms);
     }

    public void MSG_ACESSO(string msgJSON){

        msgACESSO message = JsonUtility.FromJson<msgACESSO>(msgJSON);

    
        reason = message.reason;

        if(reason == "WRONG_PASSWORD")
        {
            
            txt_erro.enabled = true;

            txt_erro.text = "Senha incorreta. Tente novamente.";

            Invoke("DesativaERRO", 5f);
            
        }
        else if(reason == "EXCEEDED_MAXIMUM_NUMBER_PARTICIPANTS")
        {
            
            txt_erro.enabled = true;

            txt_erro.text = "A equipe atingiu o número máximo de participantes.\nTente novamente.";

            Invoke("DesativaERRO", 5f);


        }


    }

    void DesativaERRO()
    {
        txt_erro.enabled = false;
    }


    // Start is called before the first frame update
    void Start()
    {

        btnEntrar.interactable = true;
        // txt_erro.SetActive(false);
        txt_erro.enabled = false;

        carregaDados.Load();
        cm = ConnectionManager.getInstance();        
    }

    // Update is called once per frame
    void Update()
    {
    
        //cm.retrieveMessages(this);
        //retriveMessages aqui dentro ->
        /*
        if ((!string.IsNullOrEmpty(playerName)) && (!string.IsNullOrEmpty(teamId)))
        {
            //btnEntrar = GameObject.GetComponent<Button>();
            btnEntrar.interactable = true;
        }*/


    }
}


[System.Serializable] 
public class msgACESSO
{
    public string messageType;
    public string reason;
}