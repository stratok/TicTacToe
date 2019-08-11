using UnityEngine;

public interface IView
{
    GameController GameController { set; }
    
    void StartGame();
    void StopGame(CurrentPlayer player);
    void RestartGame();
    void SetActivePlayer(CurrentPlayer player);
}