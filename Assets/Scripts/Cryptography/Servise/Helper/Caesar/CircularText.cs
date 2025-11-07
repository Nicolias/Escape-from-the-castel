using UnityEngine;

namespace Cryptography.Servis.Caesar
{
    public class CircularText : MonoBehaviour
    {
        [SerializeField] private float _radius = 250f;

        [SerializeField] private CharFactory _charFactory;
        [SerializeField] private Locolizer _locolizer;

        public float CharStep { get; private set; }

        private void Awake()
        {
            int count = _locolizer.CurrentAlphabet.Length;

            for (int i = 0; i < count; i++)
            {
                CharStep = -i * Mathf.PI * 2f / count;
                Vector3 position = new Vector3(Mathf.Cos(CharStep), Mathf.Sin(CharStep), 0) * _radius;
                Quaternion rotation = Quaternion.Euler(0, 0, CharStep * Mathf.Rad2Deg + -90);

                _charFactory.Create(position, rotation, _locolizer.CurrentAlphabet[i].ToString());
            }
        }
    }
}