using System;
using UnityEngine;

namespace Assets.Scripts.LevelSignals
{
    [RequireComponent(typeof(LineRenderer))]
    [RequireComponent(typeof(Transform))]
    public class Curve : MonoBehaviour
    {
        [SerializeField] private int _pointsCount;
        [SerializeField] private float _pointsStep;

        private LineRenderer _lineRenderer;
        private Transform _transform;
        private float _minHz = 10f;
        private float _maxHz = 35f;
        private float _minAmplitude = 0.03f;
        private float _maxAmplitude = 0.2f;

        [field: SerializeField, Range(10f, 35f)] public float Hz { get; private set; }

        [field: SerializeField, Range(0.03f, 0.2f)] public float Amplitude { get; private set; }

        public void Init()
        {
            _lineRenderer = GetComponent<LineRenderer>();
            _transform = transform;
            _lineRenderer.startWidth = 0.01f;
            _lineRenderer.endWidth = 0.01f;
            _lineRenderer.positionCount = _pointsCount;

            _lineRenderer.SetPositions(GetCurvePoints());
        }

        public void SetAmplitude(float percent)
        {
            Amplitude = _minAmplitude + (_maxAmplitude - _minAmplitude) * Mathf.Clamp01(percent);

            UpdateState();
        }

        public void SetHz(float percent)
        {
            Hz = _minHz + (_maxHz - _minHz) * Mathf.Clamp01(percent);

            UpdateState();
        }

        private void UpdateState() => _lineRenderer.SetPositions(GetCurvePoints());

        private Vector3[] GetCurvePoints()
        {
            Vector3[] pointsArr = new Vector3[_pointsCount];
            Vector3 rightOffset = Vector3.right * _pointsCount * _pointsStep / 2;
            float xCoordinate = (_transform.position - rightOffset).x;
            float yCoordinate;

            for (int i = 0; i < _pointsCount; i++)
            {
                yCoordinate = _transform.position.y + Amplitude * Mathf.Sin(Hz * xCoordinate);
                pointsArr[i] = new Vector3(xCoordinate, yCoordinate, _transform.position.z);
                xCoordinate += _pointsStep;
            }

            return pointsArr;
        }
    }
}