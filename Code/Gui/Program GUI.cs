﻿using Microsoft.Office.Interop.Excel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Windows.Forms;
/*using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using Font = System.Drawing.Font;*/

namespace MOCU
{
    public partial class GUI : Form
    {

        #region INITIALIZATIONS

        /// <summary>
        /// todo
        /// </summary>
        private ExcelHandler _excelHandler;

        /// <summary>
        /// todo
        /// </summary>
        private ControlLoop _controlLoop;

        /// <summary>
        /// The selected protocols path to view protocols.
        /// </summary>
        private string _protocolsDirPath;

        /// <summary>
        /// The default protocols path
        /// </summary>
        private string _defaultProtocolsDirPath;

        /// <summary>
        /// The curent selected protocol path.
        /// </summary>
        private string _protocolFilePath;

        /// <summary>
        /// All parameters
        /// </summary>
        public InputData inputData;

        /// <summary>
        /// Dictionary that describes all ButtonBase (checkboxes and radiobuttons) names in the gui as keys with their control as value.
        /// </summary>
        public Dictionary<string, ButtonBase> checkboxesDictionary;

        /// <summary>
        /// Dictionary that describes all Text boxes' names in the gui as keys with their control as value.
        /// </summary>
        public static Dictionary<string, Control> textboxesDictionary;

        /// <summary>
        /// Dictionary that describes all buttons names in the gui as keys with their control as value.
        /// </summary>
        public static Dictionary<string, Control> buttonsDictionary;

        /// <summary>
        /// Dictionary that describes all statuses textboxes names in the gui as keys with their control as value.
        /// </summary>
        public static Dictionary<string, Control> statusesDictionary;

        /// <summary>
        /// Dictionary that describes all statuses colors. Semantic name as key, C# names as value.
        /// </summary>
        public static Dictionary<string, Color> statusesColorsDictionary;

        public static Dictionary<int, Color> colorGroups;

        /// <summary>
        /// todo
        /// </summary>
        public ParametersTableHandler parametersTableHandler;

        /*protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ExStyle |= 0x02000000;
                return cp;
            }

        //DoubleBuffered = true;
        }*/


        #endregion INITIALIZATIONS

        public GUI()
        {
            // Initialize common objects

            _defaultProtocolsDirPath = CustomConfig.defaultProtocolsDirPath;
            _excelHandler = new();
            _controlLoop = new();


            // Initialize gui stuff

            InitializeComponent();          // built in WinForms function

            DictionarilizeCheckBoxes();     // fill checkboxesDictionary (put controls to dictionary)
            DictionarilizeTextBoxes();      // fill textboxesDictionary
            DictionarilizeButtons();        // fill buttonsDictionary
            DictionarilizeStatuses();       // fill statusesDictionary
            DictionarilizeStatusesColors(); // fill statusesColorsDictionary
            DictionarilizeColorGroups();    // fill colorGroups
            InitialiseCombobox();           // fill combobox with excel files from default protocol folder

            // !!!!!!
            System.Windows.Forms.TextBox.CheckForIllegalCrossThreadCalls = false;




            // Connect to Moog and Cedrus

            _controlLoop.MoogConnect();
            _controlLoop.CedrusConnect();



            //CheckConnectedDevices();        // update statuses according to Moog, Oculus, Cedrus connections




            //InfoPrinter.PrintInfo(textboxesDictionary, "Initialized some components (stam, for test)");


            /*

            Globals._systemState = SystemState.INITIALIZED;

            //make the delegate with it's control object and their nickname as pairs of dictionaries.
            Tuple<Dictionary<string, Control>, Dictionary<string, Delegate>> delegatsControlsTuple = MakeCtrlDelegateAndFunctionDictionary();
            _cntrlLoop = new ControlLoop(delegatsControlsTuple.Item2, delegatsControlsTuple.Item1, _logger);

            try
            {
                // todo: doesn't work, may be not connected without exception
                //connect to the robot.
                MoogController.MoogController.Connect();
                _cntrlLoop.IsMoogConnected = true;
            }
            catch
            {
                _cntrlLoop.IsMoogConnected = false;

                MessageBox.Show("Cannot connect to the robot - check if robot is conncted in listen mode and also not turned off", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            //allocate the start/stop button locker.
            _lockerStopStartButton = new object();
            //disable initially the start and stop button until makeTrials button is pressed.
            _btnStart.Enabled = false;
            _btnStop.Enabled = false;

            //allocate the pause/resume button locker.
            _lockerPauseResumeButton = new object();
            //disable initially both pause and resume buttons until makeTrials button is pressed.
            *//*_btnPause.Enabled = false;
            _btnResume.Enabled = false;*//*

            //enable the make trials btn.
            _btnMakeTrials.Enabled = true;

            //the start point is in the disengaged state.
            _isEngaged = false;

            //add the students names (as the setting have) to the student names combo box.
            //AddStudentsNamesToRatNamesComboBox();

            //set the default file browser protocol path directory.
            SetDefaultProtocolFileBrowserDirectory();

            //move the robot to it's home position when startup.
            //avi-insert//
            //_cntrlLoop.WriteHomePosFile();
            //_cntrlLoop.MoveRobotHomePosition();

            //create the result directory in the application path if needed.
            if (!Directory.Exists(Application.StartupPath + @"\results"))
            {
                Directory.CreateDirectory(Application.StartupPath + @"\results\");
            }*/
        }



