using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Client.ServiceReference1;

namespace Client
{
    public partial class Form1 : Form
    {
        private StringServiceClient client;
        public Form1()
        {
            client = new StringServiceClient();
            ClientRegistration();
            InitializeComponent();
        }

        private async void ClientRegistration()
        {
            try
            {
                await client.RegisterClientAsync();
            } catch (Exception e)
            {
                Console.Write("Server service wasn't found");
            }
        }

        private async void DefineClass(object sender, EventArgs e)
        {
            string set = textBox2.Text;
            var response = await client.DefineClassAsync(set);
            label4.Text = response;
            await client.IncreaseRequestsQuantityAsync();
        }

        private async void button3_Click(object sender, EventArgs e)
        {
            DatasetInfo response = await client.GetDatasetInformationAsync();
            cols.Text = response.ColumnNumber.ToString();
            rows.Text = response.RowsNumber.ToString();

            string classDistribution = "";

            foreach (var keyValuePair in response.ClassDistribution)
            {
                classDistribution += "Class " + keyValuePair.Key + " includes " + keyValuePair.Value + " instances\n";
            }

            classes.Text = classDistribution;
            await client.IncreaseRequestsQuantityAsync();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private async void AddString(object sender, EventArgs e)
        {
            string addingSet = textBox1.Text;
            string response = await client.AddStringAsync(addingSet);
            label3.Text = response;
            await client.IncreaseRequestsQuantityAsync();
        }

        private async void button4_Click(object sender, EventArgs e)
        {
            var response = await client.GetPrivateInformationAsync();
            label10.Text = response.Clients.ToString();
            label11.Text = response.Requests.ToString();
            await client.IncreaseRequestsQuantityAsync();
        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private async void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            await client.UnregisterClientAsync();
        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }
    }
}
