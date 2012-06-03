﻿using FluentData._Helpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FluentData.Features.Builders.Insert
{
	[TestClass]
	public class AutoMapTests
	{
		[TestMethod]
		public void Enum_test()
		{
			using (var context = TestHelper.Context().UseTransaction(true))
			{
				var product = new ProductWithCategoryEnum();
				product.Name = "Test";
				product.CategoryId = Categories.Movies;
				product.ProductId = context.Insert("Product", product).AutoMap(x => x.ProductId).ExecuteReturnLastId();

				product = context.Sql("select * from Product where ProductId=@0", product.ProductId).QuerySingle<ProductWithCategoryEnum>();
				Assert.AreEqual(Categories.Movies, product.CategoryId);
			}
		}
	}
}