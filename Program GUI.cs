using Microsoft.Office.Interop.Excel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using Font = System.Drawing.Font;

namespace MoogOcus
{
    public partial class GUI : Form
    {

        #region INITIALISATIONS

        /// <summary>
        /// todo
        /// </summary>
        private ExcelHandler _excelHandler;

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
        public Dictionary<string, Control> textboxesDictionary;

        /// <summary>
        /// Dictionary that describes all buttons names in the gui as keys with their control as value.
        /// </summary>
        public Dictionary<string, Control> buttonsDictionary;

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
        }*/


        #endregion INITIALISATIONS

        public GUI()
        {
            //DoubleBuffered = true;

            _defaultProtocolsDirPath = @"C:\Users\user\Documents\GitHub\V2_Levael\Protocols\";
            _excelHandler = new();


            InitializeComponent();          // built in WinForms function

            DictionarilizeCheckBoxes();     // fill checkboxesDictionary (put controls to dictionary)
            DictionarilizeTextBoxes();      // fill textboxesDictionary
            DictionarilizeButtons();        // fill buttonsDictionary
            InitialiseCombobox();           // fill combobox with excel files from default protocol folder


            InfoPrinter.PrintInfo(textboxesDictionary, "Initialized some components (stam, for test)");


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

            //reset the selected direction to be empty.
            //_selectedHandRewardDirections = 0;

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

        /// <summary>
        /// Gui changes after full page loading
        /// </summary>
        private void Form1_Load(object sender, EventArgs e)     //TODO: use overrided method OnLoad instead
        {
            // at start hide parameters_section until user selects excel_protocol_file and shown only instruction section
            VariablesInstructions_section.Panel1Collapsed = true;
            VariablesInstructions_section.Panel2Collapsed = false;
            Left_panel_section.SplitterDistance = Left_panel_section.Height - AllControlls_wrapper.Height - 10;     // resize of bottom section height. change later to function

        }

        #region DIFFERENT FUNCTIONS
        private void InitializeInputParameters()
        {
            inputData = new();      // all input parameters in inputData.parameters

            _excelHandler.ReadProtocolFile(_protocolFilePath, inputData.parameters);     // fill 'parameters' with data
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
                InfoPrinter.PrintWarning(textboxesDictionary, "No excel files");
                return;
            }
        }

        #endregion DIFFERENT FUNCTIONS

        #region DICTIONARISATION

        /// <summary>
        /// Initializes the checkboxes dictionary with names as key with the control as value.
        /// </summary>
        private void DictionarilizeCheckBoxes()
        {
            checkboxesDictionary = new()
            {
                { "EEG", EEG_checkbox },
                { "OCULUS", Oculus_checkbox },
                { "GRAPH", Graph_checkbox },
                { "INSTRUCTIONS", Instructions_checkbox }
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
                { "ENGAGE", Engage_btn },
                { "PARK", Park_btn },
                { "MAKE_TRIALS", Make_trials_btn },
                { "START_EXPERIMENT", Start_btn },
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
                { "WARNINGS", Warning_textbox },
                { "INFORMATION", Instructions_textbox }
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
            if (Choose_Protocol_combobox.Items.Count == 0)
            {
                InfoPrinter.PrintWarning(textboxesDictionary, "No excel files");
                return;
            }

            _protocolFilePath = @$"{_protocolsDirPath}\{Choose_Protocol_combobox.SelectedItem}";

            InitializeInputParameters();     // fill inputData.parameters
            InitializeGuiParametersTable();  // fill gui table with inputData.parameters values

            // hide instruction_section and show in full height parameters_table_section
            VariablesInstructions_section.Panel2Collapsed = true;   // hide instructions section
            VariablesInstructions_section.Panel1Collapsed = false;
        }

        #endregion EVENT LISTENERS
    }
}