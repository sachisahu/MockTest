using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MockTest
{
    public partial class Form1 : Form
    {
        MockTestDBEntities db = new MockTestDBEntities();
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            loadDataCbCharity();
            loadData();
            int charityIDGet = (int)cbCharity.SelectedValue;
            Console.WriteLine(charityIDGet + "");
        }
        //Load Data to Data grid
        private void loadData()
        {

            int charityIDGet = (int)cbCharity.SelectedValue;

            Console.WriteLine(charityIDGet + "");

            DateTime regnDateMin = dateMinDate.Value.Date;
            DateTime regnDateMax = dateMaxDate.Value.Date;
            Console.WriteLine(regnDateMin + " ; " + regnDateMax);
            var fdata = db.Registrations.Where(x => (x.Charity.CharityId == charityIDGet || charityIDGet == 0) &&
                                                    (x.Runner.Email.Contains(txtEmail.Text) || txtEmail.Text == "") /*&&
                                                    (x.RegistrationDateTime.Date >= regnDateMin || dateMinDate == null) &&
                                                    (x.RegistrationDateTime <= regnDateMax || dateMaxDate == null)*/)
                .Select(c => new
                {
                    RunnersEmail = c.Runner.Email,
                    RegnDate = c.RegistrationDateTime,
                    CharityName = c.Charity.CharityName,
                    SponTragate = c.SponsorshipTarget
                }).ToList();

            dgvData.DataSource = fdata;

            decimal sum = dgvData.Rows.Cast<DataGridViewRow>().Sum(f => Convert.ToDecimal(f.Cells[3].Value));
            txtTotal.Text = sum+"";



            dgvData.Columns["RunnersEmail"].HeaderText = "Runners Email";
            dgvData.Columns["RegnDate"].HeaderText = "Registration Date Time";
            dgvData.Columns["CharityName"].HeaderText = "Charity Name";
            dgvData.Columns["SponTragate"].HeaderText = "Sponsorship target";

        }
        //load Data to charity Cb Box
        private void loadDataCbCharity()
        {
            var cbCharityList = db.Charities.ToList();
            cbCharityList.Insert(0,new Charity()
            {
                CharityId = 0,
                CharityName = "All",
                CharityDescription = "",
                CharityLogo = ""
            });
            

            cbCharity.DataSource = cbCharityList;
            cbCharity.DisplayMember = "CharityName";
            cbCharity.ValueMember = "CharityId";

        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            loadData();
        }

        private void cbCharity_SelectedIndexChanged(object sender, EventArgs e)
        {
            //loadData();
        }

        private void txtEmail_TextChanged(object sender, EventArgs e)
        {
            loadData();
        }

        private void cbCharity_SelectedValueChanged(object sender, EventArgs e)
        {
            //loadData();
        }
    }
}
