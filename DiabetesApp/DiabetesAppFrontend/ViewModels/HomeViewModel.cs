using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Demo.ApiClient;
using Demo.ApiClient.Models.ApiModels;
using DiabetesAppFrontend.Views;
using IntelliJ.Lang.Annotations;
using Syncfusion.Maui.Charts;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiabetesAppFrontend.ViewModels
{
    public partial class HomeViewModel : ObservableObject
    {
        private readonly DemoApiClientService _apiService;

        [ObservableProperty]
        private string userName;

        [ObservableProperty]
        private bool isBusy;

        [ObservableProperty]
        private double chartInterval = 1;

        [ObservableProperty]
        private DateTime chartStartDate = DateTime.Now.AddDays(-7);

        [ObservableProperty]
        private DateTime chartEndDate = DateTime.Now;

        [ObservableProperty]
        private string errorMessage;

        [ObservableProperty]
        private int selectedChartIndex;

        [ObservableProperty]
        private ChartSeriesCollection chartSeries = new ChartSeriesCollection();

        [ObservableProperty]
        private int selectedDays;

        public List<int> DaysOptions { get; } = new List<int>() { 7, 14, 30 };

        private ObservableCollection<SugarEntry> SugarEntries { get; } = new();

        private ObservableCollection<BloodPressureDto> BloodPressureEntries { get; } = new();

        private DataPointSelectionBehavior _sugarBehavior;
        private DataPointSelectionBehavior _stolicBehavior;
        private DataPointSelectionBehavior _diastolicBehavior;

        private LineSeries _sugarSeries;
        private LineSeries _stolicSeries;
        private LineSeries _diastolicSeries;


        public HomeViewModel(DemoApiClientService apiService)
        {
            _apiService = apiService;
            SelectedChartIndex = 0;
            SelectedDays = 7;
            ChartInterval = Math.Ceiling((double)SelectedDays / 7.0);
            _ = LoadSugarChartAsync();
        }

        partial void OnSelectedDaysChanged(int oldValue, int newValue)
        {
            if (IsBusy) return;

            ChartInterval = Math.Ceiling((double)newValue / 7.0);

            if (SelectedChartIndex == 0)
                _ = LoadSugarChartAsync();
            else
                _ = LoadBloodPressureChartAsync();
        }

        partial void OnSelectedChartIndexChanged(int value)
        {
            if (value == 0)
            {
                _ = LoadSugarChartAsync();
                Console.WriteLine($"[DEBUG] OnSelectedChartIndexChanged => {value}");
            }
            else if (value == 1)
            {
                _ = LoadBloodPressureChartAsync();
                Console.WriteLine($"[DEBUG] OnSelectedChartIndexChanged => {value}");
            }
        }

       

        private async Task LoadSugarChartAsync()
        {
            Console.Write("Wczytuje wykres cukru");
            if (IsBusy) return;

            IsBusy = true;
            ErrorMessage = string.Empty;

            try
            {
                if (_sugarBehavior != null) _sugarBehavior.SelectionChanged -= OnSelectionChanged;

                SugarEntries.Clear();
                ChartSeries.Clear();

                var token = await SecureStorage.GetAsync("auth_token");
                if (!string.IsNullOrEmpty(token))
                {
                    var jwt = new JwtSecurityTokenHandler().ReadJwtToken(token);
                    var givenName = jwt.Claims.FirstOrDefault(c => c.Type == "given_name")?.Value;
                    UserName = !string.IsNullOrEmpty(givenName) ? $"Zalogowano {givenName}" : "Zalogowano (brak imienia)";
                }
                var sugarList = await _apiService.GetAllSugarEntries(SelectedDays);
                ChartStartDate = DateTime.Now.AddDays(-SelectedDays);
                ChartEndDate = DateTime.Now;
                
                foreach(var s in sugarList)
                {
                    SugarEntries.Add(s);
                }


                ChartSeries.Clear();
                _sugarBehavior = new DataPointSelectionBehavior { Type = ChartSelectionType.Single };
                _sugarBehavior.SelectionChanged += OnSelectionChanged;

                _sugarSeries = new LineSeries
                {
                    ItemsSource = SugarEntries,
                    XBindingPath = nameof(SugarEntry.MealTime),
                    YBindingPath = nameof(SugarEntry.SugarValue),
                    Label = "Cukier",
                    StrokeWidth = 1,
                    MarkerSettings = new ChartMarkerSettings
                    {
                        Type = ShapeType.Circle,
                        Fill = new SolidColorBrush(Colors.Red),
                        Stroke = new SolidColorBrush(Colors.Black),
                        StrokeWidth = 1,
                        Width = 5,
                        Height = 5
                    },
                    SelectionBehavior = _sugarBehavior,
                    ShowDataLabels = true,
                    DataLabelSettings = new CartesianDataLabelSettings
                    {
                        LabelStyle = new ChartDataLabelStyle
                        {
                            TextColor = Colors.Black,
                            FontSize = 12
                        },
                        LabelPlacement = DataLabelPlacement.Inner
                    }
                };
                ChartSeries.Add(_sugarSeries);
                Console.WriteLine($"[DEBUG] Liczba serii w ChartSeries: {ChartSeries.Count}");
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
            }
            finally
            {
                IsBusy = false;
            }
        }

        private async Task LoadBloodPressureChartAsync()
        {
            Console.Write("Wczytuje wykres cisnienia");
            if (IsBusy) return;
            IsBusy = true;
            ErrorMessage = string.Empty;

            try
            {

                if (_stolicBehavior != null) _stolicBehavior.SelectionChanged -= OnSelectionChanged;
                if (_diastolicBehavior != null) _diastolicBehavior.SelectionChanged -= OnSelectionChanged;

                BloodPressureEntries.Clear();
                ChartSeries.Clear();

                var token = await SecureStorage.GetAsync("auth_token");
                if (!string.IsNullOrEmpty(token))
                {
                    var jwt = new JwtSecurityTokenHandler().ReadJwtToken(token);
                    var givenName = jwt.Claims.FirstOrDefault(c => c.Type == "given_name")?.Value;
                    UserName = !string.IsNullOrEmpty(givenName) ? $"Zalogowano {givenName}" : "Zalogowano (brak imienia)";
                }
                var bpList = await _apiService.GetBloodPressureEntries(SelectedDays);
                ChartStartDate = DateTime.Now.AddDays(-SelectedDays-1);
                ChartEndDate = DateTime.Now.AddDays(1);
                foreach (var bp in bpList)
                {
                    BloodPressureEntries.Add(bp);
                    Console.WriteLine($"[DEBUG] Dodano pomiar: {bp.MeasurementDate}, cisnienie: {bp.StolicPressure}/{bp.DiastolicPressure}");
                }
                Console.WriteLine($"[DEBUG] Liczba pobranych pomiarow cisnienia: {bpList.Count}");


                _stolicBehavior = new DataPointSelectionBehavior { Type = ChartSelectionType.Single };
                _stolicBehavior.SelectionChanged += OnSelectionChanged;

                _stolicSeries = new LineSeries
                {
                    ItemsSource = BloodPressureEntries,
                    XBindingPath = nameof(BloodPressureDto.MeasurementDate),
                    YBindingPath = nameof(BloodPressureDto.StolicPressure),
                    Label = "Cisnienie skurczowe",
                    MarkerSettings = new ChartMarkerSettings
                    {
                        Height = 8,
                        Width = 8
                    },
                    ShowDataLabels = true,
                    DataLabelSettings = new CartesianDataLabelSettings
                    {
                        LabelStyle = new ChartDataLabelStyle
                        {
                            TextColor = Colors.Black,
                            FontSize = 12
                        },
                        LabelPlacement = DataLabelPlacement.Outer
                    },
                    SelectionBehavior = _stolicBehavior,
                    EnableTooltip = true
                };

                _diastolicBehavior = new DataPointSelectionBehavior { Type = ChartSelectionType.Single };
                _diastolicBehavior.SelectionChanged += OnSelectionChanged;

                _diastolicSeries = new LineSeries
                {
                    ItemsSource = BloodPressureEntries,
                    XBindingPath = nameof(BloodPressureDto.MeasurementDate),
                    YBindingPath = nameof(BloodPressureDto.DiastolicPressure),
                    Label = "Cisnienie rozkurczowe",
                    MarkerSettings = new ChartMarkerSettings
                    {
                        Height = 8,
                        Width = 8
                    },
                    ShowDataLabels = true,
                    DataLabelSettings = new CartesianDataLabelSettings
                    {
                        LabelStyle = new ChartDataLabelStyle
                        {
                            TextColor = Colors.Black,
                            FontSize = 12
                        },
                        LabelPlacement = DataLabelPlacement.Outer
                    },
                    SelectionBehavior = _diastolicBehavior,
                    EnableTooltip = true
                };

                ChartSeries.Add(_stolicSeries);
                ChartSeries.Add(_diastolicSeries);
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
            }
            finally
            {
                IsBusy = false;
            }
        }

        private async void OnSelectionChanged(object sender, ChartSelectionChangedEventArgs e)
        {
            Console.WriteLine($"[DEBUG] OnSelectionChanged wywolane");
            if (e.NewIndexes.Count == 0) return;

            int idx = e.NewIndexes[0];
            Console.WriteLine($"[DEBUG] Wybrano indeks: {idx}");

            if (sender == _sugarBehavior)
            {
                if (_sugarSeries.ItemsSource is List<SugarEntry> sugarList && idx >= 0 && idx < sugarList.Count)
                {
                    var selected = sugarList[idx];
                    Console.WriteLine($"[DEBUG] Wybrano wpis: {selected.Id} - {selected.SugarValue} - {selected.MealTime} - {selected.MealMarker}");
                    var navigationParameter = new Dictionary<string, object> { { "Entry", selected } };
                    await Shell.Current.GoToAsync(nameof(EditSugarEntryPage), navigationParameter);
                }
            }
            else if (sender == _stolicBehavior)
            {
                if (_stolicSeries.ItemsSource is List<BloodPressureDto> bpList && e.NewIndexes.Count > 0)
                {
                    if (idx < bpList.Count)
                    {
                        var selected = bpList[idx];
                    }
                }
            }
            else if (sender == _diastolicBehavior)
            {
                if (_diastolicSeries.ItemsSource is List<BloodPressureDto> bpList && e.NewIndexes.Count > 0)
                {
                    if (idx < bpList.Count)
                    {
                        var selected = bpList[idx];
                    }
                }
            }
        }

    }
}
