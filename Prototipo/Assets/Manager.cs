using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Manager
{

    //SESSIONS
    public static int gameId = 0;
    public static int sessionId;

    //BASE DE QUESTOES
    public static int totalQuestoes = 0;
    public static int totalFacil = 0;
    public static int totalMedio = 0;
    public static int totalDificil = 0;
    public static int nrEasy;
    public static int nrMedium;
    public static int nrHard;

    public static int indScore = 0;
    public static int grpScore = 0;
    

    //GERAIS
    public static int nrTeam;
    public static int nrPlayerTeam;
    public static int leaderId;

    //PLAYERS
    public static int nrHelp5050;
    public static int time;
    public static int teamId;
    public static int[] qEasy;
    public static int[] qMedium;
    public static int[] qHard;
    public static string MOMENTO = "INDIVIDUAL";

    public static string FASE = "Nível Fácil";

    // public static int[] qEasy = {0, 4, 1};
    // public static int[] qMedium = {3, 2, 4};
    // public static int[] qHard = {2, 0};
    

    //MODERADOR
    public static string moderatorName;

    public static string serverURL;

    public static void SetNrTeam(int nrTeam)
    {
        Manager.nrTeam = nrTeam;
    }

    public static int GetNrTeam()
    {
        return nrTeam;
    }

    public static void SetTotalQuestoes(int totalQuestoes)
    {
        Manager.totalQuestoes = totalQuestoes;
    }

    public static int GetTotalQuestoes()
    {
        return totalQuestoes;
    }

    public static void SetTotalFacil(int totalFacil)
    {
        Manager.totalFacil = totalFacil;
    }

    public static int GetTotalFacil()
    {
        return totalFacil;
    }

    public static void SetTotalMedio(int totalMedio)
    {
        Manager.totalMedio = totalMedio;
    }

    public static int GetTotalMedio()
    {
        return totalMedio;
    }

    public static void SetTotalDificil(int totalDificil)
    {
        Manager.totalDificil = totalDificil;
    }

    public static int GetTotalDificil()
    {
        return totalDificil;
    }





}

// public class Session
// {
//     public static string messageType;
//     public static List<Team> teams;
//     public static int sessionId;
// }

// [Serializable] public class Team
// {
//     public int id;
//     public string secret;
//     // public List<Player> players;

// }

// // [Serializable] public class Player
// // {
// //     public int id;
// //     public string name;
// // }

// }

