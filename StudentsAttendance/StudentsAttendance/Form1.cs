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

namespace StudentsAttendance
{
    public partial class Form1 : Form
    {
        
        private List<student> StudData;
        private List<employee> EmployeeData;

        class course
        {

            public string code;
            public string name;
            public string AcadmicYear;
            public int attendance;
            public int absence;
        }

        class employee

        {
            private string name;
            private string password;

            //Function to read employee data from text files & returns them as a list:

            public List<employee> Data()
            {
                List<employee> employeelist = new List<employee>();
                FileStream FS = new FileStream("employees.txt", FileMode.Open);
                StreamReader SR = new StreamReader(FS);
                string tmp;
                string[] temp;
                int i = 0;
                while (SR.Peek() != -1)
                {


                    tmp = SR.ReadLine();
                    temp = tmp.Split('@');
                    employeelist.Add(new employee());
                    employeelist[i].name = temp[0];
                    employeelist[i].password = temp[1];
                    i++;



                }

                FS.Close();
                return employeelist;

            }

            //Function to check employee login credentials:

            public void login(Form1 frm, List<employee> employeeData)
            {

                string NameCheck = frm.EmployeeNameText.Text;
                string PwCheck = frm.EmployeePwText.Text;

                bool correct = false;


                List<employee> employeelist = employeeData;

                for (int y = 0; y < employeelist.Count; y++)
                {



                    if (employeelist[y].name == NameCheck && employeelist[y].password == PwCheck)
                    {
                        correct = true;
                        break;
                    }
                }
                
                if (correct == true)
                {
                    
                    frm.StartPanel.Show();
                    frm.EmployeeDataPanel.Show();
                    frm.ChangeAtt_Button.Show();
                    frm.ViewRecord_Button.Show();
                    frm.info1Label.Show();
                }
                else
                {
                    MessageBox.Show("Wrong Username & Password");
                }
            }

            //Function to show report of all students whose absence excceded the limit:
            public void ReportAllStudents(Form1 frm, List <student> StudData)
            {
                
                List<student> studentList = StudData;
                        DataTable table = new DataTable();
                        table.Columns.Add("Student ID");
                        table.Columns.Add("Student Name");
                        table.Columns.Add("Course Name");
                for (int i = 0; i < studentList.Count; i++)
                {
                    if (studentList[i].course1.absence >= 3)
                    {
                        DataRow dr = table.NewRow();
                        dr["Student ID"] = studentList[i].ID;
                        dr["Student Name"] = studentList[i].name;
                        dr["Course Name"] = studentList[i].course1.name;

                        table.Rows.Add(dr);
                        var bindingsource = new BindingSource();
                        bindingsource.DataSource = table;
                        frm.dataGridView1.DataSource = bindingsource;
                        bindingsource.ResetBindings(true);


                    }
                    if (studentList[i].course2.absence >= 3)
                    {
                        
                        
                        DataRow dr = table.NewRow();
                        dr["Student ID"] = studentList[i].ID;
                        dr["Student Name"] = studentList[i].name;
                        dr["Course Name"] = studentList[i].course2.name;

                        table.Rows.Add(dr);
                        var bindingsource = new BindingSource();
                        bindingsource.DataSource = table;
                        frm.dataGridView1.DataSource = bindingsource;
                        bindingsource.ResetBindings(true);


                    }
                    if (studentList[i].course3.absence >= 3)
                    {
                        
                        
                        DataRow dr = table.NewRow();
                        dr["Student ID"] = studentList[i].ID;
                        dr["Student Name"] = studentList[i].name;
                        dr["Course Name"] = studentList[i].course3.name;

                        table.Rows.Add(dr);
                        var bindingsource = new BindingSource();
                        bindingsource.DataSource = table;
                        frm.dataGridView1.DataSource = bindingsource;
                        bindingsource.ResetBindings(true);


                    }
                }


            }

            public List<student> recordattendance(Form1 frm, List<student> StudData)
            {

                string coursename = frm.courseCombo.Text;
                bool correct = false;
                List<student> studentlist = StudData;

                for (int i = 0; i < studentlist.Count; i++)
                {
                    if (studentlist[i].course1.name == coursename && studentlist[i].name == frm.studentCombo.Text)
                    {
                        
                        
                        
                        if (frm.PresentRadio.Checked)
                            studentlist[i].course1.attendance++;
                        if (frm.AbsentRadio.Checked)
                            studentlist[i].course1.absence++;
                        correct = true;
                        MessageBox.Show("Saved!");
                        break;

                    }

                    if (studentlist[i].course2.name == coursename && studentlist[i].name == frm.studentCombo.Text)
                    {
                        


                        if (frm.PresentRadio.Checked)
                            studentlist[i].course2.attendance++;
                        if (frm.AbsentRadio.Checked)
                            studentlist[i].course2.absence++;
                        correct = true;
                        MessageBox.Show("Saved!");
                        break;
                    }

                    if (studentlist[i].course3.name == coursename && studentlist[i].name == frm.studentCombo.Text)
                    {
                        


                        if (frm.PresentRadio.Checked)
                            studentlist[i].course3.attendance++;
                        if (frm.AbsentRadio.Checked)
                            studentlist[i].course3.absence++;
                        correct = true;
                        MessageBox.Show("Saved!");
                        break;
                    }
                    

                }
                if (correct == false)
                {
                    MessageBox.Show("incorrect course for the selected student");
                }
                return studentlist;

            }


