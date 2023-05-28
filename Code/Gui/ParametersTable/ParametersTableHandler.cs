using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MOCU
{
    /// <summary>
    /// TODO: trash, clean later
    /// </summary>
    public class ParametersTableHandler
    {
        HashSet<string> ignoreColumns = CustomConfig.ignorePrintingExcelColumns;
        public void Init (ref DataGridView dataGrid, Dictionary<string, Dictionary<string, string>> parameters, string protocolFilePath)
        {
            int _row_index = 0;
            //int _column_index = 1;      // starts from 1 because first column already taken by "parameters names"

            dataGrid.Rows.Clear();
            dataGrid.Columns.Clear();

            // Add first column out of turn for "parameters names"
            dataGrid.Columns.Add("name", "name");

            // Add Columns (must be added before rows)
            foreach (string innerDictColumnName in parameters.Values.First().Keys)
            {
                if (ignoreColumns.Contains(innerDictColumnName)) { continue; };     // skip unwanted columns

                if (innerDictColumnName == "editable") {    // change "editable" column type to checkboxes
                    var editable_column = new DataGridViewCheckBoxColumn();
                    editable_column.Name = innerDictColumnName;
                    dataGrid.Columns.Add(editable_column);
                    continue;
                };

                dataGrid.Columns.Add(innerDictColumnName, innerDictColumnName);
            }

            // Add Rows, fill first cell with "parameter name" (in other words just add first column), and change its color according to group
            foreach (string row_name in parameters.Keys)
            {
                dataGrid.Rows.Add(row_name);
                dataGrid.Rows[dataGrid.Rows.Count - 1].Cells[0].Value = row_name;
                //dataGrid.Rows[dataGrid.Rows.Count - 1].Cells[0].Style.BackColor = GUI.colorGroups[Convert.ToInt32(parameters[row_name]["color_groups"])];
            }


            // Fill table (the rest)
            foreach (string param_name in parameters.Keys)
            {
                foreach (string param_attribute_name in parameters[param_name].Keys)
                {
                    // makes row not editable if excel says so
                    if (param_attribute_name == "editable")
                    {
                        var is_editable = Convert.ToBoolean(Convert.ToInt32(parameters[param_name][param_attribute_name]));

                        dataGrid.Rows[_row_index].ReadOnly = !is_editable;
                        dataGrid.Rows[_row_index].DefaultCellStyle.ForeColor = !is_editable ? Color.Gray: dataGrid.Rows[_row_index].DefaultCellStyle.BackColor;

                        //dataGrid.Rows[_row_index].Cells[_column_index].ReadOnly = false;
                        //dataGrid.Rows[_row_index].Cells[_column_index++].Value = is_editable;

                        continue;
                    }

                    if (ignoreColumns.Contains(param_attribute_name)) { continue; };     // skip unwanted columns

                    dataGrid.Rows[_row_index].Cells[param_attribute_name].Value = parameters[param_name][param_attribute_name];
                    //dataGrid.Rows[_row_index].Cells[_column_index++].Value = parameters[param_name][param_attribute_name];
                }

                _row_index++;
                //_column_index = 1;
            }

            // add combobox to "STIMULUS VALUE" instead of just textbox
            var combo_cell = new DataGridViewComboBoxCell();
            combo_cell.Style.BackColor = dataGrid.DefaultCellStyle.BackColor;
            combo_cell.Items.Add("visual");
            combo_cell.Items.Add("vestibular");
            combo_cell.Items.Add("combined");
            combo_cell.Value = dataGrid.Rows[0].Cells["value"].Value.ToString().ToLower();
            dataGrid.Rows[0].Cells["value"] = combo_cell;

            // some GUI stuff
            dataGrid.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGrid.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;

            // Names and Values -- bold font
            dataGrid.Columns["name"].DefaultCellStyle.Font = new Font(dataGrid.Font, FontStyle.Bold);
            dataGrid.Columns["value"].DefaultCellStyle.Font = new Font(dataGrid.Font, FontStyle.Bold);

            // prevent annoying sorting after click on column header
            foreach (DataGridViewColumn column in dataGrid.Columns)
            {
                column.SortMode = DataGridViewColumnSortMode.NotSortable;
            }
        }
    }
}
