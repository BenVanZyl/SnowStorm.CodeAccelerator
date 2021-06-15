using MediatR;
using SnowStorm.CodeBuilder.Dto;
using SnowStorm.CodeBuilder.Infrastructure;
using System;
using System.Collections.Generic;
using System.Data.Common;
using Dapper;
using System.Threading;
using System.Threading.Tasks;

namespace SnowStorm.CodeBuilder.Services.Queries.DbInfo
{
    public class GetTablesCommand : IRequest<List<TableListDefinition>>
    {
        public readonly string Filter = "";
        public GetTablesCommand()
        { }

        public GetTablesCommand(string filter)
        {
            Filter = filter;
        }

    }

    public class GetTablesCommandHandler : IRequestHandler<GetTablesCommand, List<TableListDefinition>>
    {
        private readonly AppDbConnection _dbInfo;

        public GetTablesCommandHandler(IAppDbConnection dbInfo)
        {
            _dbInfo = (AppDbConnection)dbInfo;
        }

        public async Task<List<TableListDefinition>> Handle(GetTablesCommand request, CancellationToken cancellationToken)
        {
            string filterText = "";
            string cmdText;

            if (!string.IsNullOrEmpty(request.Filter))
                filterText = $" AND TABLE_NAME Like '%{request.Filter}%'";

            cmdText = $"SELECT TABLE_CATALOG, TABLE_SCHEMA, TABLE_NAME FROM INFORMATION_SCHEMA.Tables WHERE TABLE_TYPE = 'BASE TABLE' {filterText} Order by TABLE_CATALOG, TABLE_SCHEMA, TABLE_NAME";

            try
            {
                var results = _dbInfo.DbConnection.Query<TableListDefinition>(cmdText);
                Console.WriteLine(results.AsList().Count);
                return results.AsList();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"GetTablesCommandHandler() - {ex.Message}");
                throw;
            }

        }
    }
}