            public void save(Form1 frm, List <student> StudList)
            {
                
                employee employ = new employee();
                List<student> studentlist = employ.recordattendance(frm,StudList);
                FileStream FS = new FileStream("students.txt", FileMode.Create);
                StreamWriter SW = new StreamWriter(FS);

                

                for (int x = 0; x < studentlist.Count; x++)
                {
                    SW.Write(studentlist[x].ID + '@');
                    SW.Write(studentlist[x].name + '@');
                    SW.Write(studentlist[x].AcademicYear + '@');
                    SW.Write(studentlist[x].course1.code + '@');
                    SW.Write(studentlist[x].course1.name + '@');
                    SW.Write(studentlist[x].course1.AcadmicYear + '@');
                    SW.Write(studentlist[x].course1.attendance);
                    SW.Write('@');
                    SW.Write(studentlist[x].course1.absence);
                    SW.Write('@');
                    SW.Write(studentlist[x].course2.code + '@');
                    SW.Write(studentlist[x].course2.name + '@');
                    SW.Write(studentlist[x].course2.AcadmicYear + '@');
                    SW.Write(studentlist[x].course2.attendance);
                    SW.Write('@');
                    SW.Write(studentlist[x].course2.absence);
                    SW.Write('@');
                    SW.Write(studentlist[x].course3.code + '@');
                    SW.Write(studentlist[x].course3.name + '@');
                    SW.Write(studentlist[x].course3.AcadmicYear + '@');
                    SW.Write(studentlist[x].course3.attendance);
                    SW.Write('@');
                    SW.Write(studentlist[x].course3.absence);

                    SW.WriteLine();



                }
                SW.Close();


            }
        }

        class student
        {

            public string name;
            public string ID;
            public string AcademicYear;

            public course course1 = new course();
            public course course2 = new course();
            public course course3 = new course();

           public List<student> Data()
            {
                List<student> studentlist = new List<student>();
                FileStream f = new FileStream("students.txt", FileMode.Open);
                StreamReader s = new StreamReader(f);
                string tmp;
                string[] temp;
                int i = 0;
                while (s.Peek() != -1)
                {


                    tmp = s.ReadLine();
                    temp = tmp.Split('@');
                    studentlist.Add(new student());
                    studentlist[i].ID = temp[0];
                    studentlist[i].name = temp[1];
                    studentlist[i].AcademicYear = temp[2];

                    studentlist[i].course1.code = temp[3];
                    studentlist[i].course1.name = temp[4];
                    studentlist[i].course1.AcadmicYear = temp[5];
                    studentlist[i].course1.attendance = Int32.Parse(temp[6]);
                    studentlist[i].course1.absence = Int32.Parse(temp[7]);

                    studentlist[i].course2.code = temp[8];
                    studentlist[i].course2.name = temp[9];
                    studentlist[i].course2.AcadmicYear = temp[10];
                    studentlist[i].course2.attendance = Int32.Parse(temp[11]);
                    studentlist[i].course2.absence = Int32.Parse(temp[12]);

                    studentlist[i].course3.code = temp[13];
                    studentlist[i].course3.name = temp[14];
                    studentlist[i].course3.AcadmicYear = temp[15];
                    studentlist[i].course3.attendance = Int32.Parse(temp[16]);
                    studentlist[i].course3.absence = Int32.Parse(temp[17]);


                    i++;

                }
                s.Close();
                f.Close();
                return studentlist;
            }

