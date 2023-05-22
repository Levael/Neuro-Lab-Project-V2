using Microsoft.Office.Interop.Excel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel;       //microsoft Excel 15 object in references -> COM tab

namespace MoogOcus
{
    /// <summary>
    /// This class is used to read the data from excel protocol file and load it into the GuiInterface
    /// </summary>
    public class ExcelHandler
    {
        /// <summary>
        /// Excel application handler.
        /// </summary>
        private Excel.Application _xlApp;

        /// <summary>
        /// Starts the excel app
        /// </summary>
        public ExcelHandler()
        {
            _xlApp = new();
        }

        /// <summary>
        /// Closes the excel application and destroy it's running app. Must have to call Garbage Collector
        /// </summary>
        public void CloseExcelHandler()
        {
            //close the file (part 2)

            _xlApp.Quit();
            Marshal.FinalReleaseComObject(_xlApp);
            _xlApp = null;

            // THE MAIN PART IS CALL GARBAGE COLLECTOR, F*CK U MICROSOFT
            GC.Collect();
        }

        private void CloseExcelSubprogramms(ref Excel.Range excelRange, ref Excel._Worksheet xlWorksheet, ref Excel.Workbook xlWorkbook, ref Excel.Workbooks xlWorkbooks)
        {
            xlWorkbook.Close();
            xlWorkbooks.Close();

            Marshal.FinalReleaseComObject(excelRange);
            Marshal.FinalReleaseComObject(xlWorksheet);
            Marshal.FinalReleaseComObject(xlWorkbook);
            Marshal.FinalReleaseComObject(xlWorkbooks);

            excelRange = null;
            xlWorksheet = null;
            xlWorkbook = null;
            xlWorkbooks = null;
        }

        public delegate Dictionary<string, Dictionary<string, string>> CustomCallback(Excel.Range excelRange);

        /// <summary>
        /// Opens file -> Invoke callback function -> Close file
        /// </summary>
        /// <param name="protocolFilePath"> protocol file path (string)</param>
        public Dictionary<string, Dictionary<string, string>> OpenDoClose(string protocolFilePath, Dictionary<string, Dictionary<string, string>> parameters, CustomCallback DoFunction)
        {
            Excel.Workbooks xlWorkbooks;
            Excel.Workbook xlWorkbook;
            Excel._Worksheet xlWorksheet;
            Excel.Range excelRange;


            var emptyDict = new Dictionary<string, Dictionary<string, string>>();

            //open file
            try
            {
                xlWorkbooks = _xlApp.Workbooks;
                xlWorkbook = xlWorkbooks.Open(protocolFilePath);
                xlWorksheet = xlWorkbook.Sheets["parameters"];
                excelRange = xlWorksheet.UsedRange;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error in Excel Loader: {protocolFilePath} -- {ex}", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return emptyDict;
            }

            parameters = DoFunction(excelRange);

            //close file (part 1)
            CloseExcelSubprogramms(ref excelRange, ref xlWorksheet, ref xlWorkbook, ref xlWorkbooks);


            return parameters;
        }

        /// <summary>
        /// Reads excel protocol file an updates "parameters dictionary".
        /// </summary>
        /// <param name="protocolFilePath">The protocol file path to be read.</param>
        /// <param name="parameters">The dictionary with all data: from excel to dictionary</param>
        public void ReadFromExcel(string protocolFilePath, ref Dictionary<string, Dictionary<string, string>> parameters)
        {
            // all this 'poebota' because of "Cannot use ref or out parameter in lambda expressions" error
            parameters = OpenDoClose(protocolFilePath, parameters, delegate (Excel.Range excelRange)
            {
                // parse table to 2D array of object type
                // TODO: change later to strings (to avoid boxing&inboxing)
                object[,] valuesArray = (object[,])excelRange.get_Value(Excel.XlRangeValueDataType.xlRangeValueDefault);

                // converts 2d_obj_arr to 2D_str_dict. parameters is 'ref' type, so it's instead of 'return'
                return Tools.Convert2DObjectsTo2DStringDictionary(valuesArray);
            });
        }

        /// <summary>
        /// Writes to excel file an updated (or new) "parameters dictionary".
        /// </summary>
        /// <param name="protocolFilePath">The protocol file path</param>
        /// <param name="parameters">The dictionary with all data: from dictionary to excel</param>
        public void WriteToExcel(string protocolFilePath, Dictionary<string, Dictionary<string, string>> parameters)
        {
            OpenDoClose(protocolFilePath, parameters, delegate (Excel.Range excelRange)
            {
                return parameters;
            });
        }

