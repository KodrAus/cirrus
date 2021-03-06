using System.Collections.Generic;
using System.Linq;
using System.Dynamic;
using Newtonsoft.Json;

namespace Cirrus.Catalogue.Domain.Aggregates.Products.Entities
{
	public class Product
	{
		public Product()
		{
			_variants = new List<Product>();
			_categories = new List<Category>();
			_details = new ExpandoObject();
		}

		public Product(string id, string title)
			: this()
		{
			_id = id;
			_title = title;
		}

		[JsonIgnore]
		protected string _id;
		public string Id
		{
			get
			{
				return _id;
			}
		}

		[JsonIgnore]
		protected string _title;
		public string Title
		{
			get
			{
				return _title;
			}
		}

		[JsonIgnore]
		protected ExpandoObject _details;
		public dynamic Details
		{
			get
			{
				return _details;
			}
			set
			{
				_details = value;
			}
		}

		[JsonIgnore]
		protected string _description;
		protected string Description
		{
			get
			{
				return _description;
			}
		}

		[JsonIgnore]
		protected string _summary;
		protected string Summary
		{
			get
			{
				return _summary;
			}
		}

		[JsonIgnore]
		protected IEnumerable<Category> _categories;
		protected IEnumerable<Category> Categories
		{
			get
			{
				return _categories;
			}
		}

		[JsonIgnore]
		protected IEnumerable<Product> _variants;
		protected IEnumerable<Product> Variants
		{
			get
			{
				return _variants;
			}
		}
	}
}
