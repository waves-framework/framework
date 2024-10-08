﻿using Waves.Framework.Attributes;
using Waves.Framework.UI.WPF.Controls;
using Waves.Sandbox;
using Waves.Sandbox.ViewModels.Navigation.Window;

namespace Waves.Framework.Sandbox.WPF.Desktop.Views.Navigation.Window;

[WavesView(typeof(WindowNavigationWithResultWindowViewModel), region: Regions.WindowNavigationWithParameterWithResult)]
public partial class PageNavigationWithResultWindowView : WavesWindow
{
    public PageNavigationWithResultWindowView()
    {
        InitializeComponent();
    }
}