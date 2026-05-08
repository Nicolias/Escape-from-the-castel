using System;
using UnityEngine;

namespace Assets.Scripts.LevelSignals
{
    [RequireComponent(typeof(LineRenderer))]
    [RequireComponent(typeof(Transform))]
    public class Curve : MonoBehaviour
    {
        private const float MinHz = 3.5f;
        private const float MaxHz = 99.75f;
        private const float MinAmplitude = 0.010f;
        private const float MaxAmplitude = 0.08f;

        [SerializeField] private int _pointsCount;
        [SerializeField] private float _pointsStep;

        private LineRenderer _lineRenderer;
        private Transform _transform;

        [field: SerializeField, Range(MinHz, MaxHz)] public float Hz { get; private set; }

        [field: SerializeField, Range(MinAmplitude, MaxAmplitude)] public float Amplitude { get; private set; }

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
            Amplitude = MinAmplitude + (MaxAmplitude - MinAmplitude) * percent;

            UpdateState();
        }

        public void SetHz(float percent)
        {
            Hz = MinHz + (MaxHz - MinHz) * percent;

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