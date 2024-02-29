using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player
{
    public int playerId;
    public Dictionary<string, int> inventory;

    public Player(int id) {
        playerId = id;
        inventory = new Dictionary<string, int>();
    }

    public void AddItem(string name) {
        if(inventory.ContainsKey(name)) {
            inventory[name] += 1;
        } else {
            inventory[name] = 1;
        }
    }

    public void DisplayItems() {
        string itemDisplay = "Items: ";
        foreach (KeyValuePair<string, int> item in inventory) {
            itemDisplay += item.Key + "(" + item.Value + ") ";
        }
        Debug.Log(itemDisplay);
    }

    public List<string> GetItemList() {
        List<string> list = new List<string>(inventory.Keys);
        return list;
    }

    public int GetItemCount(string name) {
        if(inventory.ContainsKey(name)) {
            return inventory[name];
        }
        return 0;
    }
}
