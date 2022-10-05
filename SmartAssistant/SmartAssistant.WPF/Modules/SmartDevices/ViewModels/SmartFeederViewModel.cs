using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using SmartAssistant.WPF.Core;
using System;
using System.Windows.Threading;

namespace SmartAssistant.WPF.Modules.SmartDevices.ViewModels;

public class SmartFeederViewModel : BindableBase, INavigationAware
{
    private readonly IRegionManager _regionManager;

    // Bizoux properties
    private int _bizouxHunger = 40;
    public int BizouxHunger
    {
        get { return _bizouxHunger; }
        set 
        { 
            SetProperty(ref _bizouxHunger, value);
            EatAndDringIfNeeded();
        }
    }

    private int _bizouxThirst = 50;
    public int BizouxThirst
    {
        get { return _bizouxThirst; }
        set 
        { 
            SetProperty(ref _bizouxThirst, value);
            EatAndDringIfNeeded();
        }
    }

    // Zizzi properties
    private int _zizziHunger = 60;
    public int ZizziHunger
    {
        get { return _zizziHunger; }
        set 
        { 
            SetProperty(ref _zizziHunger, value); 
        EatAndDringIfNeeded();}

    }

    private int _zizziThirst = 30;
    public int ZizziThirst
    {
        get { return _zizziThirst; }
        set 
        { 
            SetProperty(ref _zizziThirst, value);
            EatAndDringIfNeeded();
        }

    }

    // Food and water properties
    private int _foodQuantity = 80;
    public int FoodQuantity
    {
        get { return _foodQuantity; }
        set 
        { 
            SetProperty(ref _foodQuantity, value);
            RefillFoodAndWaterIfNeeded();
        }
    }

    private int _waterQuantity = 80;
    public int WaterQuantity
    {
        get { return _waterQuantity; }
        set 
        { 
            SetProperty(ref _waterQuantity, value);
            RefillFoodAndWaterIfNeeded();
        }
    }

    public DelegateCommand GoToSmartDevicesMenuCommand { get; private set; }

    private Random _random;
    public SmartFeederViewModel(IRegionManager regionManager)
    {
        GoToSmartDevicesMenuCommand = new DelegateCommand(GoToSmartDevicesMenu);
        _regionManager = regionManager;

        _random = new Random(DateTime.Now.Millisecond);
        DispatcherTimer increaseBizouxHungrinessAndThirst = new DispatcherTimer();
        increaseBizouxHungrinessAndThirst.Tick += IncreaseBizouxHungrinessAndThirst;
        increaseBizouxHungrinessAndThirst.Interval = TimeSpan.FromSeconds(3);
        increaseBizouxHungrinessAndThirst.Start();

        DispatcherTimer increaseZizziHungrinessAndThirst = new DispatcherTimer();
        increaseZizziHungrinessAndThirst.Tick += IncreaseZizziHungrinessAndThirst;
        increaseZizziHungrinessAndThirst.Interval = TimeSpan.FromSeconds(4);
        increaseZizziHungrinessAndThirst.Start();
    }

    private void IncreaseBizouxHungrinessAndThirst(object sender, EventArgs e)
    {
        var tempHunger = _random.Next(10, 20);
        var tempThirst = _random.Next(10, 20);

        if (BizouxHunger + tempHunger <= 100)
        {
            BizouxHunger += tempHunger;
        }
        else
        {
           BizouxHunger = 100;
        }

        BizouxThirst += _random.Next(10, 20);
        if (BizouxThirst + tempThirst <= 100)
        {
            BizouxThirst += tempThirst;
        }
        else
        {
            BizouxThirst = 100;
        }
    }

    private void IncreaseZizziHungrinessAndThirst(object sender, EventArgs e)
    {
        var tempHunger = _random.Next(10, 20);
        var tempThirst = _random.Next(10, 20);

        if (ZizziHunger + tempHunger <= 100)
        {
            ZizziHunger += tempHunger;
        }
        else
        {
            ZizziHunger = 100;
        }
        
        ZizziThirst += _random.Next(10, 20);
        if (ZizziThirst + tempThirst <= 100)
        {
            ZizziThirst += tempThirst;
        }
        else
        {
            ZizziThirst = 100;
        }
    }

    private void RefillFoodAndWaterIfNeeded()
    {
        if (FoodQuantity <= 20)
        {
            FoodQuantity = 100;
        }
        if (WaterQuantity <= 20)
        {
            WaterQuantity = 100;
        }
    }

    private void EatAndDringIfNeeded()
    {
        if (BizouxHunger >= 80)
        {
            BizouxHunger -= 30;
            FoodQuantity -= 30;
        }
        if (BizouxThirst >= 80)
        {
            BizouxThirst -= 30;
            WaterQuantity -= 30;
        }

        if (ZizziHunger >= 80)
        {
            ZizziHunger -= 30;
            FoodQuantity -= 30;
        }
        if (ZizziThirst >= 80)
        {
            ZizziThirst -= 30;
            WaterQuantity -= 30;
        }
    }

    private void GoToSmartDevicesMenu()
    {
        _regionManager.RequestNavigate(RegionNames.MainContentRegion, "SmartDevicesView");
    }

    public bool IsNavigationTarget(NavigationContext navigationContext)
    {
        return false;
    }

    public void OnNavigatedFrom(NavigationContext navigationContext)
    {

    }

    public void OnNavigatedTo(NavigationContext navigationContext)
    {

    }
}