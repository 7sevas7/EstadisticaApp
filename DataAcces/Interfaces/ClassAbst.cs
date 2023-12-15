﻿using EstadisticaApp.DataAcces.Implement;
using EstadisticaApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EstadisticaApp.DataAcces.Interfaces
{
    public abstract class ClassAbst<T> : IConsultasGeneral<T> where T : class
    {
        public abstract bool BoolCount();
        public abstract Task ClearTAble();
        public abstract Task<List<T>> Get();
        public abstract Task Insert(List<T> listRange);
        public abstract Task<List<double>> UnidadSuma();
    }
}