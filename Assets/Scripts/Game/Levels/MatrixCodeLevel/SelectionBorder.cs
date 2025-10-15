using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
[RequireComponent(typeof(RectTransform))]
public class SelectionBorder : MonoBehaviour
{
    [SerializeField] private RectTransform _rawSelection;

    private Image _borderImage;
    private RectTransform _rectTransform;

    public void Init()
    {
        _borderImage = _borderImage == null ? GetComponent<Image>() : _borderImage;
        _rectTransform = _rectTransform == null ? GetComponent<RectTransform>() : _rectTransform;
    }

    public void MoveTo(Vector2 position)
    {
        float moveSpeed = 0.5f;

        _rectTransform.DOAnchorPos(position, moveSpeed);
    }

    public void TransformSelectionRaw(Vector2 position, Quaternion rotation)
    {
        _rawSelection.DOAnchorPos(position, 0.5f);
        _rawSelection.DORotateQuaternion(rotation, 0.5f);
    }
}