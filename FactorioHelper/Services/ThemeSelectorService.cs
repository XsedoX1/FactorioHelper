﻿using FactorioHelper.Helpers;
using FactorioHelper.Interfaces.Services;
using Microsoft.UI.Xaml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactorioHelper.Services
{
    public class ThemeSelectorService : IThemeSelectorService
    {
        private const string SettingsKey = "AppBackgroundRequestedTheme";

        public ElementTheme Theme { set; get; } = ElementTheme.Default;

        private readonly ILocalSettingsService _localSettingsService;

        public ThemeSelectorService(ILocalSettingsService localSettingsService)
        {
            _localSettingsService=localSettingsService;
        }

        public async Task InitializeAsync()
        {
            Theme = await LoadThemeFromSettingsAsync();
            await Task.CompletedTask;
        }

        public async Task SetThemeAsync(ElementTheme theme)
        {
            Theme = theme;

            await SetRequestedThemeAsync();
        }

        public async Task SetRequestedThemeAsync()
        {
            if(App.MainWindow.Content is FrameworkElement rootElement)
            {
                rootElement.RequestedTheme = Theme;
                TitleBarHelper.UpdateTitleBar(Theme);

            }
            await Task.CompletedTask;
        }

        private async Task<ElementTheme> LoadThemeFromSettingsAsync()
        {
            var themeName = await _localSettingsService.ReadSettingAsync<string>(SettingsKey);

            if(Enum.TryParse(themeName, out ElementTheme cacheTheme))
            {
                return cacheTheme;
            }

            return ElementTheme.Default;
        }

        private async Task SaveThemeInSettingsAsync(ElementTheme theme)
        {
            await _localSettingsService.SaveSettingAsync(SettingsKey, theme.ToString());
        }
    }
}