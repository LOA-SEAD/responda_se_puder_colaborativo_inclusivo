using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using NativeWebSocket;
using TMPro;

public class alunoEspera : MonoBehaviour, IClient
{

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

        while (timer > 0f)
        {
            timer -= Time.deltaTime;
            
            int min = Mathf.FloorToInt(timer / 60f);
            int sec = Mathf.FloorToInt(timer % 60f);


            string timeString = string.Format("{0:00}:{1:00}", min, sec);

            textoIniciar.text = "O jogo iniciará em " + timeString + " segundos...";
        }

        //SceneManager.LoadScene(sceneName);
    }

    public void handle(string ms){
        //string messageType = ms.messageType;

        //executa JSON->messageType dentro do handle
        string messageType = JsonUtility.FromJson<ServerMessage>(ms).messageType;


        // route message to handler based on message type
        Debug.Log(ms);

        if (messageType == "INICIA_JOGO") 
        {
            MSG_INICIA_JOGO(ms);
        }

    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
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