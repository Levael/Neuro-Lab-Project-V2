using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MOCU
{
    public class ControlLoop
    {
        #region FUNCTIONS INITIALIZATION

        public void MoogConnect()
        {
            try
            {
                MoogHandler.Connect();

                GUI.buttonsDictionary["CONNECT"].Enabled = false;
                GUI.statusesDictionary["MOOG"].BackColor = GUI.statusesColorsDictionary["GOOD"];
            } catch (Exception error)
            {
                GUI.statusesDictionary["MOOG"].BackColor = GUI.statusesColorsDictionary["ERROR"];
                //MessageBox.Show("Cannot connect to the robot - check if robot is conncted in listen mode and also not turned off", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            
        }

        public void MoogEngage()
        {
            MoogHandler.Engage();
        }

        public void MoogDisengage()
        {
            MoogHandler.Disengage();
        }

        public void MoogDisconnect()
        {
            MoogHandler.Disconnect();
        }

        public void MoogSendPosition(double surge, double heave, double lateral, double yaw = 0.0, double roll = 0.0, double pitch = 0.0)
        {
            // surge = forward-backward
            // heave = up-down
            // lateral (sway) = right-left

            // yaw = "figure skating twisting on the spot "
            // roll = planet and stars rotation
            // pitch = "Choji's Human Bullet Tank"

            MoogHandler.SendPosition(surge, heave, lateral, yaw, roll, pitch);
        }

        public void CedrusConnect()
        {
            try
            {
                CedrusHandler.Connect();
                GUI.statusesDictionary["CEDRUS"].BackColor = GUI.statusesColorsDictionary["GOOD"];
            }
            catch (Exception error)
            {
                GUI.statusesDictionary["CEDRUS"].BackColor = GUI.statusesColorsDictionary["ERROR"];
                //MessageBox.Show("Cannot connect to the controller", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

        }

        #endregion FUNCTIONS INITIALIZATION

        /*public void Start ()
        {
            try
            {
                Connect();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }*/
    }
}
