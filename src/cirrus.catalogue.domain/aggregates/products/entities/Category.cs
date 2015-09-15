namespace Cirrus.Catalogue.Domain
{
	public class Category
	{
		public Category()
		{

		}

		public Category(string title)
		{
			_title = title;
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