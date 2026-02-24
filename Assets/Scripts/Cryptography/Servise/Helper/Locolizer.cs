using Lean.Localization;
using System.Collections.Generic;
using UnityEngine;
using YG;

namespace Cryptography.Servis
{
    public class Locolizer : MonoBehaviour
    {
        private const string RuAlpabet = "АБВГДЕЖЗИЙКЛМНОПРСТУФХЦЧШЩЪЫЬЭЮЯ";
        private const string EnAlpabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        private const string TrkAlphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZÇĞİÖŞÜ";

        private const string EnglishCode = "en";
        private const string TurkishCode = "tr";
        private const string RussianCode = "ru";

        [SerializeField] private LeanLocalization _leanLocalization;

        [SerializeField] private List<string> _enTexts;
        [SerializeField] private List<string> _ruTexts;
        [SerializeField] private List<string> _trkTexts;

        public string CurrentAlphabet { get; private set; }
        public IEnumerable<string> CurrentTexts { get; private set; }
        public Languages CurrentLanguage { get; private set; }

        public void Initialize()
        {
            switch (YG2.envir.language)
            {
                case EnglishCode:
                    SelectEnglish();
                    break;
                case RussianCode:
                    SelectRussian();
                    break;
                case TurkishCode:
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