using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using NativeWebSocket;
using TMPro;
using UnityEngine.EventSystems;


public class profConfig : MonoBehaviour, IClient
{

    private ConnectionManager cm; // = ConnectionManager.getInstance();

    //Declaração variáveis
    private int nrTeam;
    private int nrPlayerTeam;
    private int nrHelp5050;
    private int time;
    private int nrEasy;
    private int nrMedium;
    private int nrHard;
    private string moderatorName;

    //Declaração texto em tela
    public TMP_Text txt_MaxFacil;
    public TMP_Text txt_MaxMedio;
    public TMP_Text txt_MaxDificil;
    public TMP_Text txt_erro_numero_qst;



    //Declaração de botões
    public TMP_InputField[] inputFields;
    public Button btnCadastrar;

    //Declaração do user do moderador
    public User mod;
    public GameObject painelConexao;


    

    //Leitura dos inputs
    public void readName(string name){
        moderatorName = name;
        mod.name = name;
        Manager.moderador_user.name = name;
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

        Manager.moderator = mod;
        
        int[] vetorQuestions = new int[3] {Manager.totalFacil, Manager.totalMedio, Manager.totalDificil};
        int[] questionAmount = new int[3] {this.nrEasy, this.nrMedium, this.nrHard};

         if (questionAmount[0]>(Manager.totalFacil-1))
        {
            txt_erro_numero_qst.text = "O número de questões 'FÁCIL' escolhido está acima do possível.";
            txt_erro_numero_qst.gameObject.SetActive(true);
            Invoke("desativaTXT", 5f);

         }
        else if (questionAmount[1]>(Manager.totalMedio-1))
        {
            txt_erro_numero_qst.text = "O número de questões 'MÉDIO' escolhido está acima do possível.";
            txt_erro_numero_qst.gameObject.SetActive(true);
            Invoke("desativaTXT", 5f);
        }
        else if (questionAmount[2]>(Manager.totalDificil-1))
        {
            txt_erro_numero_qst.text = "O número de questões 'DIFÍCIL' escolhido está acima do possível.";
            txt_erro_numero_qst.gameObject.SetActive(true);
            Invoke("desativaTXT", 5f);
        }
        else {
            var msg = new CadastraSessao("CADASTRAR_SESSAO", this.nrTeam, this.nrPlayerTeam, this.nrHelp5050, this.time,
                                            vetorQuestions, questionAmount, this.mod, Manager.gameId);

            cm.send(msg);

        SceneManager.LoadScene("profEspera");
        }    
    }

    public void desativaTXT()
    {
            txt_erro_numero_qst.gameObject.SetActive(false);
    }

    public void handle(string ms){
        Debug.Log(ms);
    }

    public void connectionFail() {
        SceneManager.LoadScene("profConfig");
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


        //Apresenta como máximo uma questão a mesma dado que precisa de uma questão extra para PULAR
        txt_MaxFacil.text = "(Máx: " + (Manager.totalFacil-1) + ")";
        txt_MaxMedio.text = "(Máx: " + (Manager.totalMedio-1) + ")";
        txt_MaxDificil.text = "(Máx: " + (Manager.totalDificil-1) + ")";


        cm = ConnectionManager.getInstance();
      /*  if(!cm.isConnected()) {   
            cm.connect();
            Invoke("checkConnection", 2.0f);
            painelConexao.gameObject.SetActive(true);
        }*/
        cm.connect();
        Invoke("checkConnection", 2);
    }

    void checkConnection() {
        if(!cm.isConnected()) {   
          //  cm.connect();
           // Invoke("checkConnection", 2.0f);
            painelConexao.gameObject.SetActive(true);
        }
        else
            painelConexao.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
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
    

        if (btnCadastrar != null)
        {
            btnCadastrar.interactable = allInputs;
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
