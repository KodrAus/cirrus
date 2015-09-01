using System.Dynamic;
using Newtonsoft.Json;

namespace Cirrus.Catalogue.Domain.Aggregates.Products.ValueObjects
{
	//TODO: Improve accessibility
	//TODO: Improve relation to parent Product
	public class Variant : IProduct
	{
		public Variant()
		{
			_details = new ExpandoObject();
		}

		public Variant(string id, string title)
			: this()
		{
			Id = id;
			Title = title;
		}

		[JsonIgnore]
		protected string _id;
		[JsonIgnore]
		public string Id
		{
			get
			{
				return _id;
			}
			internal set
			{
				_id = value;
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
			internal set
			{
				_title = value;
			}
		}

		//TODO: Work out how to best transparently serialize these
		//Probably just ship up a Dictionary?
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
	}
}