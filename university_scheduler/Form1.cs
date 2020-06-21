﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Forms;
using university_scheduler.Data;
using university_scheduler.Model;
using Excel = Microsoft.Office.Interop.Excel;

namespace university_scheduler
{
    public partial class NoScheduleHome : Form
    {
        Scheduler scheduler;

        public NoScheduleHome()
        {
            InitializeComponent();
            sDay.SelectedIndex = 0;
            eDay.SelectedIndex = 5;
            comboBox3.SelectedIndex = 0;
            startTime = stInput.Value.Hour;
            endTime = etInput.Value.Hour;
            startDay = sDay.SelectedIndex;
            endDay = eDay.SelectedIndex;
            int notComeFromHomeScreenWithTable = 0;
            courses_view(notComeFromHomeScreenWithTable);
            programs_view(notComeFromHomeScreenWithTable);
            classrooms_view(notComeFromHomeScreenWithTable);
            resourses_view(notComeFromHomeScreenWithTable);
        }

        public static int startTime;
        public static int endTime;
        public static int startDay;
        public static int endDay;

        //disableBTNS = add,delete,edit buttons 
        public NoScheduleHome(int existTable_disableBTNs)
        {
            InitializeComponent();
            sDay.SelectedIndex = 0;
            eDay.SelectedIndex = 5;
            comboBox3.SelectedIndex = 0;
            courses_view(existTable_disableBTNs);
            programs_view(existTable_disableBTNs);
            classrooms_view(existTable_disableBTNs);
            resourses_view(existTable_disableBTNs);

            startTime = stInput.Value.Hour;
            endTime = etInput.Value.Hour;
            startDay = sDay.SelectedIndex;
            endDay = eDay.SelectedIndex;
        }

        private void NoScheduleHome_Load(object sender, EventArgs e)
        {
            stInput.ShowUpDown = true;
            etInput.ShowUpDown = true;
            //Generator.generateALL();//------------
            scheduler = new Scheduler();
        }

        private void courses_view(int flag) {
            if (flag != 1)
            {
                viewCoursesForm frm = new viewCoursesForm() { Dock = DockStyle.Fill, TopLevel = false, TopMost = true };
                this.coursesView.Controls.Add(frm);
                frm.Show();
            }
            else {
                viewCoursesForm frm = new viewCoursesForm(flag) { Dock = DockStyle.Fill, TopLevel = false, TopMost = true };
                this.coursesView.Controls.Add(frm);
                frm.Show();
            }
        }
        private void programs_view(int flag)
        {
            if (flag != 1){
                viewProgramForm frm = new viewProgramForm() { Dock = DockStyle.Fill, TopLevel = false, TopMost = true };
                this.programsView.Controls.Add(frm);
                frm.Show();
            }
            else {
                viewProgramForm frm = new viewProgramForm(flag) { Dock = DockStyle.Fill, TopLevel = false, TopMost = true };
                this.programsView.Controls.Add(frm);
                frm.Show();
            }
        }
        private void classrooms_view(int flag)
        {
            if (flag != 1)
            {
                viewClassroomForm frm = new viewClassroomForm() { Dock = DockStyle.Fill, TopLevel = false, TopMost = true };
                this.classroomsView.Controls.Add(frm);
                frm.Show();
            }
            else {
                viewClassroomForm frm = new viewClassroomForm(flag) { Dock = DockStyle.Fill, TopLevel = false, TopMost = true };
                this.classroomsView.Controls.Add(frm);
                frm.Show();
            }
        }
        private void resourses_view(int flag)
        {
            if (flag != 1)
            {
                viewResourcesForm frm = new viewResourcesForm() { Dock = DockStyle.Fill, TopLevel = false, TopMost = true };
                this.resourcesView.Controls.Add(frm);
                frm.Show();
            }
            else {
                viewResourcesForm frm = new viewResourcesForm(flag) { Dock = DockStyle.Fill, TopLevel = false, TopMost = true };
                this.resourcesView.Controls.Add(frm);
                frm.Show();
            }
        }

        public void onNewReservation(int reserved, int total) {
            Console.WriteLine($"RES:{reserved},TOTAL:{total},Progress:{((double)reserved / (double)total)*100}");
        }

