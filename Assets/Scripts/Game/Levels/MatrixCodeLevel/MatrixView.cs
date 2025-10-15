using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MatrixView : MonoBehaviour
{
    [SerializeField] private List<MatrixItem> _items;
    [SerializeField] private SelectionBorder _border;

    private IEnumerable<MatrixItem> _currentItems;

    public event Action<Vector2Int> Interacted;


    private void OnDisable()
    {
        DiactivateItems(_currentItems);
    }

    public void SetState(MatrixCode matrix)
    {
        DiactivateItems(_currentItems);

        _border.Init();

        foreach (MatrixItem matrixitem in _items)
        {
            matrixitem.Init();

            Vector2Int itemMatrixPosition = CalculateMatrixPosition(matrixitem.Position);
            matrixitem.SetText(matrix.GetItem(itemMatrixPosition.y, itemMatrixPosition.x));
            matrixitem.Clicked += OnItemClicked;
        }

        _currentItems = _items;
    }

    public void ActivateRaw(int value)
    {
        IEnumerable<MatrixItem> raw = _items.Where(item => CalculateMatrixPosition(item.Position).y == value);
        ActivateItems(raw);

        _border.TransformSelectionRaw(new Vector2(0.5f, raw.First().Position.y), Quaternion.Euler(0f, 0f, 90f));
    }
    
    public void ActivateColumn(int value)
    {
        IEnumerable<MatrixItem > column = _items.Where(item => CalculateMatrixPosition(item.Position).x == value);
        ActivateItems(column);

        _border.TransformSelectionRaw(new Vector2(column.First().Position.x, -0.5f), Quaternion.Euler(0f, 0f, 0f));
    }

    private void ActivateItems(IEnumerable<MatrixItem> items)
    {
        DiactivateItems(_currentItems);
        _currentItems = items;

        foreach (var item in items)
        {
            item.Clicked += OnItemClicked;
        }
    }

    private void OnItemClicked(MatrixItem item)
    {
        _border.MoveTo(item.Position);
        Interacted?.Invoke(CalculateMatrixPosition(item.Position));
    }

    private void DiactivateItems(IEnumerable<MatrixItem> items)
    {
        if (items == null)
        {
            return;
        }

        foreach (var item in items)
        {
            item.Clicked -= OnItemClicked;
        }
    }

    private Vector2Int CalculateMatrixPosition(Vector2 position)
    {
        float scale = 0.25f;
        float halfScale = 0.125f;

        return new Vector2Int(Mathf.RoundToInt(Mathf.Abs((position.x - halfScale) / scale)), Mathf.RoundToInt(Mathf.Abs((position.y + halfScale) / scale)));
    }
}
