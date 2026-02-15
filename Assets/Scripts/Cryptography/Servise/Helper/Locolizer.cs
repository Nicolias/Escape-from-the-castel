using Lean.Localization;
using System.Collections.Generic;
using UnityEngine;

namespace Cryptography.Servis
{
    public class Locolizer : MonoBehaviour
    {
        private const string RuAlpabet = "АБВГДЕЖЗИЙКЛМНОПРСТУФХЦЧШЩЪЫЬЭЮЯ";
        private const string EnAlpabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        private const string TrkAlphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZÇĞİÖŞÜ";

        [SerializeField] private LeanLocalization _leanLocalization;

        [SerializeField] private List<string> _enTexts;
        [SerializeField] private List<string> _ruTexts;
        [SerializeField] private List<string> _trkTexts;

        public string CurrentAlphabet { get; private set; }
        public IEnumerable<string> CurrentTexts { get; private set; }
        public Languages CurrentLanguage { get; private set; }

        public void Initialize()
        {
            switch (_leanLocalization.CurrentLanguage)
            {
                case nameof(Languages.English):
                    SelectEnglish();
                    break;
                case nameof(Languages.Russian):
                    SelectRussian();
                    break;
                case nameof(Languages.Turkish):
                    SelectTurkish();
                    break;
                default:
                    break;
            }
        }

        private void SelectRussian()
        {
            CurrentAlphabet = RuAlpabet;
            CurrentTexts = _ruTexts;
            CurrentLanguage = Languages.Russian;
        }

        private void SelectEnglish()
        {
            CurrentAlphabet = EnAlpabet;
            CurrentTexts = _enTexts;
            CurrentLanguage = Languages.English;
        }

        private void SelectTurkish()
        {
            CurrentAlphabet = TrkAlphabet;
            CurrentTexts = _trkTexts;
            CurrentLanguage = Languages.Turkish;
        }
    }

    public enum Languages
    {
        English,
        Russian,
        Turkish
    }
}