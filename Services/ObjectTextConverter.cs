using System.Reflection;

namespace lection4_hw.Services;


public class ObjectTextConverter<T> where T : class
{

    public ObjectTextConverter() {}

    public string Text(ICollection<T> collection)
    {
        string str = "";
        var props = typeof(T).GetProperties();
        foreach(var it in collection)
        {
            str += Text(it,props);
        }

        return str;
    }

    public string Text(T value, PropertyInfo[] infos) 
    {
        string str="";
        foreach(var info in infos)
        {
            if (info.PropertyType.IsValueType || info.PropertyType == typeof(string))
            str+= info.GetValue(value)+ " ";
        }
        return str+"\n";        
    }
}
