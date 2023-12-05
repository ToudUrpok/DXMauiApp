namespace DXMauiApp.Data;

public class Car
{
    public string BrandName { get; }
    public string ModelName { get; }
    public string FullName => $"{BrandName} {ModelName}";

    public Car(string brand, string model)
    {
        this.BrandName = brand;
        this.ModelName = model;
    }
}
