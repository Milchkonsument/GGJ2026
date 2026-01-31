using System.Linq;
using Unity.VisualScripting;

public static class Extensions
{
    public static T GetRandomElement<T>(this T[] array)
    {
        if (array == null || array.Length == 0)
            return default;

        int index = UnityEngine.Random.Range(0, array.Length);
        return array[index];
    }

    public static T GetRandomElement<T>(this System.Collections.Generic.IEnumerable<T> list)
    {
        if (list == null || list.Count() == 0)
            return default;

        int index = UnityEngine.Random.Range(0, list.Count());
        return list.ElementAt(index);
    }
}