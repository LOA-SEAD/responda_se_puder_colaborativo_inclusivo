using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using NativeWebSocket;

public class profConfig : MonoBehaviour, IClient
{

    private ConnectionManager cm = ConnectionManager.getInstance();

    //Declaração variáveis
    private int nrTeam;
    private int nrPlayerTeam;
    private int nrHelp5050;
    private int time;
    private int nrEasy;
    private int nrMedium;
    private int nrHard;
    private string moderatorName;


    //Declaração de botões
    public Button btnIniciarSessao;
    public InputField inputModerator;
    public Button btnCadastrar;

    public User mod;

    

    //Leitura dos inputs
    public void readName(string name){
        moderatorName = name;
        mod.name = name;
        Manager.moderatorName = this.moderatorName;

        Debug.Log(moderatorName);
    }

    public void readTeam(string n){
        int.TryParse(n, out nrTeam);
        Manager.nrTeam = this.nrTeam;
        
        Debug.Log(nrTeam);
    }

    public void readPlayers(string n) {
        int.TryParse(n, out nrPlayerTeam);
        Manager.nrPlayerTeam = this.nrPlayerTeam;
        
        Debug.Log(n);
    }

    public void read5050(string n){
        int.TryParse(n, out nrHelp5050);
        Manager.nrHelp5050 = this.nrHelp5050;

        Debug.Log(nrHelp5050);
    }

    public void readTime(string n){
        int.TryParse(n, out time);
        Manager.time = this.time;

        Debug.Log(time);
    }

    public void readEasy(string n) {
        int.TryParse(n, out nrEasy);
        Manager.nrEasy = this.nrEasy;

        Debug.Log(n);
    }

    public void readMedium(string n) {
        int.TryParse(n, out nrMedium);
        Manager.nrMedium = this.nrMedium;

        Debug.Log(n);
    }

    public void readHard(string n) {
        int.TryParse(n, out nrHard);
        Manager.nrHard = this.nrHard;

        Debug.Log(n);
    }


    // //message_type: CADASTRAR_SESSAO
    public void btnCadastraSessao(){

        int[] vetorQuestions = new int[3] {Manager.totalFacil, Manager.totalMedio, Manager.totalDificil};
        int[] questionAmount = new int[3] {this.nrEasy, this.nrMedium, this.nrHard};

        var msg = new CadastraSessao("CADASTRAR_SESSAO", this.nrTeam, this.nrPlayerTeam, this.nrHelp5050, this.time,
                                        vetorQuestions, questionAmount, this.mod, Manager.gameId);

        cm.send(msg);

        SceneManager.LoadScene("profEspera");
    }

    //Utilizado para tester com o server antigo
    // public void btnCadastraSessao(){

    //     var msg = new CadastraSessao("CADASTRAR_SESSAO", this.nrTeam, this.nrPlayerTeam,
    //                                 this.nrHelp5050, this.time,
    //                                 this.moderatorName);

    //     cm.send(msg);

    //     SceneManager.LoadScene("profEspera");
    // }


    public void handle(string ms){
        //string messageType = ms.messageType;

        //executa JSON->messageType dentro do handle
        // string messageType = JsonUtility.FromJson<ServerMessage>(ms).messageType;


        // route message to handler based on message type
        Debug.Log(ms);
    }
    
    // Start is called before the first frame update
    void Start()
    {
        
        /*
        Necessário para conseguir o número total de questões
        Possui o listaDados que será usado para apresentar as questões
        e o listaBase, usada para manutenção das questões
        */
        carregaDados.Load();
        // btnCadastrar.SetActive(false);


    }

    // Update is called once per frame
    void Update()
    {


        // bool isFieldEmpty = string.IsNullOrEmpty(inputModerator.text);

        //cm.retrieveMessages(this);

        
    }
        
}
