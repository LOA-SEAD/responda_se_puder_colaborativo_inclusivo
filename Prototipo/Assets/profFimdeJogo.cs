using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using NativeWebSocket;
using System.IO;
using TMPro;
using SFB;


public class profFimdeJogo : MonoBehaviour
{

    public TMP_Text teamInfoText;
    public TMP_Text resultado;

    public Button openFileButton;
    public Text filePathText;
    public Text fileContentText; 

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

        string formattedText = FormatData(message);
        string outputFilePath = Path.Combine(directoryPath, "resultados.txt");
        File.WriteAllText(outputFilePath, formattedText);

            // Acessar a propriedade sessionId
        Debug.Log("Session ID lido do arquivo: " + message.sessionId);

        openFileButton.onClick.AddListener(OpenFileDialog);

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
            sb.AppendLine($"\"name\":\"{user.name}\",\"teamId\":{user.teamId},\"indScore\":{user.indScore},\"interaction\":{user.interaction},\"elogio1\":{user.elogio1},\"elogio2\":{user.elogio2},\"elogio3\":{user.elogio3}");
        }

        return sb.ToString();
    }

    void OpenFileDialog()
    {
        var paths = StandaloneFileBrowser.OpenFilePanel("Open File", "", "txt", false);
        if (paths.Length > 0)
        {
            string filePath = paths[0];
            filePathText.text = filePath;
            ReadAndDisplayFile(filePath);
        }
    }

    void ReadAndDisplayFile(string filePath)
    {
        if (File.Exists(filePath))
        {
            string fileContent = File.ReadAllText(filePath);
            fileContentText.text = fileContent;
            Debug.Log("Arquivo lido: " + fileContent);
        }
        else
        {
            Debug.LogError("Arquivo não encontrado: " + filePath);
        }
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