using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using AutoMapper;
using LtAmpDotNet.Lib;
using LtAmpDotNet.Lib.Model.Preset;
using LtAmpDotNet.Models;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace LtAmpDotNet.ViewModels;

public partial class MainViewModel : ViewModelBase
{
    public DspUnitLists DspUnitLists { get; set; }
    private IMapper _mapper;

    public event EventHandler<PropertyChangedEventArgs> PropertyChanged;

    private AmpStateModel _ampState;
    public AmpStateModel AmpState
    {
        get => _ampState;
        set => SetProperty(ref _ampState, value);
    }

    public List<string> AmpUnits => DspUnitLists.AmpUnits.Select(x => x.FenderId).ToList();

    public int SelectedAmpUnitIndex => DspUnitLists.AmpUnits.IndexOf(DspUnitLists.AmpUnits.SingleOrDefault(x => x.FenderId == AmpState.CurrentPreset.AmpUnit.FenderId));

    public MainViewModel(AmpStateModel ampState, IMapper mapper)
    {
        _mapper = mapper;
        DspUnitLists = new DspUnitLists(mapper);
        AmpState = ampState;
        AmpState.PropertyChanged += AmpState_PropertyChanged;
    }

    private void AmpState_PropertyChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e)
    {
        switch (e.PropertyName)
        {
            case "CurentPreset":
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(SelectedAmpUnitIndex)));
                break;
        }
    }
}