        #region DIFFERENT FUNCTIONS
        private void InitializeInputParameters()
        {
            inputData = new();      // all input parameters in inputData.parameters

            _excelHandler.ReadFromExcel(_protocolFilePath, ref inputData.parameters);     // fill 'parameters' with data
        }

        private void InitializeGuiParametersTable()
        {
            parametersTableHandler = new();
            parametersTableHandler.Init(ref Parameters_table, inputData.parameters, _protocolFilePath);     // fill gui table with data from 'parameters'
        }

        private void InitialiseCombobox()
        {
            _protocolsDirPath = _defaultProtocolsDirPath;
            AddFilesToCombobox(_protocolsDirPath);

            //Choose_Protocol_combobox.Focused = true;
        }

        private void AddFilesToCombobox(string dirPath)
        {
            Choose_Protocol_combobox.Items.Clear();


            string[] allFilesInFolder = Directory.GetFiles(dirPath);

            foreach (string file in allFilesInFolder)
            {
                if (Path.GetExtension(file).Equals(".xlsx"))
                {
                    Choose_Protocol_combobox.Items.Add(Path.GetFileName(file));
                }
            }


            if (Choose_Protocol_combobox.Items.Count == 0)
            {
                InfoPrinter.PrintTextOfType("WARNING", "No excel files");
                return;
            }
        }

        /// <summary>
        /// to prevent annoing blue focusing.remove focus to 'stub_label'(заглушка)
        /// </summary>
        private void RemoveFocus()
        {
            stub_label.Focus();
        }

        #endregion DIFFERENT FUNCTIONS

        #region DICTIONARISATION


        public void CheckConnectedDevices()
        {
            statusesDictionary["MOOG"].BackColor = CheckMoog() ? statusesColorsDictionary["GOOD"] : statusesColorsDictionary["ERROR"];

            statusesDictionary["OCULUS"].BackColor = CheckOculus() ? statusesColorsDictionary["GOOD"] : statusesColorsDictionary["ERROR"];

            statusesDictionary["CEDRUS"].BackColor = CheckCedrus() ? statusesColorsDictionary["GOOD"] : statusesColorsDictionary["ERROR"];
        }

        private Boolean CheckMoog()
        {
            return false;
        }

        private Boolean CheckOculus()
        {
            return false;
        }

        private Boolean CheckCedrus()
        {
            return false;
        }

        /// <summary>
        /// Initializes the checkboxes dictionary with names as key with the control as value.
        /// </summary>
        private void DictionarilizeCheckBoxes()
        {
            checkboxesDictionary = new()
            {
                { "EEG", EEG_checkbox },
                { "VISUAL_OUTPUTS", VisualOutputs_checkbox },
                { "CONTROLLER", Controller_checkbox },
                { "INSTRUCTIONS", Instructions_checkbox },
                { "STATUS", Status_checkbox }
            };
        }

        /// <summary>
        /// Initializes the buttons dictionary with names as key with the control as value.
        /// </summary>
        private void DictionarilizeButtons()
        {
            buttonsDictionary = new()
            {
                { "BROWSE_PROTOCOL_FOLDER", Browse_protocol_btn },
                { "SAVE_PROTOCOL", Save_protocol_btn },
                { "CONNECT", Connect_btn },
                { "ENGAGE", Engage_btn },
                { "PARK", Park_btn },
                { "MAKE_TRIALS", Make_trials_btn },
                { "START_EXPERIMENT", Start_btn },
                { "PAUSE_EXPERIMENT", Pause_btn },
                { "RESUME_EXPERIMENT", Resume_btn },
                { "STOP_EXPERIMENT", Stop_btn },
                { "START_TRIAL_CONTROLLER", Controller_start_btn },
                { "UP_CONTROLLER", Controller_up_btn },
                { "DOWN_CONTROLLER", Controller_down_btn },
                { "LEFT_CONTROLLER", Controller_left_btn },
                { "RIGHT_CONTROLLER", Controller_right_btn }
            };
        }

