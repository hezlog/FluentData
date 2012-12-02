﻿using System;
using System.Collections.Generic;

namespace FluentData
{
	public interface ISelectBuilder<TEntity>
	{
		BuilderData Data { get; set; }
		ISelectBuilder<TEntity> Select(string sql);
		ISelectBuilder<TEntity> From(string sql);
		ISelectBuilder<TEntity> Where(string sql);
		ISelectBuilder<TEntity> AndWhere(string sql);
		ISelectBuilder<TEntity> OrWhere(string sql);
		ISelectBuilder<TEntity> GroupBy(string sql);
		ISelectBuilder<TEntity> OrderBy(string sql);
		ISelectBuilder<TEntity> Having(string sql);
		ISelectBuilder<TEntity> Paging(int currentPage, int itemsPerPage);
		ISelectBuilder<TEntity> Parameter(string name, object value, DataTypes parameterType = DataTypes.Object, ParameterDirection direction = ParameterDirection.Input, int size = 0);
		ISelectBuilder<TEntity> Parameters(params object[] parameters);
		TList QueryMany<TList>(Action<TEntity, IDataReader> customMapper = null) where TList : IList<TEntity>;
		List<TEntity> QueryMany(Action<TEntity, IDataReader> customMapper = null);
		void QueryComplexMany(IList<TEntity> list, Action<IList<TEntity>, IDataReader> customMapper);
		TEntity QuerySingle(Action<TEntity, IDataReader> customMapper = null);
		TEntity QueryComplexSingle(Func<IDataReader, TEntity> customMapper);
	}
}
