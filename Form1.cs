using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Reflection;

namespace DMV_GUI
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        List<MotorVehicle> arrazOfVehicle = new List<MotorVehicle> { };

        public static string fileName = "log-" + (DateTime.Now.ToString("dd-MM-yyyy")) + ".txt";

        private void FormLoad(object sender, EventArgs e)
        {
            VehicleTypeChange(null, null);

            if (!File.Exists(fileName))
            {
                FileStream fileStream = new FileStream(fileName, FileMode.Create, FileAccess.Write);
                fileStream.Close();
            }
        }

        private void VehicleTypeChange(object sender, EventArgs e) //Method for radio button selector. Displays required fileds for diferent types of motor Vehicles
        {
            if (rbTruck.Checked)
            {
                customLabel01.Visible = customTb01.Visible = true;
                customLabel02.Visible = customTb02.Visible = customLabel03.Visible = rbYes.Visible = rbNo.Visible = false;
                customLabel01.Text = "maximum weight";
            }
            else if (rbBus.Checked)
            {
                customLabel01.Visible = customTb01.Visible = true;
                customLabel02.Visible = customTb02.Visible = customLabel03.Visible = rbYes.Visible = rbNo.Visible = false;
                customLabel01.Text = "Company name";
            }
            else if (rbCar.Checked)
            {
                customLabel01.Visible = customTb01.Visible = customLabel02.Visible = customLabel03.Visible = customTb02.Visible = rbYes.Visible = rbNo.Visible = true;
                customLabel01.Text = "Car Color";
                customLabel02.Text = "Number of airbags";
                customLabel03.Text = "Does the car have AC?";
            }
            else if (rbTaxi.Checked)
            {
                customLabel01.Visible = customTb01.Visible = customLabel02.Visible = customLabel03.Visible = customTb02.Visible = rbYes.Visible = rbNo.Visible = customLabel04.Visible = rbYes2.Visible = rbNo2.Visible = true;
                customLabel01.Text = "Car Color";
                customLabel02.Text = "Number of airbags";
                customLabel03.Text = "Cab has AC?";
                customLabel04.Text = "Driver has licence?";
            }
        }

        private void RegisterVehicleClick(object sender, EventArgs e)
        {
            MotorVehicle mv = null;
            if (rbTruck.Checked)
            {
                mv = new Truck(tbVIN.Text, tbMake.Text, tbModel.Text, (int)NoOfWheels.Value, (int)NoOfSeats.Value, datePicker.Value, Convert.ToDouble(customTb01.Text));
            }
            else if (rbBus.Checked)
            {
                mv = new Bus(tbVIN.Text, tbMake.Text, tbModel.Text, (int)NoOfWheels.Value, (int)NoOfSeats.Value, datePicker.Value, customTb01.Text);                
            }
            else if (rbCar.Checked)
            {
                mv = new Car(tbVIN.Text, tbMake.Text, tbModel.Text, (int)NoOfWheels.Value, (int)NoOfSeats.Value, datePicker.Value, customTb01.Text, rbYes.Checked, Convert.ToInt32(customTb02.Text));                
            }
            else if (rbTaxi.Checked)
            {
                mv = new Taxi(tbVIN.Text, tbMake.Text, tbModel.Text, (int)NoOfWheels.Value, (int)NoOfSeats.Value, datePicker.Value, customTb01.Text, rbYes.Checked, Convert.ToInt32(customTb02.Text), rbYes2.Checked);
            }

            arrazOfVehicle.Add(mv);
            rtLog.Clear();

            foreach(MotorVehicle mVeh in arrazOfVehicle)
            {
                if (mVeh != null)
                {
                    rtLog.AppendText(mVeh.show() + "\n");
                    FileStream file = new FileStream(fileName, FileMode.Append, FileAccess.Write);
                    StreamWriter writer = new StreamWriter(file);
                    writer.WriteLine(mVeh.show());
                    writer.Close();
                    file.Close();                    
                }
            }
        }
        
        private void ShowLastVehicleFromFile(object sender, EventArgs e)
        {
            FileStream fileStream = new FileStream(fileName, FileMode.Open, FileAccess.Read);
            StreamReader streamReader = new StreamReader(fileStream);
            
            string allFileLines;
            string[] currentLine = new String[20];
            int lineCounter = 0;
            
            while((allFileLines = streamReader.ReadLine()) != null) {
                        try
                        {
                            currentLine[lineCounter++] = allFileLines;
                        }
                        catch
                        {
                            break;
                        }
                    }

            rtLog.AppendText(lineCounter.ToString() + ": " + currentLine[lineCounter-1] + "\n\n");
            streamReader.Close();
            fileStream.Close();
        }        
    }
}
