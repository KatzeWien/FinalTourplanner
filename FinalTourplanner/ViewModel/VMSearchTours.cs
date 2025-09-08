using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using FinalTourplanner.Models;
using FinalTourplanner.Commands;
using System.Collections.ObjectModel;
using System.Runtime.CompilerServices;

namespace FinalTourplanner.ViewModel
{
    public class VMSearchTours : INotifyPropertyChanged
    {
        private readonly AllDataManagement _management;
        private string _searchText = "";
        private bool _isSearching;
        private CancellationTokenSource? _cts;
        private Tour? _selected;

        // direkt an AllDataManagement.SearchResults
        public ObservableCollection<Tour> Results => _management.SearchResults;

        public string SearchText
        {
            get => _searchText;
            set
            {
                if (Set(ref _searchText, value))
                    _ = DebouncedSearchAsync();
            }
        }

        public bool IsSearching
        {
            get => _isSearching;
            private set => Set(ref _isSearching, value);
        }

        public Tour? SelectedTour
        {
            get => _selected;
            set => Set(ref _selected, value);
        }


        public ICommand ClearCommand { get; }
        public ICommand OpenDetailsCommand { get; }

        public event PropertyChangedEventHandler? PropertyChanged;

        public VMSearchTours(AllDataManagement management)
        {
            _management = management;

            // 1-Argument-Command weil einfacher

            ClearCommand = new Commands.Command(_ => SearchText = "");
            OpenDetailsCommand = new Commands.Command(_ => OpenDetails());

            _ = DebouncedSearchAsync(immediate: true);
        }

        private async Task DebouncedSearchAsync(bool immediate = false)
        {
            _cts?.Cancel();
            _cts = new CancellationTokenSource();
            var token = _cts.Token;

            try
            {
                if (!immediate)
                    await Task.Delay(250, token); // delay zum suche machen

                IsSearching = true;
                _management.SearchToursByName(SearchText);
            }
            catch (TaskCanceledException)
            {
                //
            }
            finally
            {
                if (!token.IsCancellationRequested)
                    IsSearching = false;
            }
        }

        private void OpenDetails()
        {
            if (SelectedTour is null) return;

            var win = new View.TourDetails(_management, SelectedTour);
            var ok = win.ShowDialog();
            if (ok == true)
                _ = DebouncedSearchAsync(immediate: true); // nach neuer eingabeänderung laden
        }

        private bool Set<T>(ref T field, T value, [CallerMemberName] string? prop = null)
        {
            if (EqualityComparer<T>.Default.Equals(field, value)) return false;
            field = value;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
            return true;
        }
    }
}
