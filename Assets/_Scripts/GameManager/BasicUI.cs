using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Point = UnityEngine.Vector2;

public class BasicUI : MonoBehaviour
{
    //get canvas reference
    //get inventory item ui prefab reference

    List<string> itemList;

    public Player player;

    // void OnGUI() {
    //     int posX = (player == Player.Player1) ? 10 : Screen.width - 100;
    //     int posY = Screen.height - 100;
    //     int width = 100;
    //     int height = 30;
    //     int buffer = 10;

    //     List<string> itemList = Managers.Inventory.GetItemList(player);
    //     if(itemList.Count == 0) {
    //         GUI.Box(new Rect(posX, posY, width, height), "Empty");
    //     }
    //     foreach (string item in itemList) {
    //         int count = Managers.Inventory.GetItemCount(item, player);
    //         Texture2D image = Resources.Load<Texture2D>($"Icons/{item}");
    //         GUI.Box(new Rect(posX, posY, width, height), new GUIContent($"{count}", image));
    //         posX = (player == Player.Player1) ? (posX + width + buffer) : (posX - width - buffer);
    //     }
    // }

    void OnGUI() {
        int width = 100; 
        int height = 30;
        int buffer = 10;

        Point point1 = new Point(buffer, Screen.height - width);
        Point point2 = new Point(Screen.width - (width + buffer), Screen.height - width);

        List<string> player1Items = Managers.Inventory.GetItemList(0);
        List<string> player2Items = Managers.Inventory.GetItemList(1);

        if(player1Items.Count == 0) {
            GUI.Box(new Rect(point1.x, point1.y, width, height), "Empty");
        } else {
            foreach(string item in player1Items) {
                int count = Managers.Inventory.GetItemCount(item, 0);
                if(item == "points") GameManager.Instance.player1Score = count;
                GUI.Box(new Rect(point1.x, point1.y, width, height), new GUIContent($"{count} {item}"));
            }
        }
        if(player2Items.Count == 0) {
            GUI.Box(new Rect(point2.x, point2.y, width, height), "Empty");
        } else {
            foreach(string item in player2Items) {
                int count = Managers.Inventory.GetItemCount(item, 1);
                if(item == "points") GameManager.Instance.player2Score = count;
                GUI.Box(new Rect(point2.x, point2.y, width, height), new GUIContent($"{count} {item}"));
            }
        }
    }
}
