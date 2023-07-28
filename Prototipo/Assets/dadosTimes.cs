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

        if (team != null)
        {
            team.users.Add(user);
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
}