        private void generateBTN_Click(object sender, EventArgs e) {
            string message = "By generating a new table the old reservations will be permenantly DELETED\nmake sure to keep a backup of your old table if you need it";
            string title = "Old reservations will be deleted";
            MessageBoxButtons buttons = MessageBoxButtons.OKCancel;
            DialogResult result = MessageBox.Show(message, title, buttons, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
            if (result != DialogResult.OK)
            {
                return;
            }

            //EnableTab(false);
            EnableTab(this.coursesView, false);
            EnableTab(this.programsView, false);
            EnableTab(this.classroomsView, false);
            EnableTab(this.resourcesView, false);
            this.generateBTN.Enabled = false;
            Load_view();
            resLabel.Text = viewLoadForm.res;

            HomeScreenWithTable Popup = new HomeScreenWithTable();
            Popup.MdiParent = this.ParentForm;
            this.Hide();
            Popup.ShowDialog();
            this.Close();
        }

        private void Load_view()
        {
            viewLoadForm frm = new viewLoadForm();
            frm.startBrogress();
            DialogResult dialogResult = frm.ShowDialog();

            //frm.button1_Click(sender, e);

            /*
            viewLoadForm frm = new viewLoadForm() { Dock = DockStyle.Fill, TopLevel = false, TopMost = true };
            this.coursesView.Controls.Add(frm);
            frm.Show();

            frm = new viewLoadForm() { Dock = DockStyle.Fill, TopLevel = false, TopMost = true };
            this.programsView.Controls.Add(frm);
            frm.Show();

            frm = new viewLoadForm() { Dock = DockStyle.Fill, TopLevel = false, TopMost = true };
            this.classroomsView.Controls.Add(frm);
            frm.Show();

            frm = new viewLoadForm() { Dock = DockStyle.Fill, TopLevel = false, TopMost = true };
            this.resourcesView.Controls.Add(frm);
            frm.Show();
            */
        }
        public void EnableTab(TabPage page, bool enable)
        {
            //foreach (Control ctl in page.Controls) ctl.Enabled = enable;
            //page.Enabled = enable;
            //page.Hide();
            //tabControl1.
            page.Controls.Clear();
            //this.tabControl1.Hide();
        }
        /*
        void saveClassroomsinExcel()
        {
            object misValue = System.Reflection.Missing.Value;
            // To create workbook
            Excel.Application classroomsApp = new Excel.Application();
            Excel.Workbook wb = classroomsApp.Workbooks.Add(Excel.XlWBATemplate.xlWBATWorksheet);
            classroomsApp.DisplayAlerts = false;
            List<Classroom> classrooms = Classroom.getAll();
            foreach (Classroom classroom in classrooms)
            {
                List<Reservation> allResOfClass = Reservation.getResByClassId(classroom.id);
                genWorksheets(allResOfClass, classroomsApp, wb, classroom.name, "classroom");
            }
            Excel.Sheets sheet1 = wb.Worksheets;
            sheet1[sheet1.Count].delete();
            string path = System.IO.Directory.GetCurrentDirectory();
            wb.SaveAs(@path + "/classrooms.xls", Excel.XlFileFormat.xlWorkbookNormal, misValue, misValue, misValue, misValue, Excel.XlSaveAsAccessMode.xlExclusive, misValue, misValue, misValue, misValue, misValue);
            wb.Close(true, misValue, misValue);
        }
        */
        /*
        void saveProgramssinExcel()
        {
            object misValue = System.Reflection.Missing.Value;
            // To create workbook
            Excel.Application classroomsApp = new Excel.Application();
            Excel.Workbook wb = classroomsApp.Workbooks.Add(Excel.XlWBATemplate.xlWBATWorksheet);
            classroomsApp.DisplayAlerts = false;
            List<Model.Program> programs = Model.Program.getAll();
            foreach (Model.Program program in programs)
            {
                List<Reservation> allResOfProg = ReservationHasProgram.getProgramReservations(program.id);
                genWorksheets(allResOfProg, classroomsApp, wb, program.name, "program");
            }
            Excel.Sheets sheet1 = wb.Worksheets;
            sheet1[sheet1.Count].delete();
            string path = System.IO.Directory.GetCurrentDirectory();
            wb.SaveAs(@path + "/programs.xls", Excel.XlFileFormat.xlWorkbookNormal, misValue, misValue, misValue, misValue, Excel.XlSaveAsAccessMode.xlExclusive, misValue, misValue, misValue, misValue, misValue);
            wb.Close(true, misValue, misValue);
        }
        */
        /*
        void genWorksheets(List<Reservation> allResOfClass, Excel.Application classroomsApp, Excel.Workbook wb, string name, string model) {

            Dictionary<int, string> dayAsString = new Dictionary<int, string>();
            dayAsString.Add(0, "sat");
            dayAsString.Add(1, "sun");
            dayAsString.Add(2, "mon");
            dayAsString.Add(3, "tues");
            dayAsString.Add(4, "wed");
            dayAsString.Add(5, "thur");
            dayAsString.Add(6, "fri");
            Excel.Worksheet ws = (Excel.Worksheet)classroomsApp.Worksheets.Add();

            //Header of Excel
            ws.Cells[1] = "Day";
            ws.Cells[2] = "Course Name";
            ws.Cells[3] = "Course Code";          
            ws.Cells[4] = "From";
            ws.Cells[5] = "To";
            if(model == "program")
            {
                ws.Cells[6] = "Classroom";
            }
            //cells of excel
            for (int i = 0; i < allResOfClass.Count; i++){
                Course course = Course.getCourseById(allResOfClass[i].courseId);
                ws.Cells[i + 2, 1] = dayAsString[allResOfClass[i].day]; // convert day from integers to map 
                ws.Cells[i + 2, 2] = course.name;
                ws.Cells[i + 2, 3] = course.courseNamedId;
                ws.Cells[i + 2, 4] = allResOfClass[i].from + stInput.Value.Hour;
                ws.Cells[i + 2, 5] = allResOfClass[i].to + stInput.Value.Hour;
                if (model == "program")
                {
                    //get the name of the classroom of this reservation
                    ws.Cells[i + 2, 6] = Classroom.getClassroomById(allResOfClass[i].classId)[0].name; 
                }
            }
            ws.Name = name;
        }
        */
        private void stInput_ValueChanged(object sender, EventArgs e)
        {

        }

        private void coursesView_Click(object sender, EventArgs e)
        {
            ///
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}


//SOME FAKE COMMENT TO FIX GIT