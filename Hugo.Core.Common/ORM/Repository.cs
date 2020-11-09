using Microsoft.Extensions.Logging;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hugo.Core.Common.ORM
{
    /// <summary>
    /// 业务逻辑层使用（Service）
    /// </summary>
    /// <typeparam name="TIRepository">仓储数据操作接口</typeparam>
    public class Repository<TIRepository> : IRepository where TIRepository : IRepository
    {
        protected readonly ILogger Log;
        protected readonly IDbFactory Factory;
        protected readonly TIRepository DbRepository;
        protected SqlSugarClient DB => this.Factory.GetDbContext();

        public Repository(IDbFactory factory) => Factory = factory;

        public Repository(IDbFactory factory, ILogger logger) : this(factory) => Log = logger;

        public Repository(IDbFactory factory, TIRepository repository) : this(factory) => DbRepository = repository;

        public Repository(IDbFactory factory, TIRepository repository, ILogger logger) : this(factory, repository) => Log = logger;

    }

    /// <summary>
    /// 仓储数据操作层使用（Repository）
    /// </summary>
    public class Repository : IRepository
    {
        protected readonly ILogger Log;
        protected readonly IDbFactory Factory;
        protected SqlSugarClient DB => this.Factory.GetDbContext();

        public Repository(IDbFactory factory) => Factory = factory;

        public Repository(IDbFactory factory, ILogger logger) : this(factory) => Log = logger;

    }
}