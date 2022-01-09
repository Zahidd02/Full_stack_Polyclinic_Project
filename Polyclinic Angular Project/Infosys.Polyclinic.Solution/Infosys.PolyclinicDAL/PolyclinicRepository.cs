using Infosys.PolyclinicDAL.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Infosys.PolyclinicDAL
{
    public class PolyclinicRepository
    {
        PolyclinicDBContext context;

        public PolyclinicRepository()
        {
            context = new PolyclinicDBContext();
        }

        public bool AddNewPatientDetails(Patients patientObj)
        {
            bool status = false;
            try
            {
                context.Patients.Add(patientObj);
                context.SaveChanges();
                status = true;
            }
            catch (Exception)
            {
                status = false;
            }
            return status;
        }

        public decimal CalculateDoctorFees(string doctorId, DateTime date)
        {
            decimal result;
            try
            {
                result = (from x in context.Appointments select PolyclinicDBContext.ufn_CalculateDoctorFees(doctorId, date)).FirstOrDefault();
            }
            catch (Exception)
            {
                result = -99;
            }
            return result;
        }

        public int CancelAppointment(int appointmentNo)
        {
            int status = -1;
            try
            {   
                var result = context.Appointments.Where(x => x.AppointmentNo == appointmentNo).FirstOrDefault();
                context.Appointments.Remove(result);
                context.SaveChanges();
                status = 1;
                
            }
            catch (Exception)
            {
                status = -99;
            }
            return status;
        }

        public List<DoctorAppointmentCombine> FetchAllAppointments(string doctorId, DateTime date)
        {
            List<DoctorAppointmentCombine> result = new List<DoctorAppointmentCombine>();
            try
            {
                SqlParameter prmDoctorID = new SqlParameter("@DoctorID", doctorId);
                SqlParameter prmDateofAppointment = new SqlParameter("@DateofAppointment", date);

                result = context.DoctorAppointmentCombine.FromSqlRaw("SELECT * FROM ufn_FetchAllAppointments(@DoctorID, @DateofAppointment)", prmDoctorID, prmDateofAppointment).ToList();
            }
            catch (Exception)
            {
                result = null;
            }
            return result;
        }

        public int GetDoctorAppointment(string patientId, string doctorId, DateTime dateOfAppointment, out int appointmentNo)
        {
            appointmentNo = 0;
            int returnVal = 0;
            SqlParameter prmPatientId = new SqlParameter("@PatientID", patientId);
            SqlParameter prmDoctorId = new SqlParameter("@DoctorID", doctorId);
            SqlParameter prmDateOfAppointment = new SqlParameter("@DateOfAppointment", dateOfAppointment);

            SqlParameter prmAppointmentNo = new SqlParameter("@AppointmentNo",System.Data.SqlDbType.TinyInt);
            prmAppointmentNo.Direction = System.Data.ParameterDirection.Output;

            SqlParameter prmReturnVal = new SqlParameter("@ReturnVal", System.Data.SqlDbType.Int);
            prmReturnVal.Direction = System.Data.ParameterDirection.Output;

            try
            {
                context.Database.ExecuteSqlRaw("EXEC @ReturnVal = usp_GetDoctorAppointment @PatientID, @DoctorID, @DateOfAppointment, @AppointmentNo OUT", prmReturnVal, prmPatientId, prmDoctorId, prmDateOfAppointment, prmAppointmentNo);
                appointmentNo = Convert.ToInt32(prmAppointmentNo.Value);
                returnVal = Convert.ToInt32(prmReturnVal.Value);
            }
            catch (Exception)
            {
                returnVal = -99;
                appointmentNo = 0;
            }
            return returnVal;
        }

        public Patients GetPatientDetails(string patientId)
        {
            Patients results = new Patients();
            try
            {
                results = (from x in context.Patients where x.PatientId == patientId select x).FirstOrDefault();
            }
            catch (Exception)
            {
                results = null;
            }
            return results;
        }

        public bool UpdatePatientAge(string patientId, byte newAge)
        {
            bool status = false;
            try
            {
                var result = context.Patients.Where(x => x.PatientId == patientId).FirstOrDefault();
                result.Age = newAge;
                context.SaveChanges();
                status = true;
            }
            catch (Exception)
            {
                status = false;
            }
            return status;
        }

        public List<Doctors> GetDoctorList()
        {
            List<Doctors> result = new List<Doctors>();
            try
            {
                result = (from x in context.Doctors select x).ToList();
            }
            catch (Exception)
            {
                result = null;
            }
            return result;
        }

        public List<Patients> GetPatientList()
        {
            List<Patients> result = new List<Patients>();
            try
            {
                result = (from x in context.Patients select x).ToList();
            }
            catch (Exception)
            {
                result = null;
            }
            return result;
        }

        public List<AppointmentDoctorName> GetAppointmentList()
        {
            List<AppointmentDoctorName> result = new List<AppointmentDoctorName>();
            try
            {
                result = (
                    from a in context.Appointments
                    join d in context.Doctors on a.DoctorId equals d.DoctorId
                    join p in context.Patients on a.PatientId equals p.PatientId
                    select new AppointmentDoctorName
                    {
                        AppointmentNo = a.AppointmentNo,
                        DoctorName = d.DoctorName,
                        PatientId = p.PatientId,
                        PatientName = p.PatientName,
                        DateofAppointment = a.DateofAppointment
                    }
                    ).ToList();
                           
            }
            catch (Exception)
            {
                result = null;
            }
            return result;
        }
    }
}