        /// <summary>
        /// Initializes the checkboxes dictionary with names as key with the control as value.
        /// </summary>
        private void DictionarilizeTextBoxes()
        {
            textboxesDictionary = new()
            {
                { "INFO", Info_textbox },
                { "WARNING", Warning_textbox },
                { "INFORMATION", Instructions_textbox }
            };
        }

        /// <summary>
        /// Initializes the statuses dictionary with names as key with the control as value.
        /// </summary>
        private void DictionarilizeStatuses()
        {
            statusesDictionary = new()
            {
                { "MOOG", Moog_status_indicator },
                { "OCULUS", Oculus_status_indicator },
                { "CEDRUS", Cedrus_status_indicator },
                { "UNITY", Unity_status_indicator },
                { "TRIALS", Trials_status_indicator },
                { "IS_RUNNING", IsRunning_status_indicator }
            };
        }

        /// <summary>
        /// Initializes the statuses colors dictionary with names as key with the control as value.
        /// </summary>
        private void DictionarilizeStatusesColors()
        {
            statusesColorsDictionary = new()
            {
                { "DISABLED", Color.LightSlateGray },
                { "LOADING", Color.Gold },
                { "GOOD", Color.LimeGreen },
                { "ERROR", Color.Tomato }
            };
        }

        private void DictionarilizeColorGroups()
        {
            colorGroups = new() {
                { 0, Color.LightCoral },
                { 1, Color.PaleTurquoise },
                { 2, Color.PapayaWhip },
                { 3, Color.PaleGreen },
                { 4, Color.LavenderBlush }
            };
        }


        /*/// <summary>
        /// Initializes the sections dictionary with names as key with the section as value.
        /// </summary>
        private void InitializeSectionsDictionary()
        {
            sectionsDictionary = new()
            {
                { "Info_Graph_section", Info_Graph_section },
                { "WARNINGS", Warning_textbox },
                { "INFORMATION", Instructions_textbox }
            };
        }*/

        #endregion DICTIONARISATION

        #region EVENT LISTENERS

        /// <summary>
        /// Choose folder with protocols (update dirPath from default to new)
        /// </summary>
        private void Browse_protocol_btn_Click(object sender, EventArgs e)
        {
            RemoveFocus();

            if (FolderBrowserDialog.ShowDialog() == DialogResult.OK)
            {
                _protocolsDirPath = FolderBrowserDialog.SelectedPath;
                AddFilesToCombobox(_protocolsDirPath);

                Choose_Protocol_combobox.DroppedDown = true;    // auto opening
            }
        }

        /// <summary>
        /// Show anf fill data table with parameters
        /// </summary>
        private void Choose_Protocol_combobox_SelectedIndexChanged(object sender, EventArgs e)
        {
            RemoveFocus();

            if (Choose_Protocol_combobox.Items.Count == 0)
            {
                InfoPrinter.PrintTextOfType("WARNING", "No excel files");
                return;
            }

            _protocolFilePath = @$"{_protocolsDirPath}\{Choose_Protocol_combobox.SelectedItem}";

            InitializeInputParameters();     // fill inputData.parameters
            InitializeGuiParametersTable();  // fill gui table with inputData.parameters values

            // hide instruction_section and show in full height parameters_table_section
            VariablesInstructions_section.Panel2Collapsed = true;   // hide instructions section
            VariablesInstructions_section.Panel1Collapsed = false;
        }

        private void Connect_btn_Click(object sender, EventArgs e)
        {
            _controlLoop.MoogConnect();
        }

        private void Engage_btn_Click(object sender, EventArgs e)
        {
            _controlLoop.MoogEngage();
        }

        private void Park_btn_Click(object sender, EventArgs e)
        {
            _controlLoop.MoogDisengage();
        }
        #region FORM CONTROLLER

        private void Controller_start_btn_Click(object sender, EventArgs e)
        {
            // for tests
            CedrusHandler.GetAnswer();
        }

        private void Controller_left_btn_Click(object sender, EventArgs e)
        {

        }

        private void Controller_right_btn_Click(object sender, EventArgs e)
        {

        }

        private void Controller_up_btn_Click(object sender, EventArgs e)
        {

        }

        private void Controller_down_btn_Click(object sender, EventArgs e)
        {

        }

        #endregion FORM CONTROLLER

        #endregion EVENT LISTENERS

        #region FORM EVENTS

        /// <summary>
        /// Gui changes after full page loading
        /// </summary>
        private void Form_Load(object sender, EventArgs e)     //TODO: use overrided method OnLoad instead
        {
            // at start hide parameters_section until user selects excel_protocol_file and shown only instruction section
            VariablesInstructions_section.Panel1Collapsed = true;
            VariablesInstructions_section.Panel2Collapsed = false;

            Form_ResizeEnd(sender, e);

            // combobox custom settings (from gui designer impossible to change)
            Choose_Protocol_combobox.ItemHeight = 20;
            //Choose_Protocol_combobox.DropDownHeight = 20;

        }

