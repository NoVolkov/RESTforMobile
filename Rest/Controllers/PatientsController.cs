using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Rest.Models;

namespace Rest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatientsController : ControllerBase
    {
        string STR_CON = @"Data Source = tcp:192.168.1.41,49172; Initial Catalog = Test; Persist Security Info=True;User ID = USER; Password=123456";

        PatientsContex db;
        public PatientsController(PatientsContex contex)
        {
            db = contex;
        }
        // /api/patients
        [HttpGet]
        public List<Patient> Get()
        {
            List<Patient> list = new List<Patient>();
            using (SqlConnection con = new SqlConnection(STR_CON))
            {
                SqlCommand com = new SqlCommand("SELECT * FROM Patients", con);
                con.Open();
                SqlDataReader r = com.ExecuteReader();
                while (r.Read())
                {
                    Patient p = new Patient();
                    p.Id = (int)r["id"];
                    p.Snils = (string)r["snils"];
                    p.Password=(string)r["password"]
                    p.Name = (string)r["name"];
                    p.Surname = (string)r["surname"];
                    p.Patronymic = (string)r["patronymic"];
                    p.Age = (int)r["age"];
                    p.Height = (int)r["height"];
                    p.Weight = (int)r["weight"];
                    list.Add(p);
                }
                con.Close();
            }
            return list;
        }
        //выборка паиента по его снилсу
        // /api/patients/2
        [HttpGet("{snils}")]
        public Patient Get(string snils)
        {
            Patient p = new Patient();
            using (SqlConnection con = new SqlConnection(STR_CON))
            {
                SqlCommand com = new SqlCommand("SELECT * FROM Patients WHERE snils=@snils", con);
                SqlParameter prm = new SqlParameter("@snils", System.Data.SqlDbType.Int);
                prm.Value = snils;
                com.Parameters.Add(prm);
                con.Open();
                SqlDataReader r = com.ExecuteReader();
                while (r.Read())
                {
                    p.Id = (int)r["id"];
                    p.Snils = (string)r["snils"];
                    p.Password = (string)r["password"]
                    p.Name = (string)r["name"];
                    p.Surname = (string)r["surname"];
                    p.Patronymic = (string)r["patronymic"];
                    p.Age = (int)r["age"];
                    p.Height = (int)r["height"];
                    p.Weight = (int)r["weight"];
                }
                con.Close();
            }
            return p;
        }
        //добавление. регистрация пациента
        // /api/patients
        [HttpPost]
        public void Post(string patientJSON)
        {
            Patient p = JsonSerializer.Deserialize<Patient>(patientJSON);
            using (SqlConnection con = new SqlConnection(STR_CON))
            {
                SqlCommand com = new SqlCommand("INSERT INTO Patients (snils, password, name, surname, patronymic, age, height, weight) VALUES (@snils, @password, @name, @surname, @patronymic, @age, @height, @weight)", con);
                SqlParameter[] prmt = new SqlParameter[3];
                prmt[0] = new SqlParameter("@snils",System.Data.SqlDbType.NVarChar);
                prmt[0].Value = p.Snils;
                prmt[1] = new SqlParameter("@password", System.Data.SqlDbType.NVarChar);
                prmt[1].Value = p.Password;
                prmt[2] = new SqlParameter("@name", System.Data.SqlDbType.Int);
                prmt[2].Value = p.Name;
                prmt[3] = new SqlParameter("@surname", System.Data.SqlDbType.NVarChar);
                prmt[3].Value = p.Surname;
                prmt[4] = new SqlParameter("@patronymic", System.Data.SqlDbType.NVarChar);
                prmt[4].Value = p.Patronymic;
                prmt[5] = new SqlParameter("@age", System.Data.SqlDbType.NVarChar);
                prmt[5].Value = p.Age;
                prmt[6] = new SqlParameter("@height", System.Data.SqlDbType.NVarChar);
                prmt[6].Value = p.Height;
                prmt[7] = new SqlParameter("@weight", System.Data.SqlDbType.NVarChar);
                prmt[7].Value = p.Weight;
                
                com.Parameters.AddRange(prmt);
                con.Open();
                com.ExecuteNonQuery();
                con.Close();
            }

        }
        [HttpPut]
        public void Put(string patientJSON)
        {
            Patient p = JsonSerializer.Deserialize<Patient>(patientJSON);
            using (SqlConnection con = new SqlConnection(STR_CON)) ;
            SqlCommand com;
            if (p.Height == 0) com = new SqlCommand("UPDATE Patients SET height=@height, )", con);
            if (p.Weight == 0) return;
            if (p.Height = null && p.Weight == null) return;
            
        }

    }
}
