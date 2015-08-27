namespace Cirrus.Catalogue.Domain.Aggregates.Products
{
	public class Product
	{
		public Product()
		{

		}

		public Product(string id, string title)
		{
			_id = id;
			_title = title;
		}

		protected string _id;
		public string Id
		{
			get
			{
				return _id;
			}
		}

		protected string _title;
		public string Title
		{
			get
			{
				return _title;
			}
		}
	}
}