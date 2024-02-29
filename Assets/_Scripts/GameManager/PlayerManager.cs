using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour, IGameManager
{
    public ManagerStatus status { get; private set; }

    public Player[] players;

    public void Startup() {
        Debug.Log("Player manager starting...");

        Player player1 = new Player(0);
        Player player2 = new Player(1);

        players = new Player[] { player1, player2 };

        status = ManagerStatus.Started;
    }
}
