
using System;
using System.IO;
using System.Collections;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Linq.Expressions;


/// <summary>
/// Reference Article http://www.codeproject.com/KB/tips/SerializedObjectCloner.aspx
/// Provides a method for performing a deep copy of an object.
/// Binary Serialization is used to perform the copy.
/// </summary>
public static class ObjectExtensions
{
	/// <summary>
	/// Perform a deep Copy of the object.
	/// </summary>
	/// <typeparam name="T">The type of object being copied.</typeparam>
	/// <param name="source">The object instance to copy.</param>
	/// <returns>The copied object.</returns>
	public static T Clone<T>(this T source)
	{
		if (!typeof(T).IsSerializable)
		{
			throw new ArgumentException("The type must be serializable.", "source");
		}

		// Don't serialize a null object, simply return the default for that object
		if (System.Object.ReferenceEquals(source, null))
		{
			return default(T);
		}

		IFormatter formatter = new BinaryFormatter();
		Stream stream = new MemoryStream();
		using (stream)
		{
			formatter.Serialize(stream, source);
			stream.Seek(0, SeekOrigin.Begin);
			return (T)formatter.Deserialize(stream);
		}
	}

	public static bool IsA<T>(this T source, string stringType)
	{
		return The.Same(source.GetType(), The.Type(stringType));

	}


	public static openo o(this object self, params object[] args)
	{
		if (args.Length > 1)
		{
			if (self is openo)
				return ((openo)self).Get(args);
			else
			{
				return (new openo((self)).Get(args));
			}
		}
		else if (args.Length == 1)
		{
			if (self is openo)
				return ((openo)self).Get(args[0]);
			else
			{
				return (new openo((self)).Get(args[0]));
			}
		}
		else
		{
			if (self is openo)
				return ((openo)self).Get();
			else
			{
				return (new openo((self)));
			}
		}
	}
	public static openo os(this object self, params object[] args)
	{
		if (args.Length > 1)
		{
			if (self is openo)
				return ((openo)self).FirstMatch(args);
			else
			{
				return (new openo((self)).FirstMatch(args));
			}
		}
		else if (args.Length == 1)
		{
			if (self is openo)
				return ((openo)self).Get(args[0]);
			else
			{
				return (new openo((self)).Get(args[0]));
			}
		}
		else
		{
			if (self is openo)
				return ((openo)self);
			else
			{
				return (new openo((self)));
			}
		}
	}
	
}

public static class Vector2Extensions
{

}


static class ArrayExtensions
{
	// create a subset from a range of indices
	public static T[] RangeSubset<T>(this T[] array, int startIndex, int length)
	{
		T[] subset = new T[length];
		Array.Copy(array, startIndex, subset, 0, length);
		return subset;
	}

	// create a subset from a specific list of indices
	public static T[] Subset<T>(this T[] array, params int[] indices)
	{
		T[] subset = new T[indices.Length];
		for (int i = 0; i < indices.Length; i++)
		{
			subset[i] = array[indices[i]];
		}
		return subset;
	}
}

public struct IntVector2
{
	public int x, y;

	public IntVector2(int[] raw)
	{
		this.x = raw[0];
		this.y = raw[1];
	}
}
