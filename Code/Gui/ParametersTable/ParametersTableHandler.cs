using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MOCU
{
    public class ParametersTableHandler
    {
        HashSet<string> ignoreColumns = CustomConfig.ignorePrintingExcelColumns;
        public void Init (ref DataGridView dataGrid, Dictionary<string, Dictionary<string, string>> parameters, string protocolFilePath)
        {
            int _row_index = 0;
            int _column_index = 1;      // starts from 1 because first column already taken by "parameters names"

            dataGrid.Rows.Clear();
            dataGrid.Columns.Clear();

            // Add first column out of turn for "parameters names"
            dataGrid.Columns.Add("name", "name");

            // Add Columns (must be added before rows)
            foreach (string innerDictColumnName in parameters.Values.First().Keys)
            {
                if (ignoreColumns.Contains(innerDictColumnName)) { continue; };     // skip unwanted columns

                dataGrid.Columns.Add(innerDictColumnName, innerDictColumnName);
            }

            // Add Rows and fill ferst cell with "parameter name" (in other words just add first column)
            foreach (string row_name in parameters.Keys)
            {
                // add row for every Parameter (Dictionary<string, string>)
                dataGrid.Rows.Add();

                dataGrid.Rows[dataGrid.Rows.Count - 1].Cells[0].Value = row_name;
            }


            // Fill table (the rest)
            foreach (string param_name in parameters.Keys)
            {
                foreach (string param_attribute_name in parameters[param_name].Keys)
                {
                    if (ignoreColumns.Contains(param_attribute_name)) { continue; };     // skip unwanted columns

                    dataGrid.Rows[_row_index].Cells[_column_index++].Value = parameters[param_name][param_attribute_name];
                }

                _row_index++;
                _column_index = 1;
            }


            // some GUI stuff
            dataGrid.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGrid.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;

            // Names and Values -- bold font
            dataGrid.Columns[0].DefaultCellStyle.Font = new Font(dataGrid.Font, FontStyle.Bold);
            dataGrid.Columns[3].DefaultCellStyle.Font = new Font(dataGrid.Font, FontStyle.Bold);

            // prevent annoying sorting after click on column header
            foreach (DataGridViewColumn column in dataGrid.Columns)
            {
                column.SortMode = DataGridViewColumnSortMode.NotSortable;
            }
        }
    }
}
