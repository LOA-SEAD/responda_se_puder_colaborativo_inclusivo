using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using NativeWebSocket;
using System.IO;
using TMPro;


public class profFimdeJogo : MonoBehaviour
{

    public TMP_Text teamInfoText;
    public TMP_Text resultado;

    public void DisplayTeamInformation(arqLido message)
    {

    }


    // Start is called before the first frame update
    void Start()
    {
        string executablePath = Application.dataPath;
        string directoryPath = Directory.GetParent(executablePath).FullName;
        string filePath = Path.Combine(directoryPath, "resultados.txt");

        resultado.text = "O arquivo com os resultados individuais foi gravado na pasta do jogo ("+filePath+").";

        string jsonContent = File.ReadAllText(filePath);
        arqLido message = JsonUtility.FromJson<arqLido>(jsonContent);
        teamInfoText.text = "";

        for (int i = 0; i < message.teams.Length; i++)
        {
            Debug.Log("Entrou aqui");
            arqTime team = message.teams[i];

            string teamInfo = string.Format("{0}) Equipe {1} - Pontuação: {2}\n", team.ranking, team.idTeam, team.point);

            teamInfoText.text += teamInfo;
        }

            // Acessar a propriedade sessionId
        Debug.Log("Session ID lido do arquivo: " + message.sessionId);

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

    // Classe para mapear a estrutura do JSON
    [System.Serializable]
    public class arqLido
    {
        public string messageType;
        public arqTime[] teams;
        public arqUser[] user;
        public int sessionId;
        public int gameId;
    }

    [System.Serializable]
    public class arqTime
    {
        public int idTeam;
        public int point;
        public int gameTime;
        public int ranking;
    }

    [System.Serializable]
    public class arqUser
    {
        public string _id;
        public int id;
        public string name;
        public int teamId;
        public int indScore;
        public int interaction;
        public int elogio1;
        public int elogio2;
        public int elogio3;
        public int sessionId;
        public string ws_id;
    }