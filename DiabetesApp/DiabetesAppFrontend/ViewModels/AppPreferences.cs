using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiabetesAppFrontend.ViewModels
{
    public static class AppPreferences
    {
        public static int SugarBeforeMealLower
        {
            get => Preferences.Get(nameof(SugarBeforeMealLower), 70);
            set => Preferences.Set(nameof(SugarBeforeMealLower), value);
        }

        public static int SugarBeforeMealUpper
        {
            get => Preferences.Get(nameof(SugarBeforeMealUpper), 70);
            set => Preferences.Set(nameof(SugarBeforeMealUpper), value);
        }

        public static int SugarAfterMealLower
        {
            get => Preferences.Get(nameof(SugarAfterMealLower), 140);
            set => Preferences.Set(nameof(SugarAfterMealLower), value);
        }

        public static int SugarAfterMealUpper
        {
            get => Preferences.Get(nameof(SugarAfterMealUpper), 140);
            set => Preferences.Set(nameof(SugarAfterMealUpper), value);
        }

        public static int SugarFastingLower
        {
            get => Preferences.Get(nameof(SugarFastingLower), 90);
            set => Preferences.Set(nameof(SugarFastingLower), value);
        }

        public static int SugarFastingUpper
        {
            get => Preferences.Get(nameof(SugarFastingUpper), 90);
            set => Preferences.Set(nameof(SugarFastingUpper), value);
        }
    }
}
