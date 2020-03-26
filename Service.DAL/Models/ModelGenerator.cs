﻿// This file was automatically generated by the Dapper.SimpleCRUD T4 Template
// Do not make changes directly to this file - edit the template instead
// 
// The following connection settings were used to generate this file
// 
//     Connection String Name: `default`
//     Provider:               `System.Data.SqlClient`
//     Connection String:      `server=ms-sql-9.in-solve.ru;database=1gb_excel-db;uid=1gb_bk-100886;Password=******;`
//     Include Views:          `True`

using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;
using System.Reflection;

namespace Db.Models
{
    /// <summary>
    /// A class which represents the Sheet1 table.
    /// </summary>
	[Table("Sheet1")]
	public partial class Sheet1
	{
	public object this [string name]
        {
            get
            {
                var properties = typeof(Sheet1)
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
                bool Result = false;
                foreach (var property in properties)
                {
                    if (property.Name == name && property.CanWrite)
                    {
                        property.SetValue(this, value);
                        Result = true;
                        return;
                    }
                }
				throw new ArgumentException("Can't find property");
            }
        }

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
	}

    /// <summary>
    /// A class which represents the Sheet2 table.
    /// </summary>
	[Table("Sheet2")]
	public partial class Sheet2
	{
	public object this [string name]
        {
            get
            {
                var properties = typeof(Sheet2)
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
                var properties = typeof(Sheet2)
                        .GetProperties(BindingFlags.Public | BindingFlags.Instance);
                bool Result = false;
                foreach (var property in properties)
                {
                    if (property.Name == name && property.CanWrite)
                    {
                        property.SetValue(this, value);
                        Result = true;
                        return;
                    }
                }
				throw new ArgumentException("Can't find property");
            }
        }

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
	}

    /// <summary>
    /// A class which represents the table1 table.
    /// </summary>
	[Table("table1")]
	public partial class table1
	{
	public object this [string name]
        {
            get
            {
                var properties = typeof(table1)
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
                var properties = typeof(table1)
                        .GetProperties(BindingFlags.Public | BindingFlags.Instance);
                bool Result = false;
                foreach (var property in properties)
                {
                    if (property.Name == name && property.CanWrite)
                    {
                        property.SetValue(this, value);
                        Result = true;
                        return;
                    }
                }
				throw new ArgumentException("Can't find property");
            }
        }

		[Key]
		public virtual int Id { get; set; }
		public virtual string Name { get; set; }
	}

}
