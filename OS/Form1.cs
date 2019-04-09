/*
    Ali Tourani (https://github.com/alitourani/)
    Operating System - Highest Response Ratio Next (HRRN) Algorithm Simulation
*/

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;

namespace OS
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            MyProc[0].arrival = MyProc[1].arrival = MyProc[2].arrival = 0;
            InitializeComponent();
        }

    const int MAX1 = 4;
    public struct Proc
    {
        public int id;
        public int arrival;
        public int burst;
        public int start;
        public int rem;
        public int wait;
        public int finish;
        public string NewAdd ;
        public string Insert_Time;
        public int turnaround;
        public float ratio;
    };
    public Proc[] MyProc = new Proc[MAX1];
    public int no;
    public int[] Pressed = {0,0,0};
    public string[] timer = new string[3];
    public string[] Add = {"","",""};
    public string[] Output = { "", "", "" };
    public bool mode = true;
    public int[] sort = {0,0,0};
    public int[] tmp_nmbr = { 0, 0, 0 };
                
                
    //_____________________________________________
    int chkprocess(int s){
        int i;
            for (i = 0; i < s; i++)
            {
                if (MyProc[i].rem != 0)
                    return 1;
            }
            return 0;
        }

     //_____________________________________________
     int nextprocess()
        {
            int min, l = 0, i, r;
            min = 0;
            for (i = 0; i < no; i++)
            {
                if (MyProc[i].rem != 0)
                {
                    r = ((MyProc[i].wait + MyProc[i].burst) / MyProc[i].burst);
                    if (r > min)
                    {
                        min = r;
                        l = i;
                    }
                }
            }
            return l;
        }

     void Burst_Load(string Addr, int i)
     {
         if (Addr == "") {
             MyProc[i].burst = MyProc[i].arrival;
             Output[i] = "";
         }
         else
         {
             int counter = 0;
             string line="";
             Addr = Addr.Replace("\\","\\\\");
             System.IO.StreamReader file = new System.IO.StreamReader(Addr);
             while ((line = file.ReadLine()) != null)
             {
                 Output[i] += line;
                 counter++;
             }
             file.Close();
             MyProc[i].burst = MyProc[i].arrival + counter;
         }
     }


        private void button1_Click_1(object sender, EventArgs e)
        {
            int i,j,time,n=3;
            time=0;
            
            if (mode == true)
            {
                button1.Text = "Restart";
                button1.ForeColor = Color.Red;
                button2.Enabled = button3.Enabled = button4.Enabled = false;
                mode = false;
                Proc temp;
                int x;

                for (i = 0; i < n; i++) { 
                    if (Pressed[i] == 0)
                    {
                        MyProc[i].arrival = 0;
                        MyProc[i].burst = 0;
                    }
                }

                for (i = 0; i < n; i++)
                {
                    MyProc[i].id = i;
                    Burst_Load(Add[i], i);
                    MyProc[i].rem = MyProc[i].burst;
                    
                }

                for (i = 0; i <= 2; i++)
                    if (Pressed[i] == 0) {
                        Add[i] = "";
                    }

                for (i = 0; i <= 2; i++)
                {
                    MyProc[i].NewAdd = Output[i];
                    MyProc[i].Insert_Time = timer[i];
                }
                    
                    
                // Sorting
                for (i = 0; i <= n-1; i++)
                {
                    for (j = i + 1; j < n; j++)
                    {
                        if (MyProc[i].arrival > MyProc[j].arrival)
                        {
                            temp = MyProc[i];
                            MyProc[i] = MyProc[j];
                            MyProc[j] = temp;
                        }
                    }
                }

                no = 0;
                j = 1;

                MyProc[1].start = MyProc[1].arrival;
                while (chkprocess(2) == 1)
                {
                    if (MyProc[no + 1].arrival == time)
                        no++;

                    if (MyProc[j].rem != 0)
                    {
                        MyProc[j].rem--;
                        for (i = 1; i <= no; i++)
                        {
                            if ((i != j) && (MyProc[i].rem != 0))
                                MyProc[i].wait++;
                        }
                    }

                    else
                    {
                        MyProc[j].finish = time;
                        j = nextprocess();
                        time--;
                        MyProc[j].start = time + 1;
                    }
                    time++;
                }
                MyProc[j].finish = time;

                for (i = 0; i < 3; i++)
                {
                    MyProc[i].turnaround = MyProc[i].wait + MyProc[i].burst;
                    MyProc[i].ratio = (float)MyProc[i].turnaround / (float)MyProc[i].burst;
                    textBox1.Text += "*File Number : ";
                    textBox1.Text += (MyProc[i].id + 1);
                    textBox1.Text += Environment.NewLine; 
                    textBox1.Text += "*File's Inside : ";
                    textBox1.Text += Environment.NewLine;
                    textBox1.Text += MyProc[i].NewAdd;
                    textBox1.Text += Environment.NewLine;
                    textBox1.Text += "*Insert Time : ";
                    textBox1.Text += Environment.NewLine;
                    textBox1.Text += MyProc[i].Insert_Time;
                    textBox1.Text += Environment.NewLine; 
                    textBox1.Text += Environment.NewLine;
                    textBox1.Text += "*Process Time (millisecond): ";
                    textBox1.Text += (MyProc[i].burst - MyProc[i].arrival);
                    textBox1.Text += Environment.NewLine;
                    textBox1.Text += "*Finish Time : ";
                    textBox1.Text += MyProc[i].finish;
                    textBox1.Text += Environment.NewLine;
                    textBox1.Text += "*Remain Time : ";
                    textBox1.Text += MyProc[i].rem; 
                    textBox1.Text += Environment.NewLine;
                    textBox1.Text += "*Wait Time : ";
                    textBox1.Text += MyProc[i].wait; 
                    textBox1.Text += Environment.NewLine;
                    //textBox1.Text += "*Turn Around Time : ";
                    //textBox1.Text += MyProc[i].turnaround;
                    //textBox1.Text += Environment.NewLine;
                    textBox1.Text += "*Ratio : ";
                    textBox1.Text += MyProc[i].ratio;
                    textBox1.Text += Environment.NewLine;
                    textBox1.Text += "___________________";
                    textBox1.Text += Environment.NewLine;
                }
                no = 0;
                textBox1.Text += "End of HRRN Management";
            }
            else if (mode == false)
            {
                button1.Text = "Start";
                button1.ForeColor = Color.Green;
                mode = true;
                textBox1.Text = textBox4.Text = textBox5.Text = textBox6.Text = "";
                Pressed[0] = Pressed[1] = Pressed[2] = 0;
                button2.Enabled = button3.Enabled = button4.Enabled = true;
                MyProc[0].arrival = MyProc[1].arrival = MyProc[2].arrival = 0;
                MyProc[0].burst = MyProc[1].burst = MyProc[2].burst = 0;
                timer[0] = timer[1] = timer[2] = "";
                sort[0] = sort[1] = sort[2] = 0;
                Add[0] = Add[1] = Add[2] = "";
                Output[0] = Output[1] = Output[2] = "";
                time = 0; no = 0;
            }
       
            Pressed[0] = Pressed[1] = Pressed[2] = 0;
	
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Pressed[0] = 1;
            int tmp;
            // Get File
            OpenFileDialog fdlg = new OpenFileDialog();
            fdlg.Title = "Choose Your File";
            fdlg.InitialDirectory = @"";
            fdlg.Filter = "Text files (*.txt*)|*.*";
            fdlg.RestoreDirectory = true;
            if (fdlg.ShowDialog() == DialogResult.OK)
            {
                timer[0] = DateTime.Now.ToString();
                int Mnow = DateTime.Now.Minute;
                int Snow = DateTime.Now.Second;
                textBox4.Text = fdlg.FileName;
                Add[0] = textBox4.Text;
                tmp = Mnow * 60 + Snow;
                MyProc[0].arrival = tmp;
            }
            
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Pressed[1] = 1;
            // Get File
            OpenFileDialog fdlg = new OpenFileDialog();
            fdlg.Title = "Choose Your File";
            fdlg.InitialDirectory = @"";
            fdlg.Filter = "Text files (*.txt*)|*.*";
            fdlg.RestoreDirectory = true;
            if (fdlg.ShowDialog() == DialogResult.OK)
            {
                timer[1] = DateTime.Now.ToString();
                int Mnow = DateTime.Now.Minute;
                int Snow = DateTime.Now.Second;
                textBox5.Text = fdlg.FileName;
                Add[1] = textBox5.Text;
                MyProc[1].arrival = Mnow * 60 + Snow;
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Pressed[2] = 1;
            OpenFileDialog fdlg = new OpenFileDialog();
            fdlg.Title = "Choose Your File";
            fdlg.InitialDirectory = @"";
            fdlg.Filter = "Text files (*.txt*)|*.*";
            fdlg.RestoreDirectory = true;
            if (fdlg.ShowDialog() == DialogResult.OK)
            {
                timer[2] = DateTime.Now.ToString();
                int Mnow = DateTime.Now.Minute;
                int Snow = DateTime.Now.Second;
                textBox6.Text = fdlg.FileName;
                Add[2] = textBox6.Text;
                MyProc[2].arrival = Mnow * 60 + Snow;
            }
        }

    }
}

