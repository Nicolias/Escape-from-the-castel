using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.DotsLevel
{
    public abstract class GameCircle<T> : MonoBehaviour where T : CirclePoint
    {
        [SerializeField] private List<T> _points;

        protected RectTransform RectTransform;
        
        private T[] _pointsState;

        public int Count => _pointsState.Length;

        public T this[int index]
        {
            get
            {
                if (index < 0 || index >= _pointsState.Length)
                {
                    throw new ArgumentOutOfRangeException("index");
                }

                return _pointsState[index];
            }
        }

        public virtual void Init()
        {
            RectTransform = GetComponent<RectTransform>();
            _pointsState = new T[_points.Count];

            foreach (T point in _points)
            {
                point.Init();
            }

            ResetState();
        }

        protected virtual void ResetState()
        {
            Debug.Log(" ");
            foreach (T point in _points)
            {
                int index = GetIndexByPosition(point);
                _pointsState[index] = point;
            }
        }

        protected int GetIndexByPosition(T position)
        {
            float rotation = Quaternion.FromToRotation(RectTransform.rotation * position.AnchoredPosition, Vector3.up).eulerAngles.z;

            Debug.Log(position.name + " " + RectTransform.rotation + " " + rotation + " " + Mathf.RoundToInt(rotation / 45f));

            return Mathf.RoundToInt(rotation / 45f);
        }
    }
}