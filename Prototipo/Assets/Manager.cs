using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Manager
{

    //URL
    public static string serverURL;


//TESTES
    // public static string serverURL = "ws://localhost:5000";
    // public static int[] qEasy = {0, 4, 1};
    // public static int[] qMedium = {3, 2, 4};
    // public static int[] qHard = {2, 0};

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

    public static string MOMENTO = "INDIVIDUAL";

    public static string FASE = "Nível Fácil";

    //QUESTOES A SEREM JOGADAS
    public static int[] qEasy;
    public static int[] qMedium;
    public static int[] qHard;
    public static int[] numQ;

    public static int nQ_easy;
    public static int nQ_medium;
    public static int nQ_hard;
    public static int nQ_total;
    
    

    //MODERADOR
    public static string moderatorName;


    public static string msgPrimeiraQuestao;


    public static void countQuestoesJogo()
    {
        Manager.nQ_easy = qEasy.Length - 1;
        Manager.nQ_medium = qMedium.Length - 1;
        Manager.nQ_hard = qHard.Length - 1;
        Manager.nQ_total = Manager.nQ_easy + Manager.nQ_medium + Manager.nQ_hard;
        
        int totalLength = qEasy.Length + qMedium.Length + qHard.Length;

        numQ = new int[totalLength];

        int index = 0;

        for (int i = 0; i < qEasy.Length; i++) {
            numQ[index] = qEasy[i];
            index++;
        }

        for (int i = 0; i < qMedium.Length; i++) {
            numQ[index] = qMedium[i];
            index++;
        }

        for (int i = 0; i < qHard.Length; i++) {
            numQ[index] = qHard[i];
            index++;
        }
    }

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

