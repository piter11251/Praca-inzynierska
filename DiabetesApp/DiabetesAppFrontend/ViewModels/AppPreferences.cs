using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiabetesAppFrontend.ViewModels
{
    public static class AppPreferences
    {
        public static int SugarLow
        {
            get => Preferences.Get(nameof(SugarLow), 70);
            set => Preferences.Set(nameof(SugarLow), value);
        }

        public static int SugarHigh
        {
            get => Preferences.Get(nameof(SugarLow), 140);
            set => Preferences.Set(nameof(SugarLow), value);
        }

        public static int BloodPressureLow
        {
            get => Preferences.Get(nameof(BloodPressureLow), 90);
            set => Preferences.Set(nameof(BloodPressureLow), value);
        }

        public static int BloodPressureHigh
        {
            get => Preferences.Get(nameof(BloodPressureHigh), 140);
            set => Preferences.Set(nameof(BloodPressureHigh), value);
        }
    }
}
