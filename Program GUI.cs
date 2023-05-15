using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MoogOcus
{
    public partial class GUI : Form
    {

        #region INITIALISATIONS
        /// <summary>
        /// The selected protocols path to view protocols.
        /// </summary>
        private string _protocolsDirPath;

        /// <summary>
        /// The curent selected protocol path.
        /// </summary>
        private string _protocolFilePath;

        /// <summary>
        /// The variables read from the xlsx protocol file.
        /// </summary>
        public Dictionary<string, Parameter> parameters;

        /// <summary>
        /// The dictionary of the dynamic allocated textboxes that are allocated each time the user choose different protocol.
        /// It saves the dynamic TextBox reference.
        /// The string represent the name of the varName concatinating with the attributename for each textbox.
        /// </summary>
        //private Dictionary<string, Control> _dynamicAllocatedTextBoxes;

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


        //private Dictionary<string, GuiComponent> _guiComponents;

        //private GuiHandler _guiHandler;

        /// <summary>
        /// A list that holds all the titles for the variables attribute to show in the title of the table.
        /// </summary>
        private List<Label> _titlesLabelsList;

        /*/// <summary>
        /// Holds the AcrossVectorValuesGenerator generator.
        /// </summary>
        private VaryingValuesGeneratorBase _acrossVectorValuesGenerator;

        /// <summary>
        /// Holds the StaticValuesGenerator generator.
        /// </summary>
        private StaticValuesGenerator _staticValuesGenerator;

        /// <summary>
        /// ControlLoop interface for doing the commands inserted in the gui.
        /// </summary>
        private ControlLoop _cntrlLoop;*/



        /// <summary>
        /// Locker for starting and stopping button to be enabled not both.
        /// </summary>
        private object _lockerStopStartButton;

        /// <summary>
        /// Locker for the pause and resume button to be enabled not both.
        /// </summary>
        private object _lockerPauseResumeButton;

        #endregion INITIALISATIONS

        public GUI(ref ExcelHandler excelHandler)
        {
            InitializeComponent();  // built in function

            InitializeCheckBoxesDictionary();
            InitializeTextBoxesDictionary();
            InitializeButtonsDictionary();

            parameters = new();

            _protocolFilePath = @"C:\Users\user\Documents\GitHub\V2_Levael\Protocols\HeadingDiscrimination_test.xlsx";
            excelHandler.ReadProtocolFile(_protocolFilePath, ref parameters);

            // test

            // Add columns + their names
            /*foreach (var prop in typeof(Parameter).GetProperties())
            {
                if (prop.Name == "description") { continue; };

                Parameters_table.Columns.Add(prop.Name, prop.Name);
            }

            // Add rows
            foreach (var parameter in parameters)
            {
                Parameters_table.Rows.Add();
            }

            // Fill table
            for (int r = 0; r < Parameters_table.RowCount; r++)
            {
                for (int c = 0; c < Parameters_table.ColumnCount; c++)
                {
                    Parameters_table.Rows[r].Cells[c].Value = $"{r},{c}";
                }
            }*/

            // Fill table
            /*foreach (var parameter in parameters)
            {
                foreach (var prop in typeof(Parameter).GetProperties())
                {
                    textboxesDictionary["INFO"].Text += $"{prop.Name}, {prop.GetValue(parameter.Value)}";
                    textboxesDictionary["INFO"].Text += "\r\n";
                }
                //Parameters_table.Columns.Add((parameter.Key).ToString(), (parameter.Key).ToString());
            }*/
            /*
            var test_list = parameters.ToList();
            var test_list2 = test_list[0].ToList();

            Parameters_table.Rows.Add();*/



            /*textboxesDictionary["INFO"].Text = Tools.Stringify(parameters);
            excelHandler.ReadProtocolFile(_protocolFilePath, ref parameters);
            textboxesDictionary["INFO"].Text = Tools.Stringify(parameters);*/


            //_guiHandler = new GuiHandler();


            //_guiComponents = _guiHandler.Initialize();      // GUI netto
            //Instructions_textbox.Text = _guiComponents["name1"].test;


            /*

            _excelLoader = excelLoader;
            _allVariables = new Variables();
            _allVariables._variablesDictionary = new Dictionary<string, Variable>();
            _dynamicAllocatedTextBoxes = new Dictionary<string, Control>();
            _acrossVectorValuesGenerator = DecideVaryinVectorsGeneratorByProtocolName();
            _staticValuesGenerator = new StaticValuesGenerator();
            InitializeTitleLabels();
            ShowVaryingControlsOptions(false);

            InitializeCheckBoxesDictionary();

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

        private void Form1_Load(object sender, EventArgs e)     //TODO: use overrided method OnLoad instead
        {
            
            //VariablesInstructions_section.Panel1Collapsed = true;   // hide parameters_section until choose of excel_protocol_file
            //Left_panel_section.SplitterDistance = Left_panel_section.Height - AllControlls_wrapper.Height - 10;     // resize of bottom section height. change later to function
        }

        #region DICTIONARISATION

        /// <summary>
        /// Initializes the checkboxes dictionary with names as key with the control as value.
        /// </summary>
        private void InitializeCheckBoxesDictionary()
        {
            checkboxesDictionary = new();
            checkboxesDictionary.Add("EEG", EEG_checkbox);
            checkboxesDictionary.Add("OCULUS", Oculus_checkbox);
            checkboxesDictionary.Add("GRAPH", Graph_checkbox);
            checkboxesDictionary.Add("INSTRUCTIONS", Instructions_checkbox);
        }

        /// <summary>
        /// Initializes the buttons dictionary with names as key with the control as value.
        /// </summary>
        private void InitializeButtonsDictionary()
        {
            buttonsDictionary = new();

            buttonsDictionary.Add("BROWSE_PROTOCOL_FOLDER", Browse_protocol_btn);
            buttonsDictionary.Add("SAVE_PROTOCOL", Save_protocol_btn);
            buttonsDictionary.Add("ENGAGE", Engage_btn);
            buttonsDictionary.Add("PARK", Park_btn);
            buttonsDictionary.Add("MAKE_TRIALS", Make_trials_btn);
            buttonsDictionary.Add("START_EXPERIMENT", Start_btn);
            buttonsDictionary.Add("STOP_EXPERIMENT", Stop_btn);
            buttonsDictionary.Add("START_TRIAL_CONTROLLER", Controller_start_btn);
            buttonsDictionary.Add("UP_CONTROLLER", Controller_up_btn);
            buttonsDictionary.Add("DOWN_CONTROLLER", Controller_down_btn);
            buttonsDictionary.Add("LEFT_CONTROLLER", Controller_left_btn);
            buttonsDictionary.Add("RIGHT_CONTROLLER", Controller_right_btn);
        }

        /// <summary>
        /// Initializes the checkboxes dictionary with names as key with the control as value.
        /// </summary>
        private void InitializeTextBoxesDictionary()
        {
            textboxesDictionary = new();

            textboxesDictionary.Add("INFO", Info_textbox);
            textboxesDictionary.Add("WARNINGS", Warning_textbox);
            textboxesDictionary.Add("INFORMATION", Instructions_textbox);
        }

        #endregion DICTIONARISATION

        #region EVENT LISTENERS

        private void Browse_protocol_btn_Click(object sender, EventArgs e)
        {
            if (FolderBrowserDialog.ShowDialog() == DialogResult.OK)
            {
                // temp for test
                Instructions_textbox.Text = FolderBrowserDialog.SelectedPath;
                //Instructions_textbox.Text = VariablesInstructions_section.GetType().ToString();
            }
        }

        private void Advanced_params_btn_Click(object sender, EventArgs e)
        {
            VariablesInstructions_section.Panel2Collapsed = true;   // hide instructions section
        }

        #endregion EVENT LISTENERS
    }
}