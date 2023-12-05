using System.Linq;
using System.ComponentModel;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using DXMauiApp.Data;

namespace DXMauiApp.ViewModels;

public class CarsViewModel : INotifyPropertyChanged
{
    private const int UnselectedIndex = -1;
    private static readonly IReadOnlyList<Car> allCars = new List<Car> {
            new Car("Mercedes-Benz", "SL500 Roadster"),
            new Car("Mercedes-Benz", "CLK55 AMG Cabriolet"),
            new Car("Mercedes-Benz", "C230 Kompressor Sport Coupe"),
            new Car("BMW", "530i"),
            new Car("Rolls-Royce", "Corniche"),
            new Car("Jaguar", "S-Type 3.0"),
            new Car("Cadillac", "Seville"),
            new Car("Cadillac", "DeVille"),
            new Car("Lexus", "LS430"),
            new Car("Lexus", "GS430"),
            new Car("Ford", "Ranger FX-4"),
            new Car("Dodge", "RAM 1500"),
            new Car("GMC", "Siera Quadrasteer"),
            new Car("Nissan", "Crew Cab SE"),
            new Car("Toyota", "Tacoma S-Runner"),
        };

    public IReadOnlyList<FilterTabViewModel<Car>> CarsByBrand { get; }

    int selectedIndex = UnselectedIndex;
    public int SelectedIndex
    {
        get => selectedIndex;
        set
        {
            if (selectedIndex == value) return;
            if (selectedIndex != UnselectedIndex)
            {
                CarsByBrand[selectedIndex].IsSelected = false;
            }
            selectedIndex = value;
            if (selectedIndex != UnselectedIndex)
            {
                CarsByBrand[selectedIndex].IsSelected = true;
            }
            RaisePropertyChanged();
        }
    }

    public event PropertyChangedEventHandler PropertyChanged;

    public CarsViewModel()
    {
        List<FilterTabViewModel<Car>> carBrandViewModels = new List<FilterTabViewModel<Car>>();
        carBrandViewModels.Add(new FilterTabViewModel<Car>("All", allCars));

        IEnumerable<IGrouping<string, Car>> groupedCarModels =
                                                    allCars.GroupBy(c => c.BrandName);
        foreach (IGrouping<string, Car> carModelGroup in groupedCarModels)
        {
            carBrandViewModels.Add(new FilterTabViewModel<Car>(carModelGroup.Key, carModelGroup));
        }
        CarsByBrand = carBrandViewModels;
    }

    private void RaisePropertyChanged([CallerMemberName] string caller = "")
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(caller));
    }
}
