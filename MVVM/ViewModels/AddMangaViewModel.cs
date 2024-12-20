﻿using ReadLog.Core;
using ReadLog.MVVM.Models;
using ReadLog.Services;
using ReadLog.Stores;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ReadLog.MVVM.ViewModels
{
    public class AddMangaViewModel : ViewModelBase
    {

        public MessageViewModel ErrorMessage { get; }
        public MessageViewModel SuccessMessage { get; }

        public RelayCommand AddMangaCommand { get; set; }

        private string _mangaName;
        public string MangaName
        {
            get
            {
                return _mangaName;
            }
            set
            {
                _mangaName = value;
                OnPropertyChanged(nameof(MangaName));
            }
        }

        private string _numberChapiter;
        public string NumberChapiter
        {
            get
            {
                return _numberChapiter;
            }
            set
            {
                _numberChapiter = value;
                OnPropertyChanged(nameof(NumberChapiter));
            }
        }

        private bool _isFavorite;
        public bool IsFavorite
        {
            get
            {
                return _isFavorite;
            }
            set
            {
                _isFavorite = value;
                OnPropertyChanged(nameof(IsFavorite));
            }
        }

        private readonly IMangaApiClient _mangaApiClient;
        public AddMangaViewModel(INavigationService navigationService, DataStore<Manga> dataStore, IMangaApiClient mangaApiClient) : base(navigationService, dataStore)
        {
            _mangaApiClient = mangaApiClient;
            ErrorMessage = new MessageViewModel(navigationService, dataStore);
            SuccessMessage = new MessageViewModel(navigationService, dataStore);
            AddMangaCommand = new RelayCommand(async execute => await AddManga(), canExecute => checkBoxes());
        }
        private bool checkBoxes() => !string.IsNullOrEmpty(_mangaName);
        public void resetUI()
        {
            IsFavorite = false;
            MangaName = "";
            NumberChapiter = "0";

            ErrorMessage.ClearMessage();
            SuccessMessage.ClearMessage();
        }

        private async Task AddManga()
        {

            ErrorMessage.ClearMessage();
            SuccessMessage.ClearMessage();
            try
            {
                Manga newManga = await _mangaApiClient.GetMangaByName(_mangaName);
                if (newManga != null)
                {
                    newManga.IsFavorite = IsFavorite;
                    newManga.NombreChapitresLus = NumberChapiter;
                    if (string.IsNullOrEmpty(newManga.NombreChapitresLus))
                        newManga.NombreChapitresLus = "0";

                    Debug.WriteLine(newManga.ToString());
                    await _dataStore.AddDataAsync(newManga);
                    SuccessMessage.DisplayMessage($"{MangaName} found and added!");
                }
                else
                {
                    throw new MangaNotFoundException($"'{MangaName}' doesn't exist!");
                }
            }
            catch (CustomExceptionBase ex)
            {
                ErrorMessage.DisplayMessage(ex);
            }

        }

    }
}
