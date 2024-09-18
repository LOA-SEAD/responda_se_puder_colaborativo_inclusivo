using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using NativeWebSocket;
using System.IO;
using TMPro;
using System.Diagnostics;

public class profFimdeJogo : MonoBehaviour
{

    public TMP_Text teamInfoText;
    public TMP_Text resultado;

    public Button openFileButton;
    public Text filePathText;
    public Text fileContentText; 
    string filePath;


    // Start is called before the first frame update
    void Start()
    {
        string executablePath = Application.dataPath;
        string directoryPath = Directory.GetParent(executablePath).FullName;
        filePath = Path.Combine(directoryPath, "resultados.txt");

        resultado.text = "O arquivo com os resultados individuais foi gravado na pasta do jogo ("+filePath+").";

        string jsonContent = File.ReadAllText(filePath);
        arqLido message = JsonUtility.FromJson<arqLido>(jsonContent);
        teamInfoText.text = "";

        for (int i = 0; i < message.teams.Length; i++)
        {
            arqTime team = message.teams[i];

            string teamInfo = string.Format("{0}) Equipe {1} - Pontuação: {2}\n", team.ranking, team.idTeam, team.point);

            teamInfoText.text += teamInfo;
        }

        string formattedText = FormatData(message);
        string outputFilePath = Path.Combine(directoryPath, "resultados.txt");
        File.WriteAllText(outputFilePath, formattedText);

        openFileButton.onClick.AddListener(OpenTextFile);
            // Acessar a propriedade sessionId
    }

    void OpenTextFile()
    {
        if (!string.IsNullOrEmpty(filePath))
        {
            Process.Start(new ProcessStartInfo
            {
                FileName = filePath,
                UseShellExecute = true,
                Verb = "open"
            });
        }
        else
        {
            UnityEngine.Debug.LogError("Caminho do arquivo não está definido ou está vazio.");
        }
    }

    string FormatData(arqLido data)
    {
        System.Text.StringBuilder sb = new System.Text.StringBuilder();

        sb.AppendLine("CLASSIFICACAO_FINAL");
        sb.AppendLine();

        sb.AppendLine("RANKING");
        foreach (var team in data.teams)
        {
            sb.AppendLine($"Equipe {team.idTeam} Pontuacao: {team.point}");
        }
        sb.AppendLine();

        sb.AppendLine("INDIVIDUAL");
        foreach (var user in data.user)
        {
            sb.AppendLine($"\"nome\":\"{user.name}\",\n \"equipe\":{user.teamId},\n \"pontuação individual\":{user.indScore},\n \"interações no chat\":{user.interaction},\n \"comunicativo\":{user.elogio1},\"engajado\":{user.elogio2},\"gentil\":{user.elogio3} \n\n");
        }

        return sb.ToString();
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