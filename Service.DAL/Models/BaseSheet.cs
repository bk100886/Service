using Dapper;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Service.DAL.Models
{
	public interface IBaseSheet
	{

	}
	//базовый класс для таблиц Sheet1, Sheet2 БД,
	//поскольку структура у этих классов одинаковая
	public partial class BaseSheet:IBaseSheet
    {
		[Key]
		public virtual Guid Id { get; set; }
		public virtual DateTime Created { get; set; }
		public virtual string Col1 { get; set; }
		public virtual string Col2 { get; set; }
		public virtual string Col3 { get; set; }
		public virtual string Col4 { get; set; }
		public virtual string Col5 { get; set; }
		public virtual string Col6 { get; set; }
		public virtual string Col7 { get; set; }
		public virtual string Col8 { get; set; }
		public virtual string Col9 { get; set; }
		public virtual string Col10 { get; set; }
		public virtual string Col11 { get; set; }
		public virtual string Col12 { get; set; }
		public virtual string Col13 { get; set; }
		public virtual string Col14 { get; set; }
		public virtual string Col15 { get; set; }
		public virtual string Col16 { get; set; }
		public virtual string Col17 { get; set; }
		public virtual string Col18 { get; set; }
		public virtual string Col19 { get; set; }
		public virtual string Col20 { get; set; }
		//индексатор для поиска свойств по названию,
		//для удобного мапинга ячеек excel в отот объект
		public object this[string name]
		{
			get
			{
				var properties = typeof(BaseSheet)
						.GetProperties(BindingFlags.Public | BindingFlags.Instance);
				foreach (var property in properties)
				{
					if (property.Name == name && property.CanRead)
						return property.GetValue(this, null);
				}
				throw new ArgumentException("Can't find property");
			}
			set
			{
				var properties = typeof(Sheet1)
						.GetProperties(BindingFlags.Public | BindingFlags.Instance);
				foreach (var property in properties)
				{
					if (property.Name == name && property.CanWrite)
					{
						property.SetValue(this, value);
						return;
					}
				}
				throw new ArgumentException("Can't find property");
			}
		}
	}
	[Table("Sheet1")]
	public partial class Sheet1:BaseSheet
	{
	}
	[Table("Sheet2")]
	public partial class Sheet2 : BaseSheet
	{
	}
}