            public List<student> signup(Form1 frm, List<student> StudData)
            {
                student Stud = new student();

                FileStream f = new FileStream("students.txt", FileMode.Append);
                StreamWriter w = new StreamWriter(f);


                Stud.ID = frm.StudIDSignText.Text;
                w.Write(Stud.ID + '@');

                Stud.name = frm.StudNameSignText.Text;
                w.Write(Stud.name + '@');

                if (frm.radioButton1.Checked)
                {
                    Stud.AcademicYear = "1";
                }
                if (frm.radioButton2.Checked)
                {
                    Stud.AcademicYear = "2";
                }
                if (frm.radioButton3.Checked)
                {
                    Stud.AcademicYear = "3";
                }
                if (frm.radioButton4.Checked)
                {
                    Stud.AcademicYear = "4";
                }

                w.Write(Stud.AcademicYear + '@');
                Stud.course1.attendance = 0;
                Stud.course2.attendance = 0;
                Stud.course3.attendance = 0;
                Stud.course1.absence = 0;
                Stud.course2.absence = 0;
                Stud.course3.absence = 0;
                Stud.course1.AcadmicYear = Stud.AcademicYear;
                Stud.course2.AcadmicYear = Stud.AcademicYear;
                Stud.course3.AcadmicYear = Stud.AcademicYear;

                if (Stud.AcademicYear == "1")
                {
                    Stud.course1.code = "S1";
                    Stud.course1.name = "S.P";
                    Stud.course2.code = "P1";
                    Stud.course2.name = "Physics";
                    Stud.course3.code = "M1";
                    Stud.course3.name = "Math 1";

                }
                else if (Stud.AcademicYear == "2")
                {
                    Stud.course1.code = "D2";
                    Stud.course1.name = "D.S";
                    Stud.course2.code = "F2";
                    Stud.course2.name = "F.O";
                    Stud.course3.code = "M2";
                    Stud.course3.name = "Math 3";

                }
                else if (Stud.AcademicYear == "3")
                {
                    Stud.course1.code = "A3";
                    Stud.course1.name = "Algo";
                    Stud.course2.code = "AI3";
                    Stud.course2.name = "A.I";
                    Stud.course3.code = "OS3";
                    Stud.course3.name = "O.S";

                }
                else if (Stud.AcademicYear == "4")
                {
                    Stud.course1.code = "W4";
                    Stud.course1.name = "Web Eng";
                    Stud.course2.code = "T4";
                    Stud.course2.name = "Testing";
                    Stud.course3.code = "SD4";
                    Stud.course3.name = "Design";

                }
                w.Write(Stud.course1.code + '@');
                w.Write(Stud.course1.name + '@');
                w.Write(Stud.course1.AcadmicYear + '@');
                w.Write(Stud.course1.attendance);
                w.Write('@');
                w.Write(Stud.course1.absence);
                w.Write('@');
                w.Write(Stud.course2.code + '@');
                w.Write(Stud.course2.name + '@');
                w.Write(Stud.course2.AcadmicYear + '@');
                w.Write(Stud.course2.attendance);
                w.Write('@');
                w.Write(Stud.course2.absence);
                w.Write('@');
                w.Write(Stud.course3.code + '@');
                w.Write(Stud.course3.name + '@');
                w.Write(Stud.course3.AcadmicYear + '@');
                w.Write(Stud.course3.attendance);
                w.Write('@');
                w.Write(Stud.course3.absence);

                w.WriteLine();


                w.Close();
                StudData.Add(Stud);
                return StudData;
            }

        

        public student Login(Form1 frm, List<student> StudData)
            {

                string IDcheck = frm.StudID_Login.Text;
                string namecheck = frm.StudName_Login.Text;

                bool correct = false; //Used to determine if Login indices are right or wrong.

                int rightstudent=0;

                
                List<student> studentlist = StudData;

                for (int y = 0; y < studentlist.Count; y++)
                {

                    if (studentlist[y].ID == IDcheck && studentlist[y].name == namecheck)
                    {
                        correct = true;
                        rightstudent = y;
                        break;

                    }
                   

                }

                if (correct == true)
                {
                    
                    frm.HideStudentPanel.Hide();
                    return studentlist[rightstudent];
                }
                else
                {
                    MessageBox.Show("Wrong ID & Name, Try again!");
                    frm.HideStudentPanel.Show();
                    return studentlist[rightstudent];
                }

            }


        }

public Form1()
        {
            InitializeComponent();

            student x = new student();
            StudData = x.Data();

            employee e = new employee();
            EmployeeData = e.Data();

            List<string> coursesList = new List<string>();
             
            for (int i = 0; i < StudData.Count;i++ )
            {

                coursesList.Add(StudData[i].course1.name);
                coursesList.Add(StudData[i].course2.name);
                coursesList.Add(StudData[i].course3.name);


            }

            coursesList = coursesList.Distinct().ToList();


            for (int i = 0; i<coursesList.Count; i++)
            {
                courseCombo.Items.Add(coursesList[i]);
                
            }


            

            this.courseCombo.SelectedIndexChanged +=
            new System.EventHandler(StudentCombo_SelectedIndexChanged);




        }

