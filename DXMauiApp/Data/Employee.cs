using System.Reflection;

namespace DXMauiApp.Data;

public enum AccessLevel
{
    Admin,
    User
}

public class Employee
{
    string name;
    string resourceName;

    public string Name
    {
        get { return name; }
        set
        {
            name = value;
            if (Photo is null)
            {
                //resourceName = "Images." + value.ToLower().Replace(" ", "_") + ".jpg";
                //if (!String.IsNullOrEmpty(resourceName))
                //    Photo = ImageSource.FromResource(resourceName);
                resourceName = value.ToLower().Replace(" ", "_") + ".jpg";
                if (!String.IsNullOrEmpty(resourceName))
                    Photo = ImageSource.FromFile(resourceName);
            }
        }
    }

    public Employee(string name)
    {
        this.Name = name;
    }
    public ImageSource Photo { get; set; }
    public DateTime BirthDate { get; set; }
    public DateTime HireDate { get; set; }
    public string Position { get; set; }
    public string Address { get; set; }
    public string Phone { get; set; }
    public AccessLevel Access { get; set; }
    public bool OnVacation { get; set; }
}
