namespace Cirrus.Catalogue.Data.Repositories.Transactional
{
	//TODO: Install this on the ES node, don't use dynamic scripts
	internal static class Scripts
	{
		public const string update_variants =
		@"
			existing_variant = ctx._source.variants.find { v -> v.id == new_variant.id }
			do_update = existing_variant ? true : false
			if (do_update) { 
				i = ctx._source.variants.findIndexOf { v -> v.id == new_variant.id }
				ctx._source.variants[i] = new_variant
			}
			else { 
				ctx._source.variants += new_variant 
			}
		";
	}
}