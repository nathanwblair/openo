
using System.Collections;
using System.Collections.Generic;

using MethodInfo = System.Reflection.MethodInfo;

/// <summary>
/// open-object: a wrapper to peel open a .NET object, 
/// revealing its members as strings with reflection
/// </summary
[System.Serializable]
public class openo
{
	public object value;

	/// <summary>
	/// open-function!
	/// </summary>
	[System.Serializable]
	public class f : openo
	{
		public openo parent;
        static object EmptyRequest = new object();

		public f(string name, openo parent)
			: base(
				  The.Method(
					  name,
					  parent.Type()
					  ))
		{
			this.parent = parent;
		}

		public f(string name, object function, openo parent)
			: base(function)
		{
			this.parent = parent;
        }

        public override openo Get(object request)
        {
            object result;

            if (request is object[])
                result = The.Result(this.parent.value, (MethodInfo)this.value, (object[])request);
            else
                result = The.Result(this.parent.value, (MethodInfo)this.value, request);

            return new openo(result);
        }

        public override openo Get()
        {
            object result = The.Result(this.parent.value, (MethodInfo)this.value, new object[] { });

            return new openo(result);
        }
    }   /// <summary>
		/// open-function!
		/// </summary>
	[System.Serializable]
	public class v : openo
	{
		public object variable;
		public openo parent;

		public v(string name, openo parent)
			: base(null)
		{
			this.parent = parent;

			this.variable = The.Variable(name, this.parent.Type());
			this.value = The.Value((System.Reflection.FieldInfo)this.variable, this.parent.value);

		}

		public v(object variable, openo parent)
			: base(The.Value((System.Reflection.FieldInfo)variable, parent.value))
		{
			this.parent = parent;
			this.variable = variable;
		}

		public override void Set(object request)
		{
			Make.The.Variable((System.Reflection.FieldInfo)this.value, this.parent.value, request);
		}
	}

	[System.Serializable]
	public class tuple : openo
	{
		public tuple(params object[] values) : base(values)
		{
		}
	}

	/// <summary>
	/// Credit: http://stackoverflow.com/a/11065781
	/// </summary>
	public static implicit operator openo(object[] request)
	{
		return new tuple(request);
	}

	/// <summary>
	/// Credit: http://stackoverflow.com/a/11065781
	/// </summary>
	public static implicit operator openo(string request)
	{
		return new openo(request);
	}

	public static implicit operator string(openo o)
	{
		return o.value.ToString();
	}

	public static implicit operator int(openo o)
	{
		return System.Convert.ToInt32(o.value);
	}

	public static implicit operator float(openo o)
	{
		return System.Convert.ToSingle(o.value);
	}

	public virtual void Set(object request)
	{
		// do nothing
	}

	public openo(object value)
	{
		this.value = value;
	}

	public T As<T>()
	{
		return (T)this.value;
	}

	/// <summary>
	/// reveal! (with this.Get)
	/// </summary>
	public static openo O(openo request)
	{
		return new openo(request);
	}

	public virtual openo Get(object[] request)
	{
		return this.Get((object)request);
	}

	public virtual T Get<T>()
	{
		return (T)this.value;
	}

	public virtual openo Get()
	{
		return this; // by default
	}

	public virtual openo FirstMatch(object[] requests)
	{
		openo result;

		foreach (var r in requests)
		{
			result = Get(r);
			if (result != null)
				return result;
		}
		return null;
	}

	public virtual openo Get(object request)
	{
		openo result = null;


		if (request is string)
		{
			var strRequest = (string)request;

			result = this.TryGetVar(strRequest);
			if (result != null)
				return result;

			result = this.TryGetFunction(strRequest);
			if (result != null)
				return result;

			int intRequest;

			if (int.TryParse(strRequest, out intRequest))
			{
				result = this.TryGetIndex(intRequest);
				if (result != null)
					return result;
			}
		}
		else if (request is int)
		{
			result = this.TryGetIndex((int)request);
			if (result != null)
				return result;
		}

		return null;
	}

	public virtual openo Get(openo request)
	{
		return Get(request.value);
	}

	private openo TryGetIndex(int index)
	{
		IList col = this.value as IList;

		if (col == null)
			return this;

		return new openo(col[index]);
	}

	public int Length()
	{
		ICollection col = this.value as ICollection;
		if (col != null)
			return col.Count;
		else
			return 1;
	}

	private openo TryGetFunction(string name)
	{
		var function = The.Method(name, this.value.GetType());

		if (function != null)
		{
			return new f(name, function, this);
		}

		return null;
	}

	private openo TryGetVar(string name)
	{
		var variable = The.Variable(name, this.value.GetType());

		if (variable != null)
		{
			return new v(variable, this);
		}

		return null;
	}

	public System.Type Type()
	{
		return this.value.GetType();
	}

	//public f Method(string name)
	//{
	//
	//}

	bool IsA(object o)
	{
		return The.Same(this, o);
	}

	bool IsA(string type)
	{
		return this.IsA(type.GetType());
	}


}

