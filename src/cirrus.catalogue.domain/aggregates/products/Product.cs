using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace Cirrus.Catalogue.Domain.Aggregates.Products
{
	using ValueObjects;

	public class Product : IProduct
	{
		public Product()
		{
			_variants = new List<Variant>{ new Variant() };
		}

		public Product(string id, string title)
			: this()
		{
			_id = id;
			Title = title;
		}

		[JsonIgnore]
		protected string _id;
		public virtual string Id
		{
			get
			{
				return _id;
			}
			protected set
			{
				_id = value;
			}
		}

		[JsonIgnore]
		public virtual string Title
		{
			get
			{
				return _defaultVariant != null ? _defaultVariant.Title : default(string);
			}
			protected set
			{
				_defaultVariant.Title = value;
			}
		}

		[JsonIgnore]
		public virtual dynamic Details
		{
			get
			{
				return _defaultVariant != null ? _defaultVariant.Details : null;
			}
			set
			{
				_defaultVariant.Details = value;
			}
		}

		[JsonIgnore]
		protected IEnumerable<Variant> _variants;
		public virtual IEnumerable<Variant> Variants
		{
			get
			{
				return _variants;
			}
		}

		[JsonIgnore]
		private Variant _defaultVariant
		{
			get
			{
				return _variants.FirstOrDefault();
			}
		}
	}
}