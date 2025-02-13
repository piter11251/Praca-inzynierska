using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.ApiClient.Models.ApiModels
{
    public class GetUserPreferencesDto
    {
        public List<PreferableSugarLevelDto> PrefelableSugarLevels { get; set; } = new();
    }

    public class PreferableSugarLevelDto
    {
        public int Id { get; set; }
        public string MealMarker { get; set; }
        public int MinValue { get; set; }
        public int MaxValue { get; set; }
    }

    public class UpdateSugarLevelDto
    {
        public int Id { get; set; }
        public int MinValue { get; set; }
        public int MaxValue { get; set; }
    }

    public class UpdateUserPreferencesDto
    {
        public List<UpdateSugarLevelDto> PreferableSugarLevels { get; set; } = new();
    }
}
