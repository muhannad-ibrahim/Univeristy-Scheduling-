﻿using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace university_scheduler.Model
{
    class Program
    {
        public int id { get; set; }
        public string name { get; set; }

        public string conString = env.db_con_str;

        List<TermData> termsData = new List<TermData>();

        List<Program> progData = new List<Program>();
        public List<Program> getAll()
        {
            SqlConnection cn = new SqlConnection(conString);
            cn.Open();
            string query = "SELECT * FROM program";
            using (SqlCommand cmd = new SqlCommand(query, cn))
            {
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    this.progData.Add(new Program { id = (int)reader.GetValue(0), name = (string)reader.GetValue(1) });
                }
                return progData;
            }
        }

        public List<Program> getAll(string dummyName)
        {
            SqlConnection cn = new SqlConnection(conString);
            cn.Open();
            string query = "SELECT * FROM program WHERE name LIKE '% " + dummyName + "%'";
            using (SqlCommand cmd = new SqlCommand(query, cn))
            {
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    this.progData.Add(new Program { id = (int)reader.GetValue(0), name = (string)reader.GetValue(1) });
                }
                return progData;
            }
        }
    }
}