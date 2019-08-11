using UnityEngine;
using UnityEngine.UI;

public class Cell : MonoBehaviour
{
    [SerializeField] private Button _button;
    [SerializeField] private GameObject[] _images = new GameObject[2];

    private CurrentPlayer _currentStatus = CurrentPlayer.None;
    private GameController _gameController;

    public CurrentPlayer CurrentStatus { get { return _currentStatus; } }

    public void MakeMove()
    {
        _images[(int)_gameController.CurrentPlayer].SetActive(true);
        _currentStatus = _gameController.CurrentPlayer;

        _button.interactable = false;

        _gameController.CompleteStep();
    }

    public void SetGameController(GameController controller)
    {
        _gameController = controller;
    }

    public void ResetCurrentStatus()
    {
        _currentStatus = CurrentPlayer.None;
        for (int i = 0; i < _images.Length; i++)
        {
            _images[i].SetActive(false);
        }
    }
}