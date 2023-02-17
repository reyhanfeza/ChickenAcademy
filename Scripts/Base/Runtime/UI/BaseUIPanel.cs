using System;
using System.Globalization;
using UnityEngine;

namespace Base.Runtime.UI
{
    public abstract class BaseUIPanel : MonoBehaviour
    {
        [SerializeField] private GameObject upgradePanel;

        public virtual void EnablePanel()
        {
            upgradePanel.SetActive(true);
        }

        public virtual void DisablePanel()
        {
            upgradePanel.SetActive(false);
        }

        public virtual void UpdateContent() { }

        
        public string FormatCurrency(float number) => FormatCurrency(Convert.ToDouble(number));

        /// <summary>
        /// Formats currency in 1.0K, 1.0M or 1.0B forms.
        /// </summary>
        public string FormatCurrency(double number)
        {
            double kValue = number * 0.001;
            double mScore = number  * 0.000001 ;
            double bValue = number  * 0.000000001;

            if (number < 1000)
            {
                return number.ToString("C0", CultureInfo.CreateSpecificCulture("en-US"));
            }
            else if(number>=1000 && number<1000000)
            {
                return Math.Round(kValue,2).ToString( "C2", CultureInfo.CreateSpecificCulture("en-US"))+"K";
            }else if (number >= 1000000 && number<1000000000)
            {
                return Math.Round(mScore,2).ToString( "C2", CultureInfo.CreateSpecificCulture("en-US"))+"M";
            }
            else
            {
                return Math.Round(bValue,2).ToString( "C2", CultureInfo.CreateSpecificCulture("en-US"))+"B";
            }
 
        }
    }
}
