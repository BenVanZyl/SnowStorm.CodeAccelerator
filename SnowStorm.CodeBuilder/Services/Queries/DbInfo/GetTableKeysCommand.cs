using Dapper;
using MediatR;
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
    public class GetTableKeysCommand : IRequest<List<string>>
    {
        public string Schema { get; set; }
        public string TableName { get; set; }
        public KeyTypes KeyType { get; set; }

        public GetTableKeysCommand(string schema, string tableName, KeyTypes keyType)
        {
            Schema = schema;
            TableName = tableName;
            KeyType = keyType;
        }
    }

    public class GetTableKeysCommandHandler : IRequestHandler<GetTableKeysCommand, List<string>>
    {
        private readonly AppDbConnection _dbInfo;

        public GetTableKeysCommandHandler(IAppDbConnection dbInfo)
        {
            _dbInfo = (AppDbConnection)dbInfo;
        }

        public async Task<List<string>> Handle(GetTableKeysCommand request, CancellationToken cancellationToken)
        {
            string cmdText;
            cmdText = $@"SELECT ccu.COLUMN_NAME
                        FROM INFORMATION_SCHEMA.TABLE_CONSTRAINTS tc
                            JOIN INFORMATION_SCHEMA.CONSTRAINT_COLUMN_USAGE ccu ON tc.CONSTRAINT_NAME = ccu.Constraint_name
                        WHERE tc.CONSTRAINT_TYPE = @ConstraintType
	                      and tc.TABLE_SCHEMA = @schema
	                      and tc.TABLE_NAME = @tableName";

            string constraintType = "";
            switch (request.KeyType)
            {
                case KeyTypes.PrimaryKey:
                    constraintType = "PRIMARY KEY";
                    break;
                case KeyTypes.ForeignKey:
                    constraintType = "FOREIGN KEY";
                    break;
                default:
                    break;
            }

            try
            {
                var results = _dbInfo.DbConnection.Query<string>(cmdText, new { ConstraintType = constraintType, schema = request.Schema, tableName = request.TableName });
                Console.WriteLine(results.AsList().Count);
                return results.AsList();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"GetTableKeysCommandHandler() - {ex.Message}");
                throw;
            }
        }

    }
}