        /// <summary>
        /// On resize event fix stuff
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form_ResizeEnd(object sender, EventArgs e)
        {
            // resize of bottom section height. change later to function
            Left_panel_section.SplitterDistance = Left_panel_section.Height - AllControlls_wrapper.Height - 10;

            // move splitter right up to status section (16 = status_label.paddings + status_wrapper.paddings)
            ProtocolNames_Status_section.SplitterDistance = Header_Body_section.Width - Statuses_wrapper.Width - Status_label.Width - 16;

            // devides both header and body in a center
            Protocol_Names_section.SplitterDistance = Body_section.SplitterDistance;

            // adjust header height to the largest to take as mush less place as possible (+2 = padding of wrapper)
            Header_Body_section.SplitterDistance = Math.Max(Protocol_browse_save_wrapper.Height, Math.Max(Name_inputs_wrapper.Height + 2, Statuses_wrapper.Height));
        }

        /// <summary>
        /// Important stuff to do when exiting
        /// </summary>
        private void Form_Closing(object sender, FormClosingEventArgs e)
        {
            if (MessageBox.Show("Exit?", "test window", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                _excelHandler.CloseExcelHandler();
                CedrusHandler.Disconnect();
            }
            else
            {
                e.Cancel = true;
            }
        }

        /// <summary>
        /// dataGridView EditingControlShowing (for bug fix of black dropped list)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DGV_ECS(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            var cmbBx = e.Control as DataGridViewComboBoxEditingControl; // or your combobox control
            if (cmbBx != null)
            {
                // Fix the black background on the drop down menu
                e.CellStyle.BackColor = Color.White;
            }
        }

        /// <summary>
        /// dataGridView DataError (for bug fix of "value is not valid")
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DGV_DE(object sender, DataGridViewDataErrorEventArgs e)
        {
            if (e.Exception.Message == "DaraGridViewComboBoxCell value is not valid.")
            {
                e.ThrowException = false;
            }
        }

        #endregion

        /// <summary>
        /// Starts experiment
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Start_btn_Click(object sender, EventArgs e)
        {
            if (statusesDictionary["MOOG"].BackColor != statusesColorsDictionary["GOOD"])
            {
                InfoPrinter.PrintTextOfType("WARNING", "Moog is still not ready");
                return;
            }

            if (statusesDictionary["CEDRUS"].BackColor != statusesColorsDictionary["GOOD"])
            {
                InfoPrinter.PrintTextOfType("WARNING", "Cedrus is still not ready");
                return;
            }

            InfoPrinter.PrintTextOfType("INFO", "Ready to start");
        }

        /// <summary>
        /// Prepare at least the first trial (is static params -> all trials)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Make_trials_btn_Click(object sender, EventArgs e)
        {
            // duration == time, sigma == const, magnitude == distance in cm, frequency == moog fps
            if (statusesDictionary["MOOG"].BackColor == statusesColorsDictionary["GOOD"]) TestSendPosFunc();
            //TrajectoryMaker.GetCDF(1, 3, 13, 1000);
        }

        private void TestSendPosFunc ()
        {
            var cdf_vector = TrajectoryMaker.CDF_VECTOR;
            int length = cdf_vector.Count;
            double distance = 0.15;
            double alpha_sin = 0.5;
            double alpha_cos = 0.866025404;

            double surge; double sway; double heave;



            // forward
            for (int i = 0; i < length; i++)
            {
                //InfoPrinter.PrintTextOfType("INFO", $"{(int)Math.Round(i / length)}");

                surge = cdf_vector[i] * distance * alpha_cos;
                sway = cdf_vector[i] * distance * alpha_sin;
                heave = -0.22077500;

                //InfoPrinter.PrintTextOfType("INFO", $"{surge}, {sway}, {heave}");
                //Thread.Sleep(10);


                _controlLoop.MoogSendPosition(surge, heave, sway, 0, 0, 0);
                //_controlLoop.MoogSendPosition(surge, heave, sway, 0, 0, 0);
            }

            Thread.Sleep(2000);

            //backward
            for (int i = length - 1; i > 0 ; i--)
            {
                //InfoPrinter.PrintTextOfType("INFO", $"{(int)Math.Round(i / length)}");

                surge = cdf_vector[i] * distance * alpha_cos;
                sway = cdf_vector[i] * distance * alpha_sin;
                heave = -0.22077500;

                //InfoPrinter.PrintTextOfType("WARNING", $"{surge}, {sway}, {heave}");
                //Thread.Sleep(10);


                _controlLoop.MoogSendPosition(surge, heave, sway, 0, 0, 0);
                //_controlLoop.MoogSendPosition(surge, heave, sway, 0, 0, 0);
            }

        }
    }
}