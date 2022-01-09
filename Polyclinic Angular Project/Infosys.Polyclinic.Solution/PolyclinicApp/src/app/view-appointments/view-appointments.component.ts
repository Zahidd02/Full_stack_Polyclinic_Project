import { Component, OnInit } from '@angular/core';
import { AppointmentService } from '../../polyclinic-services/appointment-service/appointment.service';
import { IAppointment } from '../polyclinic-interfaces/appointment';
import { IAppointmentDoctorName } from '../polyclinic-interfaces/AppointmentDoctorName';
import { IDoctor } from '../polyclinic-interfaces/doctor';

@Component({
  selector: 'app-view-appointments',
  templateUrl: './view-appointments.component.html',
  styleUrls: ['./view-appointments.component.css']
})
export class ViewAppointmentsComponent implements OnInit {

  appointments?: IAppointmentDoctorName[];
  doctors?: IDoctor[];
  signal?: number;
  constructor(private _appointmentService: AppointmentService) { }

  ngOnInit(): void {
    this.getAllAppointments();
  }

  getAllAppointments() {
    this._appointmentService.getAllAppointments().subscribe(
      responseAppointmentData => {
        this.appointments = responseAppointmentData;
      }
    );
  }

  cancelAnAppointment(appNo: number) {
    this._appointmentService.cancelAnAppointment(appNo).subscribe(
      responseAppointmentData => {
        this.signal = responseAppointmentData;
        if (this.signal == 1) {
          alert("Appointment Cancelled Successfully!");
        }
        else {
          alert("Appointment could not be deleted.Please try after sometime.");
        }
      }
    );
  }
}
