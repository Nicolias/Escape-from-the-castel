using TMPro;
using UnityEngine;

namespace Cryptography.Servis
{
    public class CharFactory : MonoBehaviour
    {
        [SerializeField] private TMP_Text _template;
        [SerializeField] private Transform _parent;

        public void Create(Vector3 position, Quaternion rotation, string text)
        {
            TMP_Text symbol = Instantiate(_template, _parent);
            symbol.transform.localPosition = position;
            symbol.transform.localRotation = rotation;
            symbol.text = text;
        }
    }
}