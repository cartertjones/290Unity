using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour, IGameManager
{
    public ManagerStatus status { get; private set; }

    public void Startup()
    {
        Debug.Log("Inventory manager starting...");
        status = ManagerStatus.Started;
    }

    private void DisplayItems(int playerId) {
        Managers.Player.players[playerId].DisplayItems();
    }

    public void AddItem(string name, int playerId) {
        Managers.Player.players[playerId].AddItem(name);
    }

    public List<string> GetItemList(int playerId) {
        return Managers.Player.players[playerId].GetItemList();
    }

    public int GetItemCount(string name, int playerId) {
        return Managers.Player.players[playerId].GetItemCount(name);
    }
}
