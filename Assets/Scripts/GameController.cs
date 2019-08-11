using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    private List<Cell> _cells = new List<Cell>();
    private int moveCount;
    private IView _view;

    public CurrentPlayer CurrentPlayer { get; set; }

    // Statistics
    public int XWins { get; private set; }
    public int OWins { get; private set; }
    public int Drows { get; private set; }

    private void Start()
    {
        FillCellList();
        GetReferences();
        SetReferences();
        SetFieldInteractable(false);
        StartGame();
    }

    private void FillCellList()
    {
        var cells = FindObjectsOfType<Cell>();

        for (int i = 0; i < cells.Length; i++)
        {
            _cells.Add(cells[i]);
        }
    }

    private void GetReferences()
    {
        _view = FindObjectOfType<UIController>();
    }

    private void SetReferences()
    {
        for (int i = 0; i < _cells.Count; i++)
        {
            _cells[i].GetComponentInParent<Cell>().SetGameController(this);
        }

        _view.GameController = this;
    }

    private void StartGame()
    {
        moveCount = 0;
        SetFieldInteractable(false);
        _view.StartGame();
    }

    public void RestartGame()
    {
        ClearField();
        StartGame();
    }

    private void StopGame(CurrentPlayer winner)
    {
        SetFieldInteractable(false);
        ChangeStatistics(winner);
        _view.StopGame(winner);
    }

    private void ChangeStatistics(CurrentPlayer winner)
    {
        switch (winner)
        {
            case CurrentPlayer.PlayerX:
                XWins++;
                break;
            case CurrentPlayer.PlayerO:
                OWins++;
                break;
            case CurrentPlayer.None:
                Drows++;
                break;
            default:
                break;
        }
    }

    public void CompleteStep()
    {
        moveCount++;

        // Размерность доски
        int sqrtLength = (int)Mathf.Sqrt(_cells.Count);

        // Проверка совпадений по горизонтали
        for (int i = 0; i < _cells.Count; i+= sqrtLength)
        {
            bool win = true;
            for (int j = 0; j < sqrtLength; j++)
            {
                if (_cells[i + j].CurrentStatus != CurrentPlayer) win = false;
            }
            if (win)
            {
                StopGame(CurrentPlayer);
                return;
            }
        }

        // Проверка совпадений по вертикали
        for (int i = 0; i < sqrtLength; i++)
        {
            bool win = true;
            for (int j = 0; j < _cells.Count; j += sqrtLength)
            {
                if (_cells[j + i].CurrentStatus != CurrentPlayer) win = false;
            }
            if (win)
            {
                StopGame(CurrentPlayer);
                return;
            }
        }

        // Проверка совпадений по первой диагонали
        bool winFirstDiag = true;
        for (int i = 0; i < _cells.Count; i += sqrtLength + 1)
        {
            if (_cells[i].CurrentStatus != CurrentPlayer) winFirstDiag = false;
        }
        if (winFirstDiag)
        {
            StopGame(CurrentPlayer);
            return;
        }

        // Проверка совпадений по второй диагонали
        bool winSecondDiag = true;
        for (int i = sqrtLength-1; i <= _cells.Count - sqrtLength; i += sqrtLength - 1)
        {
            if (_cells[i].CurrentStatus != CurrentPlayer) winSecondDiag = false;
        }
        if (winSecondDiag)
        {
            StopGame(CurrentPlayer);
            return;
        }

        if (moveCount == _cells.Count)
        {
            StopGame(CurrentPlayer.None);
            return;
        }

        ChangeSides();
    }

    private void ChangeSides()
    {
        CurrentPlayer = (CurrentPlayer == CurrentPlayer.PlayerX) ? CurrentPlayer.PlayerO : CurrentPlayer.PlayerX;

        if (CurrentPlayer == CurrentPlayer.PlayerX)
            _view.SetActivePlayer(CurrentPlayer.PlayerX);
        else
            _view.SetActivePlayer(CurrentPlayer.PlayerO);
    }

    public void SetFieldInteractable(bool state)
    {
        for (int i = 0; i < _cells.Count; i++)
        {
            _cells[i].GetComponentInParent<Button>().interactable = state;
        }
    }

    private void ClearField()
    {
        for (int i = 0; i < _cells.Count; i++)
        {
            _cells[i].ResetCurrentStatus();
        }
    }
}
