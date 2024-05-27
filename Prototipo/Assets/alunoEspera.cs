using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using NativeWebSocket;
using TMPro;
using System;

public class alunoEspera : MonoBehaviour, IClient
{

    private ConnectionManager cm = ConnectionManager.getInstance();

    public TMP_Text textoIniciar; 

    public void MSG_INICIA_JOGO(string msgJSON) 
    {
        
        msgINICIA_JOGO message = JsonUtility.FromJson<msgINICIA_JOGO>(msgJSON);

        Manager.sessionId = message.sessionId;
        Manager.gameId = message.gameId;
        Manager.leaderId = message.leaderId;
        Manager.time = message.timeQuestion;
        Manager.nrHelp5050 = message.help5050;
        Manager.qEasy = message.question.easy;
        Manager.qMedium = message.question.medium;
        Manager.qHard = message.question.hard;
        
        dadosTimes.meuTime = message.team;

        Manager.nrPlayerTeam = dadosTimes.meuTime.Count - 1;

        // Debug.Log("Nr jogadores do meu time: " + Manager.nrPlayerTeam);

        SceneManager.LoadScene("Jogo");
    }


    public void handle(string ms)
    {
        //string messageType = ms.messageType;

        //executa JSON->messageType dentro do handle
        string messageType = JsonUtility.FromJson<ServerMessage>(ms).messageType;



        if (messageType == "INICIA_JOGO") 
        {
            MSG_INICIA_JOGO(ms);
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
public class msgINICIA_JOGO
{
    public string messageType;
    public int[] totalQuestion;
    public Question question;
    public List<User> team;
    public int timeQuestion;
    public int help5050;
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