﻿using EstadisticaApp.DataAcces.Interfaces;
using Microsoft.EntityFrameworkCore;


namespace EstadisticaApp.DataAcces.Implement
{
    public class ConsultaGeneral<TT> : ClassAbst<TT>  where TT : class,IModels
    {
        private DBContext __context;
        public ConsultaGeneral()
        {
            __context = new DBContext();

        }      
        public override async Task<List<TT>> Get()
        {
            
            return await __context.Set<TT>().ToListAsync();
        }

        public override async Task Insert(List<TT> listRange)
        {
            List<TT> insert = new();
            foreach (var item in listRange)
            {
                insert.Add(item);
                if (insert.Count == 300)
                {
                    await __context.Set<TT>().AddRangeAsync(insert);
                    await __context.SaveChangesAsync();
                    insert.Clear();
                    __context.ChangeTracker.Clear();
                }
            }
            await __context.Set<TT>().AddRangeAsync(insert);
            await __context.SaveChangesAsync();
            insert.Clear();
            __context.ChangeTracker.Clear();
        }
        public override async Task<List<double>> UnidadSuma()
        {
            List<double> listSuma = new List<double>();
            foreach (var item in Rubros)
            {
               var uni =  await __context.Set<TT>().Where(u => u.Rubro == item).SumAsync(r => r.Recaudado);
                listSuma.Add((double)uni);
            }

            return listSuma;
        }
        List<string> Rubros = new List<string> { "01", "02", "03", "04", "05" };
    }
}