        /// <summary>
        /// Writes a protocol file to the protocols folder.
        /// </summary>
        /// <param name="protocolFilePath">The protocol folder path with protocol name.</param>
        /// <param name="variables">The variables to write to the protocol.</param>
        /// <param name="checkboxesDictionary">A dictionary consists all gui checkboxes and their states.</param>
        /*public void WriteProtocolFile(string protocolFilePath, Variables variables, Dictionary<string, ButtonBase> checkboxesDictionary)
        {
            Excel.Workbook newProtocolFile = _xlApp.Workbooks.Add();

            Excel.Worksheet workSheet = newProtocolFile.Worksheets.Add();

            //change the sheet name to variables.
            workSheet.Name = "variables";

            //add the first line of heads titles to each column
            int columnIndex = 1;
            foreach (string title in variables._variablesDictionary.ElementAt(0).Value._description.Keys)
            {
                //add the title for each column of the excel file.
                workSheet.Cells[1, columnIndex] = title;
                //move next column title.
                columnIndex++;
            }

            //save each line according to the variable parameters.
            int rowIndex = 2;
            foreach (KeyValuePair<string, Variable> item in variables._variablesDictionary)
            {
                //reset the column index for the new line.
                columnIndex = 1;

                //if not a checkbox property
                if (checkboxesDictionary.Keys.Count(x => x == item.Key) == 0)
                {
                    foreach (string titleName in item.Value._description.Keys)
                    {
                        //write the column to the variable
                        workSheet.Cells[rowIndex, columnIndex] = item.Value._description[titleName].MoogParameter;
                        //go to next column for the same variable.
                        columnIndex++;
                    }
                }
                //if a checkbox property.
                else
                {
                    foreach (string titleName in item.Value._description.Keys)
                    {
                        //add the name of the variable checkbox.
                        if (titleName == "name")
                        {
                            workSheet.Cells[rowIndex, columnIndex] = item.Key;
                        }
                        else if (titleName == "parameters")
                        {
                            //write the column to the variable
                            if (checkboxesDictionary[item.Key] is RadioButton)
                                workSheet.Cells[rowIndex, columnIndex] =
                                    (checkboxesDictionary[item.Key] as RadioButton).Checked ? "1" : "0";
                            else if (checkboxesDictionary[item.Key] is CheckBox)
                                workSheet.Cells[rowIndex, columnIndex] =
                                    (checkboxesDictionary[item.Key] as CheckBox).Checked ? "1" : "0";
                            //todo:exception if the type is else.

                        }
                        else if (titleName == "status")
                        {
                            //write the column to the variable
                            workSheet.Cells[rowIndex, columnIndex] = "-1";
                        }
                        else
                        {
                            //write the column to the variable
                            workSheet.Cells[rowIndex, columnIndex] = "0";
                        }

                        //go to next column for the same variable.
                        columnIndex++;
                    }
                }
                //go to next line (for next variable)
                rowIndex++;
            }

            //add all checkboxes that are not in the excel file from the beginning.
            foreach (var item in checkboxesDictionary)
            {
                //reset the column index for the new line.
                columnIndex = 1;

                //if the checkbox not included in the variable dictionary.
                if (variables._variablesDictionary.Keys.Count(key => key == item.Key) == 0)
                {
                    foreach (string titleName in variables._variablesDictionary.ElementAt(0).Value._description.Keys)
                    {
                        //add the name of the variable checkbox.
                        if (titleName == "name")
                        {
                            workSheet.Cells[rowIndex, columnIndex] = item.Key;
                        }
                        else if (titleName == "parameters")
                        {
                            //write the column to the variable
                            if (item.Value is CheckBox)
                                workSheet.Cells[rowIndex, columnIndex] = (item.Value as CheckBox).Checked ? "1" : "0";
                            else if (item.Value is RadioButton)
                                workSheet.Cells[rowIndex, columnIndex] =
                                    (item.Value as RadioButton).Checked ? "1" : "0";
                            //todo:exception if the type is else.
                        }
                        else if (titleName == "status")
                        {
                            //write the column to the variable
                            workSheet.Cells[rowIndex, columnIndex] = "-1";
                        }
                        else
                        {
                            //write the column to the variable
                            workSheet.Cells[rowIndex, columnIndex] = "0";
                        }

                        //go to next column for the same variable.
                        columnIndex++;
                    }

                    //go to next line (for next variable)
                    rowIndex++;
                }
            }

            try//it is for the event when the file with the same name exists and the user checked the saving.
            {
                //save the file and close it.
                newProtocolFile.SaveAs(protocolFilePath + ".xlsx");
                newProtocolFile.Close();
            }
            catch { }
        }*/
         


    }
}
