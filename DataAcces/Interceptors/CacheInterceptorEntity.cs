using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Caching.Memory;
using System.Data;
using System.Data.Common;


namespace EstadisticaApp.DataAcces.Interceptors
{
    public class CacheInterceptorEntity : DbCommandInterceptor
    {
        //private readonly IMemoryCache _cache;
        //public CacheInterceptorEntity(IMemoryCache cache) {
        //    _cache = cache;
        //}
        ////Tal vez se tenga que cambiar para la parte de result
        //public override async ValueTask<InterceptionResult<DbDataReader>> ReaderExecutedAsync(
        //    DbCommand command, CommandExecutedEventData eventData,
        //    InterceptionResult<DbDataReader> result,
        //    CancellationToken cancellationToken = default)
        //{
        //    string key =
        //        command.CommandText + string.Join(",",command.Parameters.Cast<DbParameter>().Select(p => p.Value));
        //    if(_cache.TryGetValue(key, out List<Dictionary<string,object>>? cacheEntry)) {
                
        //        DataTable table = new DataTable();
        //        if (cacheEntry != null && cacheEntry.Any()) {
                 
        //            Console.WriteLine("Read of cahce <3================");
        //            foreach (var item in cacheEntry.First())
        //            {
        //                table.Columns.Add(item.Key,
        //                    item.Value is not null && item.Value?.GetType() != typeof(DBNull)
        //                    ? item.Value.GetType()
        //                    : typeof(object));

        //            }
        //            foreach (var item in cacheEntry)
        //            {
        //                table.Rows.Add(item.Values.ToArray());
        //            }
        //            DataTableReader reader = table.CreateDataReader();
        //            Console.Write("========END========================");
        //            return InterceptionResult<DbDataReader>.SuppressWithResult(reader);
        //        }

        //    }
        //    return result;
        //}
                                                      
        //public override async InterceptionResult<DbDataReader> ReaderExecutingAsync(
        //    DbCommand command, CommandEventData eventData, 
        //    DbDataReader result,
        //    CancellationToken cancellationToken = default)
        //{
        //    string key =
        //           command.CommandText + string.Join(",", command.Parameters.Cast<DbParameter>().Select(p => p.Value));
        //    List<Dictionary<string, object>> resultList = new List<Dictionary<string, object>>();
        //    if(result.HasRows)
        //    {
        //        while (await result.ReadAsync(cancellationToken))
        //        {
        //            var row = new Dictionary<string,object>();

        //            for (int i = 0; i < result.FieldCount; i++)
        //            {
        //                row.TryAdd(result.GetName(i),result.GetValue(i));
        //            }
        //            resultList.Add(row);
        //        }
        //        if (resultList.Any())
        //        {
        //            _cache.Set(key,resultList);
        //        }
        //    }

        //    await result.CloseAsync();

        //    DataTable table = new DataTable();
        //    if (resultList.Any())
        //    {

        //        Console.WriteLine("Read of cahce <3================");
        //        foreach (var item in resultList.First())
        //        {
        //            table.Columns.Add(item.Key,
        //                item.Value is not null && item.Value?.GetType() != typeof(DBNull)
        //                ? item.Value.GetType()
        //                : typeof(object));

        //        }
        //        foreach (var item in resultList)
        //        {
        //            table.Rows.Add(item.Values.ToArray());
        //        }

                
        //    }

        //    return table.CreateDataReader();
        //}


    }
}
