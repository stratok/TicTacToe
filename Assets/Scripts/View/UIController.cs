using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIController : MonoBehaviour, IView
{
    private GameController _gameController;

    [SerializeField] private GameObject _infoPanel;
    [SerializeField] private GameObject _resultPanel;
    [SerializeField] private Text       _resultPanelText;
    [SerializeField] private GameObject _playerXButton;
    [SerializeField] private GameObject _playerOButton;

    [SerializeField] private Text _XWins;
    [SerializeField] private Text _OWins;
    [SerializeField] private Text _drows;

    public GameController GameController { set { _gameController = value; } }

    public void StartGame()
    {
        _resultPanel.SetActive(false);
        _infoPanel.SetActive(true);

        ActivatePlayersButton();
    }

    public void StopGame(CurrentPlayer player)
    {
        switch (player)
        {
            case CurrentPlayer.PlayerX:
                _resultPanelText.text = "Победили крестики!";
                break;
            case CurrentPlayer.PlayerO:
                _resultPanelText.text = "Победили нолики!";
                break;
            case CurrentPlayer.None:
                _resultPanelText.text = "Ничья!";
                break;
            default:
                break;
        }

        UpdateStatistics();
        _resultPanel.SetActive(true);
    }

    public void RestartGame()
    {
        _gameController.RestartGame();
    }

    public void ChooseFirstSide(int playerId)
    {
        CurrentPlayer player = (CurrentPlayer)playerId;

        if (player == CurrentPlayer.PlayerX)
        {
            _playerXButton.SetActive(true);
            _playerOButton.SetActive(false);
            _gameController.CurrentPlayer = player;
        }
        else
        {
            _playerXButton.SetActive(false);
            _playerOButton.SetActive(true);
            _gameController.CurrentPlayer = player;
        }

        DeactivatePlayersButton();
        _infoPanel.SetActive(false);
        _gameController.SetFieldInteractable(true);
    }

    public void SetActivePlayer(CurrentPlayer player)
    {
        if (player == CurrentPlayer.PlayerX)
        {
            _playerXButton.SetActive(true);
            _playerOButton.SetActive(false);
        }
        else
        {
            _playerXButton.SetActive(false);
            _playerOButton.SetActive(true);
        }
    }

    public void DeactivatePlayersButton()
    {
        _playerXButton.GetComponent<Button>().interactable = false;
        _playerXButton.GetComponent<Button>().interactable = false;
    }

    public void ActivatePlayersButton()
    {
        _playerXButton.SetActive(true);
        _playerXButton.GetComponent<Button>().interactable = true;
        _playerOButton.SetActive(true);
        _playerXButton.GetComponent<Button>().interactable = true;
    }

    public void UpdateStatistics()
    {
        _XWins.text = _gameController.XWins.ToString();
        _OWins.text = _gameController.OWins.ToString();
        _drows.text = _gameController.Drows.ToString();
    }

    public void Exit()
    {
        SceneManager.LoadScene(0);
    }
}
