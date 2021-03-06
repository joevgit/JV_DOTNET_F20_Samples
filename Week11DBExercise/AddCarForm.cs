﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Week12DBExercise
{
    public partial class AddCarForm : Form
    {
        MainForm parentForm; // Reference to the Form that invoked us
        public AddCarForm(MainForm parent)
        {
            InitializeComponent();
            parentForm = parent;
        }

        private void addBtn_Click(object sender, EventArgs e)
        {
            if ((makeTB.Text.Trim() == "") || modelTB.Text.Trim() == "")
                return;  //log an error or popup a message

            try
            {
                // Build the SQL string to calculate the average Milege
                SqlCommand command = new SqlCommand("INSERT INTO CARS (make, model, modelyear)" +
                "VALUES (@ParamMake, @ParamModel, @ParamYear)", parentForm.Connection);
                SqlParameter make = new SqlParameter("@ParamMake", SqlDbType.VarChar);
                SqlParameter model = new SqlParameter("@ParamModel", SqlDbType.VarChar);
                SqlParameter modelYear = new SqlParameter("@ParamYear", SqlDbType.Int);
                make.Value = makeTB.Text;
                model.Value = modelTB.Text;
                modelYear.Value = yearNUD.Value;
                command.Parameters.Add(make);
                command.Parameters.Add(model);
                command.Parameters.Add(modelYear);
                // Create new SqlDataReader object and read data from the command.
                int result = command.ExecuteNonQuery();
                if (result != 1)
                    MessageBox.Show("No record was added to Database ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {

            }
        }

        private void closeBtn_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
