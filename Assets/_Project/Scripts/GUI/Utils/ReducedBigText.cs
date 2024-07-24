using UnityEngine;
using TMPro;

namespace FunnyBlox.GUI
{
    public class ReducedBigText : MonoBehaviour
    {
        private bool initialize;
        private TextMeshProUGUI _labelGUI;
        private TextMeshPro _label;

        private void Initialize()
        {
            _labelGUI = GetComponent<TextMeshProUGUI>();
            _label = GetComponent<TextMeshPro>();
            initialize = true;
        }

        public void SetValue(float value, bool floatFormat = false)
        {
            if (!initialize) Initialize();
            if (_labelGUI)
                _labelGUI.text = GetText(value, floatFormat);
            if (_label)
                _label.text = GetText(value, floatFormat);
        }

        public void SetText(string text)
        {
            _labelGUI.text = text;
        }

        public static string GetText(float value, bool floatFormat = false)
        {
            string result;

            if (value < 1000)
            {
                if (floatFormat)
                    result = value.ToString("F1");
                else
                    result = ((int)value).ToString(); //.ToString("F").TrimEnd('0').TrimEnd('.');
            }
            else if (value < 1000000)
            {
                if (value % 1000 == 0)
                    result = (value / 1000).ToString() + "K";
                else if (value % 100 == 0)
                    result = (value / 1000).ToString("F1") + "K";
                else
                    result = (value / 1000).ToString("F2") + "K";
            }
            else if (value < 1000000000)
            {
                if (value % 1000000 == 0)
                    result = (value / 1000000).ToString() + "M";
                else if (value % 100000 == 0)
                    result = (value / 1000000).ToString("F1") + "M";
                else
                    result = (value / 1000000).ToString("F2") + "M";
            }
            else
            {
                if (value % 1000000000 == 0)
                    result = (value / 1000000000).ToString() + "B";
                else if (value % 1000000 == 0)
                    result = (value / 1000000000).ToString("F1") + "B";
                else
                    result = (value / 1000000000).ToString("F2") + "B";
            }

            return result;
        }
    }
}