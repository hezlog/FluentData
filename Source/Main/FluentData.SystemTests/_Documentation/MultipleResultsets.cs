﻿using System.Collections.Generic;
using FluentData._Helpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FluentData._Documentation
{
	[TestClass]
	public class MultipleResultsets : BaseDocumentation
	{
		[TestMethod]
		public void MultipleResultset()
		{
			using (var command = Context().MultiResultSql())
			{
				List<Category> categories = command.Sql(@"select * from Category;
												select * from Product;").Query<Category>();

				List<Product> products = command.Query<Product>();

				Assert.IsTrue(categories.Count > 0);
				Assert.IsTrue(products.Count > 0);
			}
		}
	}
}
