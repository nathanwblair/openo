
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;


using MethodInfo = System.Reflection.MethodInfo;
using FieldInfo = System.Reflection.FieldInfo;
using Reflection = System.Reflection;



public static class Utility<T>
{
	static Utility()
	{
		Create = Expression.Lambda<System.Func<T>>(Expression.New(typeof(T).GetConstructor(System.Type.EmptyTypes))).Compile();
	}
	public static System.Func<T> Create { get; private set; }
}

public class An
{
	public static T Instance<T>(params object[] parms)
	{
		object instance = null;

		var constructorInfo = typeof(T).GetConstructor(The.Types(parms));
		if (constructorInfo != null)
		{
			if (parms.Length == 0)
				return Utility<T>.Create();
			else
				instance = constructorInfo.Invoke(parms);
		}
		else
		{
			instance = default(T);
		}

		return (T)instance;
	}
}

public class Empty
{

}

public class A
{
	public static T New<T>(params object[] parms)
	{
		return An.Instance<T>(parms);
	}

	public delegate T Make<T>(params object[] parms);
}

public class Make
{
	public class The
	{
		public static void Variable(FieldInfo variable, object parent, object value)
		{
			variable.SetValue(parent, value);
		}
	}
}

public class The
{
	private static Dictionary<string, System.Type> typeCache = new Dictionary<string, System.Type>();
	private static Dictionary<string, string> typeAliases = new Dictionary<string, string>();
	
	public static void Alias(string alias, System.Type type)
	{
		typeAliases.Add(alias, type.FullName);
	}

	static The()
	{
		Alias("string", typeof(string));
		Alias("float", typeof(float));
		Alias("double", typeof(double));
		Alias("int", typeof(int));
		Alias("bool", typeof(bool));
		Alias("long", typeof(long));
	}

	/// <summary>
	/// 
	/// </summary>
	/// <param name="typeName"></param>
	/// <param name="_depth">For internal function recursion</param>
	/// <returns></returns>
	public static System.Type Type(string typeName, int _depth=0)
	{
		System.Type t = null;

		if (_depth > 10)
			throw new System.Exception("Too deep bro");

		if (typeAliases.ContainsKey(typeName))
		{
			return Type(typeAliases[typeName], _depth + 1);
		}

		lock (typeCache)
		{
			if (!typeCache.TryGetValue(typeName, out t))
			{
				foreach (System.Reflection.Assembly a in System.AppDomain.CurrentDomain.GetAssemblies())
				{
					t = a.GetType(typeName);
					if (t != null)
						break;
				}
				if (t == null)
					return Type("System." + typeName, _depth=10);
				typeCache[typeName] = t; // perhaps null
			}
		}
		
		return t;
	}

	public static object Method(string name, System.Type type)
	{
		var info = type.GetMethod(name,
			System.Reflection.BindingFlags.Instance
				| System.Reflection.BindingFlags.NonPublic
				| System.Reflection.BindingFlags.Static
				| System.Reflection.BindingFlags.Public);

		return info;
	}


	// source: http://stackoverflow.com/a/8442803
	public static object Variable(string name, System.Type type)
	{
		return type.GetField(name, Reflection.BindingFlags.NonPublic | Reflection.BindingFlags.Instance);
	}

	public static object Value(FieldInfo variable, object parent)
	{
		return variable.GetValue(parent);
	}


	public static bool Same(System.Type a, System.Type b)
	{
		return (
					a.FullName == b.FullName)
					|| b.IsAssignableFrom(a)
					|| (a.IsAssignableFrom(b)
				);
	}

	public static bool Same(params System.Type[] types)
	{
		for (int i = 0; i < types.Length - 1; i++)
		{
			var isSame = false;
			for (int j = i + 1; j < types.Length; j++)
			{
				isSame = Same(types[i], types[j]);

				if (isSame)
					break;
			}

			if (!isSame)
				return false;
		}

		return true;
	}

	public static System.Type[] Types(params object[] objects)
	{
		var types = objects.Select(o => (Same(o.GetType(), typeof(System.Type)) ? (System.Type)o : o.GetType())).ToArray();

		return types;
	}

	public static bool Same(params object[] objects)
	{
		return Same(The.Types(objects));
	}

	public static object Result(object self, MethodInfo method, params object[] parms)
	{
		return method.Invoke(self, parms);
	}

	public static openo O(params object[] request)
	{
		return Openo(request);
	}

	public static openo Openo(params object[] request)
	{
		return new openo.tuple(request);
	}

	public static openo Openo(object request)
	{
		return new openo(request);
	}
}
