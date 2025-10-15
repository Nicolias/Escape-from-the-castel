using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CodeView : MonoBehaviour
{
    [SerializeField] private List<TMP_Text> _codeHalfs;
    [SerializeField] private RectTransform _selection;

    private Queue<TMP_Text> _textQueue;
    private TMP_Text _selectedItem;

    public void Init(IReadOnlyList<string> codeHalfs)
    {
        if (codeHalfs.Count != _codeHalfs.Count)
        {
            throw new ArgumentOutOfRangeException();
        }

        _textQueue = new Queue<TMP_Text>();

        for (int i = 0; i < codeHalfs.Count; i++)
        {
            _codeHalfs[i].text = codeHalfs[i];
            _textQueue.Enqueue(_codeHalfs[i]);
        }

        Select();
    }

    public void Select()
    {
        if (_textQueue.Count == 0)
        {
            return;
        }

        _selection.anchoredPosition = new Vector2(_textQueue.Dequeue().rectTransform.anchoredPosition.x, _selection.anchoredPosition.y);
    }
}
