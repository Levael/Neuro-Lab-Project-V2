using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOCU
{
    public class DataTableTemplate
    {
        public DataTableTemplate (ref DataSet parameters)
        {
            DataTable _table = new DataTable("Parameters");
            DataColumn _column;
            DataRow _row;

            // CREATE COLUMNS

            // names
            _column = new DataColumn();
            _column.DataType = typeof(String);
            _column.ColumnName = "Name";
            _table.Columns.Add(_column);

            // units
            _column = new DataColumn();
            _column.DataType = typeof(String);
            _column.ColumnName = "units";
            _table.Columns.Add(_column);

            // type
            _column = new DataColumn();
            _column.DataType = typeof(String);
            _column.ColumnName = "type";
            _table.Columns.Add(_column);

            // editable
            _column = new DataColumn();
            _column.DataType = typeof(Boolean);
            _column.ColumnName = "editable";
            _table.Columns.Add(_column);

            // description
            _column = new DataColumn();
            _column.DataType = typeof(String);
            _column.ColumnName = "description";
            _table.Columns.Add(_column);

            // value
            _column = new DataColumn();
            _column.DataType = typeof(double);
            _column.ColumnName = "value";
            _table.Columns.Add(_column);

            // low bound
            _column = new DataColumn();
            _column.DataType = typeof(double);
            _column.ColumnName = "low_bound";
            _table.Columns.Add(_column);

            // high bound
            _column = new DataColumn();
            _column.DataType = typeof(double);
            _column.ColumnName = "high_bound";
            _table.Columns.Add(_column);

            // increment
            _column = new DataColumn();
            _column.DataType = typeof(double);
            _column.ColumnName = "step_plus";
            _table.Columns.Add(_column);

            // multiplier
            _column = new DataColumn();
            _column.DataType = typeof(double);
            _column.ColumnName = "step_mult";
            _table.Columns.Add(_column);


            // ===

            parameters.Tables.Add(_table);
        }
    }
}
