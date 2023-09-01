using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using NativeWebSocket;
using TMPro;

public class alunoEspera : MonoBehaviour, IClient
{

    private ConnectionManager cm = ConnectionManager.getInstance();

    public TMP_Text textoIniciar; 
    public float timer = 100f;

    public void MSG_INICIA_JOGO(string msgJSON) 
    {
        
        msgINICIA_JOGO message = JsonUtility.FromJson<msgINICIA_JOGO>(msgJSON);

        Manager.sessionId = message.sessionId;
        Manager.gameId = message.gameId;
        Manager.leaderId = message.leaderId;
        Manager.time = message.timeQuestion;
        Manager.qEasy = message.question.easy;
        Manager.qMedium = message.question.medium;
        Manager.qHard = message.question.hard;
        
        dadosTimes.meuTime = message.team;


        timer = 10f;

        // while (timer > 0f)
        // {
        //     atualizaTimer();
        // }

        SceneManager.LoadScene("Jogo");
    }
    
    public void atualizaTimer()
    {
        timer -= Time.deltaTime;

        int min = Mathf.FloorToInt(timer / 60f);
        int sec = Mathf.FloorToInt(timer % 60f);


        string timeString = string.Format("{0:00}:{1:00}", min, sec);

        textoIniciar.text = "O jogo iniciará em " + timeString + " segundos...";
    }

    public void MSG_ENTROU_SESSAO(string msgJSON) 
    {
        msgENTROU_SESSAO_ALUNO message = JsonUtility.FromJson<msgENTROU_SESSAO_ALUNO>(msgJSON);

        Debug.Log(message.teamId);
        
        // dadosTimes.player.name = message.user.name;
        // dadosTimes.player.id = message.user.id;
        dadosTimes.player = message.user;
        Manager.teamId = message.teamId;

        Debug.Log(dadosTimes.player.name);
        Debug.Log(dadosTimes.player.id);

    }

    public void handle(string ms){
        //string messageType = ms.messageType;

        //executa JSON->messageType dentro do handle
        string messageType = JsonUtility.FromJson<ServerMessage>(ms).messageType;


        // route message to handler based on message type
        Debug.Log("CENA ALUNOESPERA: " + ms);

        if (messageType == "INICIA_JOGO") 
        {
            MSG_INICIA_JOGO(ms);
        }
        if (messageType == "ENTROU_SESSAO") 
        {
            MSG_ENTROU_SESSAO(ms);
        }
        if (messageType == "NOVA_QUESTAO"){
            Manager.msgPrimeiraQuestao = ms;
        }

    }
    // Start is called before the first frame update
    void Start()
    {

        
    }

    // Update is called once per frame
    void Update()
    {
        cm.retrieveMessages(this);

        
        
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
public class msgINICIA_JOGO
{
    public string messageType;
    public int[] totalQuestion;
    public Question question;
    public List<User> team;
    public int timeQuestion;
    public int leaderId;
    public int sessionId;
    public int gameId;
}

[System.Serializable]
public class Question
{
    public int[] easy;
    public int[] medium;
    public int[] hard;
}