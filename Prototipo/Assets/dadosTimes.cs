using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class dadosTimes : MonoBehaviour
{

    public static User player;

    public static List<Team> listaTimes = new List<Team>();

    public static List<User> meuTime = new List<User>();            

    public static void addPlayer(User user, int teamId)
    {
        Team team = listaTimes.Find(t => t.id == teamId);

        // Debug.Log("Time ID encontrado: " + team.id);

        if (team != null)
        {
            team.users.Add(user);
        }
    }

    public static string GetUser(int userId)
    {
        User user = meuTime.Find(u => u.id == userId);

        return user.name;

    }

    public static void SetEquipe()
    {
        for (int i = meuTime.Count - 1; i >= 0; i--)
        {
            User user = meuTime[i];
            if (user.id == 1)
            {
                meuTime.RemoveAt(i);
            }
        }
    }


}


[System.Serializable] 
public class Team
{
    public int id;
    public string secret;
    public List<User> users;
}

[System.Serializable] 
public class User
{
    public int id;
    public string name;
    // public int indScore = 0;
}
