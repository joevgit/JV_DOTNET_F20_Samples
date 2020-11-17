﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Week10_Collections
{
    public partial class Form1 : Form
    {
        private List<Car> carList = new List<Car>();
        private Stack<Car> carStack = new Stack<Car>();
        private Queue<Car> carQueue = new Queue<Car>();
        private Dictionary<int, Car> carDictionary = new Dictionary<int, Car>();

        // Dictionary<string, Student>
        // Dictionary<string, string> 
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "Text file (*.txt)|*.txt|All files (*.*)|*.*";
            openFileDialog1.InitialDirectory = Application.StartupPath;
            openFileDialog1.Title = "Select a car text file to open";
        }

        private void loadBtn_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() != DialogResult.OK)
            {
                MessageBox.Show("You did not select a file");
                return;
            }

            try
            {
                StreamReader sr = File.OpenText(openFileDialog1.FileName);
                while (!sr.EndOfStream)
                {
                    string carInfo = sr.ReadLine();
                    if (!ParseCar(carInfo, out Car car))
                    {
                        MessageBox.Show("Error parsing file");
                        return;
                    }

                    // add new car to collections
                    carList.Add(car);
                    carStack.Push(car);
                    carQueue.Enqueue(car);
                    carDictionary.Add(car.Id, car);
                }

            }
            catch (Exception ex)
            {

            }

            // Update listbox
            carsLB.Items.Clear();
            foreach (Car c in carDictionary.Values)
                carsLB.Items.Add(c);
        }

        private bool ParseCar(string carInfo, out Car car)
        {
            // parse the values
            car = null;
            string[] values = carInfo.Split(',');
            if (values.Length != 4)
                return false;

            string make = values[0];
            string model = values[1];
            if (double.TryParse(values[2], out double fuelAmount) == false)
            {
                MessageBox.Show("Failed to parse the fuelAmount from the file");
                return false;
            }
            if (double.TryParse(values[3], out double mileage) == false)
            {
                MessageBox.Show("Failed to parse the mileage from the file");
                return false;
            }

            // create a new car
            car = new Car(make, model, fuelAmount, mileage);
            return true;
            //myCarList.Add(new Car(make, model, fuelAmount, mileage));
        }
    }
}
