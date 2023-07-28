using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using NativeWebSocket;

public class playerConfig : MonoBehaviour, IClient
{
    private ConnectionManager cm = ConnectionManager.getInstance();

    private string namePlayer;
    private string secret;
    //public InputField inputName;

    public Button btnEntrar;
    
    public void readName(string name){
        //this.playerName = inputName.GetComponent<Text>().text;
        namePlayer = name;
        Debug.Log(namePlayer);
    }

    public void readTeam(string team){
        secret = team;
        Debug.Log(secret);
    }

//Envio da mensagem para servidor quando clicar no botao
    public void btnEntrarJogo(string sceneName){


        //PASSAR sessionID@secret
        var msg = new EntrarSessao("ENTRAR_SESSAO", this.namePlayer, "23@"+this.secret); 

        //var msg = new EntrarSessao("ENTRAR_SESSAO", this.namePlayer, this.secret); //delano bruna necessidade da session em tela ou exite forma do aluno receber ela anteriormente???

        //var msg = new EntrarSessao("ENTRAR_SESSAO", this.name, sessionId+"@"+this.secret);
        //msg.messageType = "ENTRAR SESSAO";

        cm.send(msg);
        SceneManager.LoadScene(sceneName);
    }
    

     public void handle(string ms) {
        //string messageType = ms.messageType;

        //executa JSON->messageType dentro do handle
        //string messageType = JsonUtility.FromJson<ServerMessage>(messageJSON).messageType;


        // route message to handler based on message type
        Debug.Log(ms);
     }

    // Start is called before the first frame update
    void Start()
    {

        btnEntrar.interactable = true;
        
    }

    // Update is called once per frame
    void Update()
    {
    
        cm.retrieveMessages(this);
        //retriveMessages aqui dentro ->
        /*
        if ((!string.IsNullOrEmpty(playerName)) && (!string.IsNullOrEmpty(teamId)))
        {
            //btnEntrar = GameObject.GetComponent<Button>();
            btnEntrar.interactable = true;
        }*/


    }
}
