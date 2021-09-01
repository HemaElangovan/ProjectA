using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Clinic_Management_System.Models
{
    public class LoginModel
    {
        SqlConnection con = new SqlConnection("Data Source=HEMA-E; Database =Clinic_Management_System; User ID=sa; Password=sasa@123");

       
        public DataTable Get()
            {
            string login = "select UserName, StaffPassword from tbl_StaffLogin";
            SqlDataAdapter da = new SqlDataAdapter(login, con);
            DataTable dt = new DataTable();
            da.Fill(dt);
            return dt;
            }
        public DataTable GetDoc()
        {
            string str = "select Doctor_ID,FirstName,LastName,Sex,Specialization,VisitingHours from tbl_Doctor";
            SqlDataAdapter da = new SqlDataAdapter(str, con);
            DataTable dt = new DataTable();
            da.Fill(dt);
            return dt;
        }
        public List<SelectListItem> DocList { get; set; }
        public int DocId { get; set; }
        public string FirstName { get; set; }
        public List<SelectListItem> GetDocList()
        {
            List<SelectListItem> dlist = new List<SelectListItem>();
            string doc = "select Doctor_ID,FirstName from tbl_Doctor";
            SqlCommand cmd = new SqlCommand(doc, con);
            con.Open();
            SqlDataReader rdr = cmd.ExecuteReader();
            while (rdr.Read())
            {
                dlist.Add(new SelectListItem {Text = rdr["FirstName"].ToString(), Value=rdr["FirstName"].ToString()});
            }
            con.Close();
            return dlist;
        }
        public int InsertDocDetails(string fname, string lname, string sex, string spl, string vhrs)
        {
            string ins = "Insert into tbl_Doctor values (@fname, @lname, @sex, @spl, @vhrs)";
            SqlCommand cmd = new SqlCommand(ins, con);
            cmd.Parameters.AddWithValue("@fname", fname);
            cmd.Parameters.AddWithValue("@lname", lname);
            cmd.Parameters.AddWithValue("@sex", sex);
            cmd.Parameters.AddWithValue("@spl", spl);
            cmd.Parameters.AddWithValue("@vhrs", vhrs);
            con.Open();
            int x = cmd.ExecuteNonQuery();
            //return cmd.ExecuteNonQuery();
            con.Close();
            return x;
        }
        public DataTable GetPat()
        {
            string str = "select Patient_ID,FirstName,LastName,Sex,Age,Date_Of_Birth from tbl_Patient";
            SqlDataAdapter da = new SqlDataAdapter(str, con);
            DataTable dt = new DataTable();
            da.Fill(dt);
            return dt;
        }
        public int InsertPatDetails(string fname, string lname, string sex, string age, DateTime dob)
        {
            string ins = "Insert into tbl_Patient values (@fname, @lname, @sex, @age, @dob)";
            SqlCommand cmd = new SqlCommand(ins, con);
            cmd.Parameters.AddWithValue("@fname", fname);
            cmd.Parameters.AddWithValue("@lname", lname);
            cmd.Parameters.AddWithValue("@sex", sex);
            cmd.Parameters.AddWithValue("@age", age);
            cmd.Parameters.AddWithValue("@dob", dob.ToString());
            con.Open();
            int x = cmd.ExecuteNonQuery();
           // return cmd.ExecuteNonQuery();
            con.Close();
            return x;
        }
        public DataTable GetAppt()
        {
            string app ="select Appointment_ID, tp.Patient_Id, ts.Specialization, td.FirstName as DoctorName,Format(VisitDate,'dd/MM/yyyy')as Visit_Date,CAST(CONVERT(TIME(6),AppointmentTime) AS VARCHAR(5)) as Appointment_Time from tbl_Schedule ts join tbl_doctor td on ts.Specialization = td.Specialization join tbl_Patient tp on tp.Patient_ID= ts.Patient_Id";
            SqlDataAdapter da = new SqlDataAdapter(app, con);
            DataTable dt = new DataTable();
            da.Fill(dt);
            return dt;
        }

        public int ScheduleApp(int pid,string spl,string dname, string vdate, string atime)
        {
                string ins = "Insert into tbl_Schedule values (@pid,@spl, @dname, @vdate, @atime)";
                SqlCommand cmd = new SqlCommand(ins, con);
                cmd.Parameters.AddWithValue("@pid", pid);
                cmd.Parameters.AddWithValue("@spl", spl);
                cmd.Parameters.AddWithValue("@dname", dname);
                cmd.Parameters.AddWithValue("@vdate", vdate);
                cmd.Parameters.AddWithValue("@atime", atime);
                con.Open();
                return cmd.ExecuteNonQuery();
                con.Close();           
        }
        public DataTable CancelById(int aid)
        {
            string str = "select Appointment_ID,Patient_Id, Specialization, DoctorName, VisitDate, AppointmentTime from tbl_Schedule where Appointment_ID=" + aid;
            SqlDataAdapter da = new SqlDataAdapter(str, con);
            DataTable dt = new DataTable();
            da.Fill(dt);
            return dt;
        }
        public int CancelAppt(int aid)
        {
            string del = "delete tbl_Schedule where Appointment_ID=" + aid;
            SqlCommand cmd = new SqlCommand(del, con);
            con.Open();
            int res = cmd.ExecuteNonQuery();
            con.Close();
            return res;
        }
    }
}