        public void button2_Click(object sender, EventArgs e)
        {
            student x = new student();
            x = x.Login(this,StudData);

            


            //Assigning Student Data:
            NameLabel.Text = x.name;
            IDLabel.Text = x.ID;
            YearLabel.Text = x.AcademicYear;

            NameLabel.Show();
            IDLabel.Show();
            YearLabel.Show();

            //First Subject:
            Subject1.Text = x.course1.name;
            Attendance1.Text = x.course1.attendance.ToString();
            Absence1.Text = x.course1.absence.ToString();
            
            if (x.course1.absence>=3)
            {
                Absence1.BackColor = Color.Red;
            }

            Subject1.Show();
            Attendance1.Show();
            Absence1.Show();

            //Second Subject:
            Subject2.Text = x.course2.name;
            Attendance2.Text = x.course2.attendance.ToString();
            Absence2.Text = x.course2.absence.ToString();

            if (x.course2.absence >= 3)
            {
                Absence2.BackColor = Color.Red;
            }

            Subject2.Show();
            Attendance2.Show();
            Absence2.Show();

            //Third Subject:
            Subject3.Text = x.course3.name;
            Attendance3.Text = x.course3.attendance.ToString();
            Absence3.Text = x.course3.absence.ToString();

            if (x.course3.absence >= 3)
            {
                Absence3.BackColor = Color.Red;
            }

            Subject3.Show();
            Attendance3.Show();
            Absence3.Show();

        }

        private void StudentSelected_Click(object sender, EventArgs e)
        {
            StartPanel.Hide();
            StudentSignUpPanel.Hide();
            StudentLogInPanel.Hide();
        }

        private void EmployeeSelected_Click(object sender, EventArgs e)
        {
            StartPanel.Hide();
            EmployyeSignPanel.Hide();
        }

        private void BackButton_Click(object sender, EventArgs e)
        {
            StartPanel.Show();
            StudentSignUpPanel.Show();
            HideStudentPanel.Show();
            StudentLogInPanel.Show();
            EmployyeSignPanel.Show();

            EmployeeDataPanel.Hide();
            ChangeAtt_Button.Hide();
            ViewRecord_Button.Hide();
            info1Label.Hide();


        }

        private void button3_Click(object sender, EventArgs e)
        {
            employee m = new employee();
            m.login(this, EmployeeData);
            
        }


        private void ViewRecord_Button_Click(object sender, EventArgs e)
        {
            dataGridView1.Show();
            employee m = new employee();
            m.ReportAllStudents(this,StudData);
        }

        private void ChangeAtt_Button_Click(object sender, EventArgs e)
        {
            
            employee m = new employee();
            m.save(this,StudData);

            

        }

        private void ExitButton_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            student x = new student();
            StudData = x.signup(this,StudData);
            MessageBox.Show("Signed up");
            StudID_Login.Text = StudIDSignText.Text;
            StudName_Login.Text = StudNameSignText.Text;
            x = x.Login(this, StudData);
            
            
            

            //Assigning Student Data:
            NameLabel.Text = x.name;
            IDLabel.Text = x.ID;
            YearLabel.Text = x.AcademicYear;

            NameLabel.Show();
            IDLabel.Show();
            YearLabel.Show();

            //First Subject:
            Subject1.Text = x.course1.name;
            Attendance1.Text = x.course1.attendance.ToString();
            Absence1.Text = x.course1.absence.ToString();

            

            Subject1.Show();
            Attendance1.Show();
            Absence1.Show();

            //Second Subject:
            Subject2.Text = x.course2.name;
            Attendance2.Text = x.course2.attendance.ToString();
            Absence2.Text = x.course2.absence.ToString();

            

            Subject2.Show();
            Attendance2.Show();
            Absence2.Show();

            //Third Subject:
            Subject3.Text = x.course3.name;
            Attendance3.Text = x.course3.attendance.ToString();
            Absence3.Text = x.course3.absence.ToString();

         

            Subject3.Show();
            Attendance3.Show();
            Absence3.Show();


            //Clearing fields:

            StudID_Login.Text = "";
            StudIDSignText.Text = "";
            StudName_Login.Text = "";
            StudNameSignText.Text = "";
        }

        private void StudentCombo_SelectedIndexChanged(object sender, System.EventArgs e)
        { 
            studentCombo.Items.Clear();
            studentCombo.Text = " ";
            for (int i = 0; i < StudData.Count; i++)
            {
                if (courseCombo.Text == StudData[i].course1.name || courseCombo.Text == StudData[i].course2.name || courseCombo.Text == StudData[i].course3.name)
                {
                    studentCombo.Items.Add(StudData[i].name);
                }

            }
        
    }
    }
}
