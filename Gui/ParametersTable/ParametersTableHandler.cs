using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MoogOcus
{
    public class ParametersTableHandler
    {
        public void Init (ref DataGridView dataGrid, Dictionary<string, Parameter> parameters, string protocolFilePath)
        {
            dataGrid.Rows.Clear();
            dataGrid.Columns.Clear();

            var test_array = Tools.DictionaryToMultidimensionalArray(parameters);

            // Add columns + their names
            foreach (var prop in typeof(Parameter).GetProperties())
            {
                if (prop.Name == "description") { continue; };

                dataGrid.Columns.Add(prop.Name, prop.Name);
            }

            // Add rows
            foreach (var parameter in parameters)
            {
                dataGrid.Rows.Add();
            }

            // Fill table
            for (int r = 0; r < dataGrid.RowCount; r++)
            {
                for (int c = 0; c < dataGrid.ColumnCount; c++)
                {
                    dataGrid.Rows[r].Cells[c].Value = test_array[r, c];
                }
            }

            dataGrid.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGrid.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;

            dataGrid.Columns[0].DefaultCellStyle.Font = new Font(dataGrid.Font, FontStyle.Bold);

            // prevent annoying sorting after click on column header
            foreach (DataGridViewColumn column in dataGrid.Columns)
            {
                column.SortMode = DataGridViewColumnSortMode.NotSortable;
            }
        }
    }
}
