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
            get { return _i; }
        }
        public _osmenu _menu
        {
            get { return _m; }
        }

		// constructor ,for implicit inheritence
		public _osscaffold()
		{
            this._o = this;
            if (this._o == null) {
                throw new Exception("EXCEPTION: " + "Object not found");
			}
            this._i = new _osinfo(this._o);
            this._m = new _osmenu();
		}

		// constructor ,for explicit operation
		public _osscaffold(Object _object)
		{
            this._o = _object;
            if (this._o == null) {
                throw new Exception("EXCEPTION: " + "Object not found");
			}
            this._i = new _osinfo(this._o);
            this._m = new _osmenu();
		}        
	}

    public class _osinfo
    {
        // private properties
        private Type? _t;
        private string _n;
        private Assembly _a;

        // public properties
        public Type? _type
        {
            get { return this._t; }
        }
        public string _originalname
        {
            get { return this._n; }
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
        public Assembly _assembly
        {
            get { return this._a; }
        }

        // constructor
        public _osinfo(Object _object)
        {
            this._t = typeof(Object);
            if (_object.GetType() != null) {
                this._t = _object.GetType();
                this._n = this._t?.Namespace ?? string.Empty;
                this._a = this._t?.Assembly ?? new Object().GetType().Assembly ;
            }
            else {
                throw new Exception("EXCEPTION: " + "Object not found");
            }
        }
    }

    public class _osmenu
    {

    }
}