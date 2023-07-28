using System;
using UnityEngine;

// Classe base dos JSON enviados/recebidos

[Serializable]
public class Message
{
    public string messageType;

}

[Serializable]
public class EntrarSessao : Message
{
    public string user;
    public string secret;

    public EntrarSessao( string messageType, string user, string secret){
        this.user = user;
        this.secret = secret;
        this.messageType = messageType;
        
    }

}


[Serializable]
public class CadastraSessao : Message
{
    public int nrTeams;
    public int nrPlayers;
    public int nrHelp5050;
    public int timeQuestion;
    public string moderator;

    public CadastraSessao(string messageType, int nrTeams, int nrPlayers, int nrHelp5050, int timeQuestion, string moderator)
    {
        this.nrTeams = nrTeams;
        this.nrPlayers = nrPlayers;
        this.nrHelp5050 = nrHelp5050;
        this.timeQuestion = timeQuestion;
        this.moderator = moderator;
        this.messageType = messageType;
    }

}

// [Serializable]
// public class CadastraSessao : Message
// {
//     public int nrTeams;
//     public int nrPlayers;
//     public int nrHelp5050;
//     public int timeQuestion;
//     public int[] totalQuestion;
//     public int[] questionAmount;
//     public string moderator;
//     public int gameId;

//     public CadastraSessao(string messageType, int nrTeams, int nrPlayers, int nrHelp5050, int timeQuestion, int[] totalQuestion, int[] questionAmount, string moderator, int gameId)
//     {
//         this.nrTeams = nrTeams;
//         this.nrPlayers = nrPlayers;
//         this.nrHelp5050 = nrHelp5050;
//         this.timeQuestion = timeQuestion;
//         this.totalQuestion = totalQuestion;
//         this.questionAmount = questionAmount;
//         this.moderator = moderator;
//         this.messageType = messageType;
//         this.gameId = gameId;
//     }
// }

[Serializable]
public class ComecarJogo : Message
{

    public int sessionId;
    public int gameId;

    public ComecarJogo(string messageType, int sessionId, int gameId)
    {
        this.sessionId = sessionId;
        this.gameId = gameId;
        this.messageType = messageType;

    }
}

// [Serializable]
// public class ComecarJogo : Message
// {
//     public string sessionId;
//     public int gameId;

//     public ComecarJogo(string messageType, string sessionId, int gameId)
//     {
//         this.sessionId = sessionId;
//         this.gameId = gameId;
//         this.messageType = messageType;
//     }
// }



// [Serializable]
// public class RespostaIndividual
// {
//     public string message_type;
//     public User user;
//     public int teamId;
//     public int sessionId;
//     public int gameId;
//     public string answer;
//     public int level;
//     public int nrQuestion;

//     public RespostaIndividual(string messageType, User user, int teamId, int sessionId, 
//                              int gameId, string answer, int level, int nrQuestion)
//     {
//         this.user = user;
//         this.teamId = teamId;
//         this.sessionId = sessionId;
//         this.gameId = gameId;
//         this.answer = answer;
//         this.messageType = messageType;
//         this.level = level;
//         this.nrQuestion = nrQuestion;

//     }
// }

// [Serializable]
// public class RespostaFinal
// {
//     public string message_type
//     public User user;
//     public int teamId;
//     public int sessionId;
//     public int gameId;
//     public string finalAnswer;
//     public int correct; 

//     public RespostaFinal(string messageType, User user, int teamId, int sessionId, int gameId, string finalAnswer, int correct)
//     {
//         this.user = user;
//         this.teamId = teamId;
//         this.sessionId = sessionId;
//         this.gameId = gameId;
//         this.answer = answer;
//         this.messageType = messageType;

//     }
// }

[Serializable]
public class ClientMessage : Message
{

    public Player player;

    public ClientMessage(Player playerModel, string messageType)
    {
        this.player = playerModel;
        this.messageType = messageType;
    }
}

[Serializable]
public class ClientMessagePlayerEnter : ClientMessage
{

    public ClientMessagePlayerEnter(Player playerModel) : base(playerModel, "CLIENT_MESSAGE_TYPE_PLAYER_ENTER")
    {
    }
}

[Serializable]
public class ClientMessagePlayerUpdate : ClientMessage
{

    public ClientMessagePlayerUpdate(Player playerModel) : base(playerModel, "CLIENT_MESSAGE_TYPE_PLAYER_UPDATE")
    {
    }
}

[Serializable]
public class ServerMessage: Message {

}

[Serializable]
public class ServerMessagePlayerEnter : ServerMessage
{
    public Player player;
}

[Serializable]
public class ServerMessagePlayerExit : ServerMessage
{
    public Player player;
}

[Serializable]
public class ServerMessagePlayerUpdate : ServerMessage
{
    public Player player;
}

[Serializable]
public class ServerMessageGameState : ServerMessage
{
    public GameState gameState;
}