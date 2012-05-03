﻿using System;
using System.Collections.Generic;

namespace FluentData
{
	internal class GenericQueryHandler<TEntity>
	{
		internal TList ExecuteListReader<TList>(
									DbCommandData data,
									Action<IDataReader, TEntity> customMapperReader,
									Action<dynamic, TEntity> customMapperDynamic
					)
			where TList : IList<TEntity>
		{
			var items = (TList) data.ContextData.EntityFactory.Create(typeof(TList));

			var autoMapper = new AutoMapper<TEntity>(data);

			DynamicTypAutoMapper dynamicAutoMapper = null;

			while (data.Reader.Read())
			{
				var item = (TEntity) data.ContextData.EntityFactory.Create(typeof(TEntity));

				autoMapper.AutoMap(item);

				if (customMapperReader != null)
					customMapperReader(data.Reader, item);

				if (customMapperDynamic != null)
				{
					if (dynamicAutoMapper == null)
						dynamicAutoMapper = new DynamicTypAutoMapper(data);
					var dynamicObject = dynamicAutoMapper.AutoMap();
					customMapperDynamic(dynamicObject, item);
				}

				items.Add(item);
			}

			return items;
		}

		internal TEntity ExecuteSingle(DbCommandData data,
										Action<IDataReader, TEntity> customMapper,
										Action<dynamic, TEntity> customMapperDynamic)
		{
			AutoMapper<TEntity> autoMapper = null;

			autoMapper = new AutoMapper<TEntity>(data);

			var item = default(TEntity);

			if (data.Reader.Read())
			{
				item = (TEntity) data.ContextData.EntityFactory.Create(typeof(TEntity));

				autoMapper.AutoMap(item);

				if (customMapper != null)
					customMapper(data.Reader, item);

				if (customMapperDynamic != null)
				{
					var dynamicAutoMapper = new DynamicTypAutoMapper(data);
					var dynamicObject = dynamicAutoMapper.AutoMap();
					customMapperDynamic(dynamicObject, item);
				}
			}

			return item;
		}
	}
}
