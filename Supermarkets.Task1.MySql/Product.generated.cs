#pragma warning disable 1591
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by the ClassGenerator.ttinclude code generation file.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
using System;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Data.Common;
using System.Collections.Generic;
using Telerik.OpenAccess;
using Telerik.OpenAccess.Metadata;
using Telerik.OpenAccess.Data.Common;
using Telerik.OpenAccess.Metadata.Fluent;
using Telerik.OpenAccess.Metadata.Fluent.Advanced;
using Supermarkets.Task1.MySql;

namespace Supermarkets.Task1.MySql	
{
	public partial class Product
	{
		private int _iD;
		public virtual int ID
		{
			get
			{
				return this._iD;
			}
			set
			{
				this._iD = value;
			}
		}
		
		private int _vendorID;
		public virtual int VendorID
		{
			get
			{
				return this._vendorID;
			}
			set
			{
				this._vendorID = value;
			}
		}
		
		private string _name;
		public virtual string Name
		{
			get
			{
				return this._name;
			}
			set
			{
				this._name = value;
			}
		}
		
		private int _measureID;
		public virtual int MeasureID
		{
			get
			{
				return this._measureID;
			}
			set
			{
				this._measureID = value;
			}
		}
		
		private decimal _basePrice;
		public virtual decimal BasePrice
		{
			get
			{
				return this._basePrice;
			}
			set
			{
				this._basePrice = value;
			}
		}
		
		private Vendor _vendor;
		public virtual Vendor Vendor
		{
			get
			{
				return this._vendor;
			}
			set
			{
				this._vendor = value;
			}
		}
		
		private Measure _measure;
		public virtual Measure Measure
		{
			get
			{
				return this._measure;
			}
			set
			{
				this._measure = value;
			}
		}
		
	}
}
#pragma warning restore 1591
