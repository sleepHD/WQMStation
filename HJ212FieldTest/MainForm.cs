using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Windows.Forms;
using WQMStation.HJ212.Data;
using WQMStation.HJ212.Device;

namespace HJ212FieldTest
{
    public partial class MainForm : Form
    {
        private HJ212Field _hj212Field;
        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            var client = new TcpClient("192.168.56.1", 5003);
            _hj212Field = HJ212Field.CreateTCPField("21", "123456", "12345678901234", client);
        }

        private void btnReportHData_Click(object sender, EventArgs e)
        {
            var varList = new List<VarItem>();
            var v1 = new VarItem();
            v1.Code = "311";
            v1.Value = 7.15;
            v1.Flag = "N";
            v1.Format = "0.00";
            varList.Add(v1);
            var v2 = new VarItem();
            v2.Code = "311";
            v2.Value = 7.15;
            v2.Flag = "N";
            v2.Format = "0.00";
            varList.Add(v2);
            var v3 = new VarItem();
            v3.Code = "311";
            v3.Value = 7.15;
            v3.Flag = "N";
            v3.Format = "0.00";
            varList.Add(v3);
            var v4 = new VarItem();
            v4.Code = "311";
            v4.Value = 7.15;
            v4.Flag = "N";
            v4.Format = "0.00";
            varList.Add(v4);
            var v5 = new VarItem();
            v5.Code = "311";
            v5.Value = 7.15;
            v5.Flag = "N";
            v5.Format = "0.00";
            varList.Add(v5);

            var data = new HourData(DateTime.Now, varList.ToArray());
            try
            {
                _hj212Field.ReportHourData(data);
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
    }
}
