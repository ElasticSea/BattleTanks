using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using UnityEngine;

namespace System
{
}

namespace _Framework.Scripts.Extensions
{
	public static class OtherExtensions
	{
        /// <summary>
        /// Sets the stream's length to 0
        /// </summary>
        public static void Reset(this MemoryStream memory)
        {
            memory.SetLength(0);
        }

        /// <summary>
        /// http://stackoverflow.com/questions/4108828/generic-extension-method-to-see-if-an-enum-contains-a-flag
        /// </summary>
        public static bool HasFlag(this Enum variable, Enum value)
        {
            if (variable == null)
                return false;

            if (value == null)
                throw new ArgumentNullException("value");

            // Not as good as the .NET 4 version of this function, but should be good enough
            if (!Enum.IsDefined(variable.GetType(), value))
            {
                throw new ArgumentException(string.Format(
                    "Enumeration type mismatch.  The flag is of type '{0}', was expecting '{1}'.",
                    value.GetType(), variable.GetType()));
            }

            ulong num = Convert.ToUInt64(value);
            return ((Convert.ToUInt64(variable) & num) == num);
        }

        public static bool TryReadLine(this StreamReader reader, out string line)
        {
            line = reader.ReadLine();
            return line != null;
		}

		/// <summary>
		/// Returns the value at the specified key in the dictionary if the key exists,
		/// otherwise the default value of the type 'V'
		/// </summary>
		public static V ValueOrDefault<K, V>(this Dictionary<K, V> dictionary, K key)
		{
		    return Equals(key, default(K)) ? default(V) : ValueOrDefault(dictionary, key, default(V));
		}

		/// <summary>
		/// Returns the value at the specified key in the dictionary if the key exists,
		/// otherwise the specified default value
		/// </summary>
		public static V ValueOrDefault<K, V>(this Dictionary<K, V> dictionary, K key, V defaultValue)
		{
			V result;
			if (dictionary.TryGetValue(key, out result))
				return result;
			return defaultValue;
		}

		public static void WriteAllLines(this StreamWriter writer, string[] lines)
		{
			for (int i = 0; i < lines.Length; i++)
				writer.WriteLine(lines[i]);
		}

		public static void WriteAtLine(this StreamWriter writer, string[] lines, int lineIdx, string value)
		{
			for (int i = 0; i < lines.Length; i++)
			{
				if (i == lineIdx)
					writer.WriteLine(value);
				else
					writer.WriteLine(lines[i]);
			}
		}

		public static float Sqr(this float value)
		{
			return value * value;
		}

		/// <summary>
		/// Ex: [true, true, false, false] ->
		///	  [1, 1, 0, 0] ->
		///	  12 (in decimal)
		/// </summary>
		public static int ToInt(this IEnumerable<bool> input)
		{
			var builder = new StringBuilder();
			foreach (var x in input)
				builder.Append(Convert.ToByte(x)); // true -> 1, false -> 0
			return Convert.ToInt32(builder.ToString(), 2); // 2 is the base - cause we're converting from base 2 (binary)
		}

		/// <summary>
		/// Credits: http://stackoverflow.com/questions/4448063/how-can-i-convert-an-int-to-an-array-of-bool
		/// </summary>
		public static bool[] ToBooleanArray(this int input)
		{
			return Convert.ToString(input, 2).Select(s => s.Equals('1')).ToArray();
		}

		/// <summary>
		/// Returns true if this object's current value is between (greater or equal to) 'from' and (less than or equal to) 'to'
		/// Credits: http://extensionmethod.net/csharp/type/between
		/// </summary>
		public static bool Between<T>(this T value, T from, T to) where T : IComparable<T>
		{
			return value.CompareTo(from) >= 0 && value.CompareTo(to) <= 0;
		}

		/// <summary>
		/// Sets the builder's length to zero and returns it
		/// </summary>
		public static StringBuilder Clear(this StringBuilder builder)
		{
			builder.Length = 0;
			return builder;
	    }

	    public static Rect ToUvRect(this Sprite sprite)
	    {
	        var uv = sprite.rect;
	        uv.x /= sprite.texture.width;
	        uv.width /= sprite.texture.width;
	        uv.y /= sprite.texture.height;
	        uv.height /= sprite.texture.height;
	        return uv;
	    }

	    public static Vector2[] ToUvs(this Sprite sprite)
	    {
	        var rect = sprite.ToUvRect();
	        return new[]
	        {
	            new Vector2(rect.xMin, rect.yMin),
	            new Vector2(rect.xMax, rect.yMin),
	            new Vector2(rect.xMax, rect.yMax),
	            new Vector2(rect.xMin, rect.yMax)
	        };
	    }

	    public static StringBuilder AppendLineIf(this StringBuilder builder, bool @if, string line)
	    {
	        if (@if) builder.AppendLine(line);
	        return builder;
	    }

	    public static Bounds CreateBoxFromBounds(this Bounds bounds)
	    {
	        var cutBox = new Bounds(bounds.center, bounds.size);
	        if (cutBox.size.y > Mathf.Max(cutBox.size.x, cutBox.size.z))
	        {
	            cutBox.min = cutBox.min.SetY(cutBox.max.y - Mathf.Max(cutBox.size.x, cutBox.size.z));
	        }
	        return cutBox;
	    }

	    public static string ToRoman(this int number)
	    {
	        var romanNumerals = new[]
	        {
	            new[] {"", "I", "II", "III", "IV", "V", "VI", "VII", "VIII", "IX"}, // ones
	            new[] {"", "X", "XX", "XXX", "XL", "L", "LX", "LXX", "LXXX", "XC"}, // tens
	            new[] {"", "C", "CC", "CCC", "CD", "D", "DC", "DCC", "DCCC", "CM"}, // hundreds
	            new[] {"", "M", "MM", "MMM"} // thousands
	        };

	        // split integer string into array and reverse array
	        var intArr = number.ToString().Reverse().ToArray();
	        var len = intArr.Length;
	        var romanNumeral = "";
	        var i = len;

	        // starting with the highest place (for 3046, it would be the thousands
	        // place, or 3), get the roman numeral representation for that place
	        // and add it to the final roman numeral string
	        while (i-- > 0)
	        {
	            romanNumeral += romanNumerals[i][int.Parse(intArr[i].ToString())];
	        }

	        return romanNumeral;
	    }

	    public static Vector2 ToVector2(this Resolution resolution) => new Vector2(resolution.width, resolution.height);

	    public static bool DynamicEquals<T>(this T value, T other)
	    {
	        if (Equals(value, other))
	        {
	            return true;
	        }

	        if (Equals(value, default(T)) || Equals(other, default(T)))
	        {
	            return false;
	        }

	        if (value is IEnumerable && other is IEnumerable && (other is string == false) && (other is Array == false))
	        {
	            var t = typeof(Enumerable);
	            var meth = t.GetMethods(BindingFlags.Public | BindingFlags.Static)
	                .Single(m => m.Name.Equals("SequenceEqual") && m.GetParameters().Count() == 2);
	            var gm = meth.MakeGenericMethod(typeof(T).GetGenericArguments().Single());
	            return (bool)gm.Invoke(null, new object[] { value, other });
	        }

	        return value.Equals(other);
	    }
    }
}
