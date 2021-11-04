using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MediatR;
using SnowStorm.CodeBuilder.Dto;
using SnowStorm.CodeBuilder.Infrastructure;
using SnowStorm.CodeAccelerator.Services.Commands;
using SnowStorm.CodeBuilder.Services.Queries.DbInfo;

namespace SnowStorm.CodeBuilder
{
    public partial class MainForm : Form
    {


        public MainForm(IMediator mediator, IAppDbConnection dbInfo)
        {
            InitializeComponent();

            _mediator = mediator;
            _dbInfo = (AppDbConnection)dbInfo;
        }

        public TableDefinition TableInfo = null;

        private IMediator _mediator;
        private readonly AppDbConnection _dbInfo;

        private Dictionary<string, string> GeneratedCode = new Dictionary<string, string>();

        private void button1_Click(object sender, EventArgs e)
        {
            GetTables().GetAwaiter();
        }

        private void GenerateCodeButton_Click(object sender, EventArgs e)
        {
            GenerateTheCode().GetAwaiter();
        }

        private void SetDbInfo()
        {
            _dbInfo.ConnectionString = ConnectionStringText.Text;
        }

        private void ListOfTables_DoubleClick(object sender, EventArgs e)
        {
            var tbl = ListOfTables.SelectedItem;
            GetTable((TableListDefinition)tbl).GetAwaiter();

        }

        private async Task GetTables()
        {
            SetDbInfo();

            var results = await _mediator.Send(new GetTablesCommand());
            ListOfTables.DataSource = results;
            ListOfTables.DisplayMember = "TableId";
            ListOfTables.ValueMember = "TableId";
        }

        private async Task GetTable(TableListDefinition tbl)
        {
            SetDbInfo();

            TableInfo = new TableDefinition
            {
                Table = tbl
            };

            TableInfo.Columns = await _mediator.Send(new GetColumnsCommand(TableInfo.Table.TABLE_SCHEMA, TableInfo.Table.TABLE_NAME));
            ListOfColumns.DataSource = TableInfo.Columns;

            TableInfo.PrimaryKeys = await _mediator.Send(new GetTableKeysCommand(TableInfo.Table.TABLE_SCHEMA, TableInfo.Table.TABLE_NAME, References.KeyTypes.PrimaryKey));
            ListOfPrimaryKeys.DataSource = TableInfo.PrimaryKeys;

            TableInfo.ForeignKeys = await _mediator.Send(new GetTableKeysCommand(TableInfo.Table.TABLE_SCHEMA, TableInfo.Table.TABLE_NAME, References.KeyTypes.ForeignKey));
            ListOfForeignKeys.DataSource = TableInfo.ForeignKeys;
        }

        private async Task GenerateTheCode()
        {
            InitDictionary();

            var helper = new CodeGeneratorHelper()
            {
                Schema = TableInfo.Table.TABLE_SCHEMA,
                TableName = TableInfo.Table.TABLE_NAME,
                TableDomainName = TableInfo.Table.TABLE_NAME,
                Columns = TableInfo.Columns,
                PrimaryKeys = TableInfo.PrimaryKeys,
                ForeignKeys = TableInfo.ForeignKeys

            };

            string r;

            r = await _mediator.Send(new CreateDtoObjectCommand(helper));
            GeneratedCode["DomainDto"] = r;

            r = await _mediator.Send(new CreateDomainObjectCommand(helper));
            GeneratedCode["DomainObject"] = r;

            r = await _mediator.Send(new CreateQueryCommand(helper));
            GeneratedCode["QueryClass"] = r;

            r = await _mediator.Send(new CreateSaveCmdObjectCommand(helper));
            GeneratedCode["SaveCommand"] = r;
        }

        private void LoadCodeOptions()
        {
            CodeOptions.Items.Add("---");
            CodeOptions.Items.Add("DomainDto");
            CodeOptions.Items.Add("DomainObject");
            CodeOptions.Items.Add("QueryClass");
            CodeOptions.Items.Add("SaveCommand");

            InitDictionary();
        }

        private void InitDictionary()
        {
            GeneratedCode = new Dictionary<string, string>();
            foreach (var item in CodeOptions.Items)
            {
                GeneratedCode[item.ToString()] = "No code generated for this option.";
            }
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            LoadCodeOptions();
        }

        private void CodeOptions_SelectedIndexChanged(object sender, EventArgs e)
        {
            GeneratedCodeText.Text = GeneratedCode[CodeOptions.SelectedItem.ToString()];
        }
    }

}
