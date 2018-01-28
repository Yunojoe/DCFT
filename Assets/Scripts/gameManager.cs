using UnityEngine;
using System.Collections;

public class gameManager : MonoBehaviour
{

    private void Start(){
        BeginGame();
    }

    private void Update(){
        if (Input.GetKeyDown(KeyCode.Space)) //Todo - change the "reset" key to something better or even a menu button
        {
            RestartGame();
        }
    }

    public mapGenerator dungeonPrefab;
    private mapGenerator dungeonInstance;

    private void BeginGame() {
        dungeonInstance = Instantiate(dungeonPrefab) as mapGenerator;
        dungeonInstance.Generate();
    }

    private void RestartGame() {

    }
}