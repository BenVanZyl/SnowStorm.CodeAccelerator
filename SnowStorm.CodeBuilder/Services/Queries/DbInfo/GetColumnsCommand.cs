using Dapper;
using MediatR;
using SnowStorm.CodeBuilder.Dto;
using SnowStorm.CodeBuilder.Infrastructure;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using static SnowStorm.CodeBuilder.Infrastructure.References;

namespace SnowStorm.CodeBuilder.Services.Queries.DbInfo
{
    public class GetColumnsCommand : IRequest<List<ColumnDefinition>>
    {
        public string Schema { get; set; }
        public string TableName { get; set; }

        public GetColumnsCommand(string schema, string tableName)
        {
            Schema = schema;
            TableName = tableName;
        }
    }

    public class GetColumnsCommandHandler : IRequestHandler<GetColumnsCommand, List<ColumnDefinition>>
    {
        private readonly AppDbConnection _dbInfo;

        public GetColumnsCommandHandler(IAppDbConnection dbInfo)
        {
            _dbInfo = (AppDbConnection)dbInfo;
        }

        public async Task<List<ColumnDefinition>> Handle(GetColumnsCommand request, CancellationToken cancellationToken)
        {
            string cmdText;
            cmdText = $@"SELECT COLUMN_NAME, DATA_TYPE, COLUMN_DEFAULT, CHARACTER_MAXIMUM_LENGTH, NUMERIC_PRECISION, NUMERIC_PRECISION_RADIX, NUMERIC_SCALE, DATETIME_PRECISION, ORDINAL_POSITION, IS_NULLABLE
                         FROM INFORMATION_SCHEMA.COLUMNS
                         WHERE TABLE_SCHEMA = @schema and TABLE_NAME = @tableName
                         ORDER BY ORDINAL_POSITION";

            try
            {
                var results = _dbInfo.DbConnection.Query<ColumnDefinition>(cmdText, new { schema = request.Schema, tableName = request.TableName});

                Console.WriteLine(results.AsList().Count);
                return results.AsList();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"GetColumnsCommandHandler() - {ex.Message}");
                throw;
            }
        }
}
}
