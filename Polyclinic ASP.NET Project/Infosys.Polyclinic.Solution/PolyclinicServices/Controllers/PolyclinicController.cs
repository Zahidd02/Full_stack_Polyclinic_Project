using Infosys.PolyclinicDAL;
using Infosys.PolyclinicDAL.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

[Route("api/[controller]/[action]")]
[ApiController]
public class PolyclinicController : Controller
{
    PolyclinicRepository repository;

    public PolyclinicController()
    {
        repository = new PolyclinicRepository();
    }

    [HttpGet]
    public JsonResult WelcomeMessage()
    {
        string msg = "Welcome to my Capstone Project Service Layer";
        return Json(msg);
    }

    [HttpPost]
    public bool AddNewPatientDetails(Patients patientobj)
    {
        Infosys.PolyclinicDAL.Models.Patients dalPatientObj = new Infosys.PolyclinicDAL.Models.Patients();
        bool status = false;
        try
        {
            if (ModelState.IsValid)
            {
                dalPatientObj.PatientId = patientobj.PatientId;
                dalPatientObj.PatientName = patientobj.PatientName;
                dalPatientObj.Age = patientobj.Age;
                dalPatientObj.Gender = patientobj.Gender;
                dalPatientObj.ContactNumber = patientobj.ContactNumber;

                status = repository.AddNewPatientDetails(dalPatientObj);
            }
            else
            {
                status = false;
            }
        }
        catch (Exception)
        {
            status = false;
        }
        return status;
    }

    [HttpGet]
    public JsonResult CalculateDoctorFees(string doctorId, DateTime date)
    {
        decimal status;
        try
        {
            status = repository.CalculateDoctorFees(doctorId, date);
        }
        catch (Exception)
        {
            status = -99;
        }
        return Json(status);
    }

    [HttpDelete]
    public int CancelAppointment(int appointmentNo)
    {
        int status = 0;
        try
        {
            status = repository.CancelAppointment(appointmentNo);
        }
        catch (Exception)
        {
            status = -99;
        }
        return status;
    }

    [HttpGet]
    public JsonResult FetchAllAppointments(string doctorId, DateTime date)
    {
        List<DoctorAppointmentCombine> status = null;
        try
        {
            status = repository.FetchAllAppointments(doctorId, date);
        }
        catch (Exception)
        {
            status = null;
        }
        return Json(status);
    }

    [HttpGet]
    public int GetDoctorAppointment(string patientId, string doctorId, DateTime dateOfAppointment)
    {
        int status = 0; 
        try
        {
            int appointmentNo;
            status = repository.GetDoctorAppointment(patientId, doctorId, dateOfAppointment, out appointmentNo);
        }
        catch (Exception)
        {
            status = 0;
        }
        return status;
    }

    [HttpGet]
    public JsonResult GetPatientDetails(string patientId)
    {
        Infosys.PolyclinicDAL.Models.Patients status = new Infosys.PolyclinicDAL.Models.Patients();
        try
        {
            status = repository.GetPatientDetails(patientId);
        }
        catch (Exception)
        {
            status = null;
        }
        return Json(status);
    }

    [HttpPut]
    public bool UpdatePatientAge(string patientId, byte newAge)
    {
        bool status = false;
        try
        {
            status = repository.UpdatePatientAge(patientId, newAge);
        }
        catch (Exception)
        {
            status = false;
        }
        return status;
    }
}

