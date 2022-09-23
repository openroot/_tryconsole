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
		public _menuform _menubase;

		// constructor ,for implicit operation
		public _osmenu()
		{
			// assiging initial & non-nullable properties
			this._menubase = new _osmenu._menuform();
		}

		// constructor ,for explicit operation
		public _osmenu(_menuform _menubase)
		{
			// assiging initial & non-nullable properties
			this._menubase = _menubase;
		}

		public class _menuform
		{
			// public properties
			public List<_menu> _menus;

			// constructor ,for implicit operation
			public _menuform()
			{
				// assiging initial & non-nullable properties
				this._menus = new List<_menu>() {};
			}

			// constructor ,for explicit operation
			public _menuform(List<_menu> _menus)
			{
				// assiging initial & non-nullable properties
				this._menus = _menus;
			}
		}

		public class _menu
		{
			// private properties
			private string? _tk;


			// public properties
			public string? _triggeringkey
			{
				get {
					return this._tk;
				}
			}
			public _menuform _nextmenuform;

			// constructor ,for implicit operation
			public _menu()
			{
				// assiging initial & non-nullable properties
				this._nextmenuform = new _menuform();
			}

			// constructor ,for explicit operation
			public _menu(_menuform _nextmenuform)
			{
				// assiging initial & non-nullable properties
				this._nextmenuform = _nextmenuform;
			}
		}
	}
}