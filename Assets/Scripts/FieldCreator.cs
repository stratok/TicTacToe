using UnityEngine;
using UnityEngine.UI;

public class FieldCreator : MonoBehaviour
{
    [SerializeField] private GameObject _cell;
    [SerializeField] private GridLayoutGroup _gridLayout;

    private void Awake()
    {
        _gridLayout = GetComponent<GridLayoutGroup>();
        CreateField(Settings.FieldSize);
    }

    public void CreateField(int cells)
    {
        for (int i = 0; i < cells; i++)
        {
            Instantiate(_cell, gameObject.transform);
        }
        _gridLayout.constraintCount = (int)Mathf.Sqrt(Settings.FieldSize);
    }
}
