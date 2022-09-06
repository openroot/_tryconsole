using System.Reflection;

namespace _tryconsole
{
    public class _gateway
	{
		List<Object>? _models = null;

		public _gateway()
		{
			this._models = new List<Object>();
		}

		public static void Main(string[] args)
		{
			_gateway _gateway = new _gateway();

			try
			{
				_classconfiguration _sampleclassconfig = _gateway._getasampleclassconfig();
				_constructclass _constructclass = new _constructclass(_sampleclassconfig);
				Object? _constructedclass = _constructclass._getconstructedclass();
				
				if (_constructedclass != null) {
					_gateway._models?.Add(_constructedclass);
				}
				_gateway._outputclassbehaviourminimal();
			}
			catch(Exception _exception)
			{
				Console.WriteLine("EXCEPTION: " + _exception.Message);
			}
		}

		public void _readclassconfigs()
		{
			//Console.ReadLine();
		}

		public _classconfiguration _getasampleclassconfig()
		{
			_classconfiguration _sampleclassconfig = new _classconfiguration(
				"_student", 
				new List<_propertyconfiguration>() {
					new _propertyconfiguration("int", "_id"),
					new _propertyconfiguration("string", "_name"),
					new _propertyconfiguration("string", "_address")
				}
			);
			return _sampleclassconfig;
		}

		public void _outputclassbehaviourminimal()
		{
			// output the models found in this._models
			if(this._models != null)
			{
				foreach(Object _model in _models)
				{
					if (_model != null)
					{
						Type _class = _model.GetType();

						Console.WriteLine(_class + " [class name] (");
						foreach(PropertyInfo _property in _class.GetProperties())
						{
							Console.WriteLine(_property.Name + " [property name] <-- " + _property?.ToString()?.Split(" ").FirstOrDefault() + " [property type]");
						}
						Console.WriteLine(")");
					}
				}
			}
		}
	}
}
