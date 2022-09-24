using System;
using System.Collections.Generic;
using System.Reflection;

namespace _os
{
	public class _scaffold
	{
		// private properties
		private Object _o;
		private _info _i;
		private _menu _m;

		// public properties
		public _info _info
		{
			get {
				return this._i;
			}
		}
		public _menu _menu
		{
			get {
				return this._m;
			}
		}

		// constructor ,for implicit operation
		public _scaffold()
		{
			this._o = this;
			if (this._o != null)
			{
				// assiging initial & non-nullable properties
				this._i = new _info(this._o);
				this._m = new _menu();
			}
			else {
				throw new Exception("EXCEPTION: " + "Object not found");
			}
		}

		// constructor ,for explicit operation
		public _scaffold(Object _sentobject)
		{
			this._o = _sentobject;
			if (this._o != null)
			{
				// assiging initial & non-nullable properties
				this._i = new _info(this._o);
				this._m = new _menu();
			}
			else {
				throw new Exception("EXCEPTION: " + "Object not found");
			}
		}
	}

	public class _info
	{
		// private properties
		private Assembly _a;
		private Type? _t;
		private string _n;

		// public properties
		public Assembly _assembly
		{
			get { 
				return this._a;
			}
		}
		public Type? _type
		{
			get {
				return this._t;
			}
		}
		public string _originalname
		{
			get {
				return this._n;
			}
		}
		public string _beautyname
		{
			get
			{
				string _message = string.Empty;
				if (!String.IsNullOrEmpty(this._n))
				{
					if (this._n.Length > 0 && this._n[0] == '_') {
						_message = this._n.Substring(1);
					}
					else {
						_message = this._n;
					}
				}
				return _message;
			}
		}

		// constructor ,for explicit operation
		public _info(Object _sentobject)
		{
			this._t = typeof(Object);

			if (_sentobject.GetType() != null)
			{
				// assiging initial & non-nullable properties
				this._a = this._t?.Assembly ?? new Object().GetType().Assembly ;
				this._t = _sentobject.GetType();
				this._n = this._t?.Namespace ?? string.Empty;
			}
			else {
				throw new Exception("EXCEPTION: " + "Object not found");
			}
		}
	}

	public class _menu
	{
		// public properties
		public _set _base;

		// constructor ,for implicit operation
		public _menu()
		{
			// assiging initial & non-nullable properties
			this._base = new _os._menu._set();
		}

		// constructor ,for explicit operation
		public _menu(_set _base)
		{
			// assiging initial & non-nullable properties
			this._base = _base;
		}

		public class _set
		{
			// public properties
			public List<_value> _values;

			// constructor ,for implicit operation
			public _set()
			{
				// assiging initial & non-nullable properties
				this._values = new List<_value>() {};
			}

			// constructor ,for explicit operation
			public _set(List<_value> _values)
			{
				// assiging initial & non-nullable properties
				this._values = _values;
			}
		}

		public class _value
		{
			// private properties
			private _accessortype? _a;
			private string? _tk;
			private string? _d;
			private Action? _act;
			private _set? _ns;


			// public properties
			public enum _accessortype: byte { service, console, ui };
			public _accessortype? _accessor
			{
				get {
					return this._a;
				}
			}
			public string? _triggeringkey
			{
				get {
					return this._tk;
				}
			}
			public string? _description
			{
				get {
					return this._d;
				}
			}
			public Action? _action
			{
				get {
					return this._act;
				}
			}
			public _set? _nextset
			{
				get {
					return this._ns;
				}
			}

			// constructor ,for implicit operation
			public _value()
			{
				// assiging initial & non-nullable properties
			}

			// constructor ,for explicit operation
			public _value(_accessortype? _accessor, string? _triggeringkey, string? _description, Action? _action, _set? _nextset)
			{
				// assiging initial & non-nullable properties
				this._a = _accessor;
				this._tk = _triggeringkey;
				this._d = _description;
				this._act = _action;
				this._ns = _nextset;
			}
		}
	}
}