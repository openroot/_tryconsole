using System;
using System.Collections.Generic;
using System.Reflection;

namespace _os
{
	public class _osscaffold
	{
		// private properties
		private Object _o;
		private _osinfo _i;
		private _osmenu _m;

		// public properties
		public _osinfo _info
		{
			get {
				return this._i;
			}
		}
		public _osmenu _menu
		{
			get {
				return this._m;
			}
		}

		// constructor ,for implicit operation
		public _osscaffold()
		{
			this._o = this;
			if (this._o != null)
			{
				// assiging initial & non-nullable properties
				this._i = new _osinfo(this._o);
				this._m = new _osmenu();
			}
			else {
				throw new Exception("EXCEPTION: " + "Object not found");
			}
		}

		// constructor ,for explicit operation
		public _osscaffold(Object _sentobject)
		{
			this._o = _sentobject;
			if (this._o != null)
			{
				// assiging initial & non-nullable properties
				this._i = new _osinfo(this._o);
				this._m = new _osmenu();
			}
			else {
				throw new Exception("EXCEPTION: " + "Object not found");
			}
		}
	}

	public class _osinfo
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
		public _osinfo(Object _sentobject)
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

	public class _osmenu
	{
		// public properties
		public _menuset _menus;

		// constructor ,for implicit operation
		public _osmenu()
		{
			// assiging initial & non-nullable properties
			this._menus = new _osmenu._menuset();
		}

		// constructor ,for explicit operation
		public _osmenu(_menuset _menubase)
		{
			// assiging initial & non-nullable properties
			this._menus = _menubase;
		}

		public class _menuset
		{
			// public properties
			public List<_menu> _set;

			// constructor ,for implicit operation
			public _menuset()
			{
				// assiging initial & non-nullable properties
				this._set = new List<_menu>() {};
			}

			// constructor ,for explicit operation
			public _menuset(List<_menu> _menus)
			{
				// assiging initial & non-nullable properties
				this._set = _menus;
			}
		}

		public class _menu
		{
			// private properties
			private _accessortype? _a;
			private string? _tk;
			private string? _d;
			private Action? _act;
			private _menuset? _ns;


			// public properties
			public enum _accessortype: byte { ui, service };
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
			public _menuset? _nextset
			{
				get {
					return this._ns;
				}
			}

			// constructor ,for implicit operation
			public _menu()
			{
				// assiging initial & non-nullable properties
			}

			// constructor ,for explicit operation
			public _menu(_accessortype? _accessor, string? _triggeringkey, string? _description, Action? _action, _menuset? _nextset)
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