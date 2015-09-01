using Cirrus.Catalogue.Domain.Aggregates.Products.ValueObjects;

namespace Cirrus.Catalogue.Data.DTOs
{
	public class VariantDTO : Variant
	{
		public new string Id
		{
			get
			{
				return _id;
			}
			set
			{
				_id = value;
			}
		}

		public new string Title
		{
			get
			{
				return _title;
			}
			set
			{
				_title = value;
			}
		}
	}
}