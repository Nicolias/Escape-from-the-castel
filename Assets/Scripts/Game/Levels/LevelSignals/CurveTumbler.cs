using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Transform))]
public abstract class CurveTumbler : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IDragHandler
{
    private Curve _curve;
    private RectTransform _transform;
    private bool _canDrag;
    private float _maxRotation = 90;
    private float _minRotation = 270f;
    private Coroutine _rotationCoroutine;
    private Quaternion _targetRotation;

    public event Action CurveChanged;

    public void Init(Curve curve)
    {
        _transform = GetComponent<RectTransform>();
        _curve = curve;
        _curve.Init();
    }

    public void OnPointerEnter(PointerEventData eventData) => _canDrag = true;

    public void OnPointerExit(PointerEventData eventData) => _canDrag = false;

    public void OnDrag(PointerEventData eventData)
    {
        if (_canDrag)
        {
            Vector3 pointerDirection = eventData.pointerCurrentRaycast.worldPosition - _transform.position;
            pointerDirection.z = 0;

            _targetRotation = Quaternion.LookRotation(_transform.forward, pointerDirection);
            _targetRotation = Quaternion.Euler(0f, 0f, Mathf.Clamp(_targetRotation.eulerAngles.z, _maxRotation, _minRotation));

            _rotationCoroutine ??= StartCoroutine(RotateRoutine());
        }
    }

    protected abstract void ChangeCurve(Curve curve, float percent);

    private IEnumerator RotateRoutine()
    {
        while (_transform.localRotation != _targetRotation)
        {
            float rotationStep = Time.deltaTime * 10f;

            _transform.localRotation = Quaternion.Lerp(_transform.localRotation, _targetRotation, rotationStep);

            float rotationPercent = (_transform.localRotation.eulerAngles.z - _maxRotation) / (_minRotation - _maxRotation);

            ChangeCurve(_curve, rotationPercent);

            yield return null;
        }

        _rotationCoroutine = null;

        CurveChanged?.Invoke();
    }
}
