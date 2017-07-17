using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Management;
using System.Text;

abstract class BaseWin32Entity
{
    public Dictionary<string, object> Properties { get; set; }
    public string Caption { get; protected set; }
    public string Name { get; protected set; }
    public string Manufacturer { get; protected set; }
    public string Model { get; protected set; }
    public string Description { get; protected set; }

    public BaseWin32Entity(ManagementBaseObject obj)
    {
        this.Properties = new Dictionary<string, object>();
        foreach (var p in obj.Properties)
            this.Properties.Add(p.Name, p.Value);

        this.Caption = ParseValue<string>(obj, "Caption");
        this.Name = ParseValue<string>(obj, "Name");
        this.Manufacturer = ParseValue<string>(obj, "Manufacturer");
        this.Model = ParseValue<string>(obj, "Model");
        this.Description = ParseValue<string>(obj, "Description");

        if (!string.IsNullOrEmpty(this.Caption))
            this.Caption = this.Caption.ToLower();

        if (!string.IsNullOrEmpty(this.Name))
            this.Name = this.Name.ToLower();

        if (!string.IsNullOrEmpty(this.Manufacturer))
            this.Manufacturer = this.Manufacturer.ToLower();

        if (!string.IsNullOrEmpty(this.Model))
            this.Model = this.Model.ToLower();

        if (!string.IsNullOrEmpty(this.Description))
            this.Description = this.Description.ToLower();
    }

    public override string ToString()
    {
        return string.Format("manufacturer={0}, name={1}, model={2}, caption={3}, description={4}"
            , this.Manufacturer
            , this.Name
            , this.Model
            , this.Caption
            , this.Description);
    }

    protected string PrintProperties()
    {
        var sb = new StringBuilder();
        foreach (var key in this.Properties.Keys)
            sb.AppendLine(string.Format("{0} = {1}", key, GetValue(this.Properties[key])));

        return sb.ToString();
    }

    object GetValue(object value)
    {
        var valueIsArray = false;

        if (value == null)
            return string.Empty;

        valueIsArray = value.GetType().IsArray;

        if (valueIsArray)
            return ToJSON(value);
        else
            return value;
    }

    protected string ToJSON(object value)
    {
        return JsonConvert.SerializeObject(value);
    }

    protected T ParseValue<T>(ManagementBaseObject obj, string key)
    {
        object value = null;
        try
        {
            value = obj[key];
        }
        catch { }

        if (value == null)
        {
            if (typeof(T) == typeof(string))
                return (T)Convert.ChangeType(string.Empty, typeof(T));
            else
                return default(T);
        }

        return (T)Convert.ChangeType(value, typeof(T));
    }
}
