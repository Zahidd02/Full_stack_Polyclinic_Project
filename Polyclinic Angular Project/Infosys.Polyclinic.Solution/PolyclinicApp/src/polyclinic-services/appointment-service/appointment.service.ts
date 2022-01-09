import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { IAppointment } from '../../app/polyclinic-interfaces/appointment';
import { IAppointmentDoctorName } from '../../app/polyclinic-interfaces/AppointmentDoctorName';
import { IDoctor } from '../../app/polyclinic-interfaces/doctor';

@Injectable({
  providedIn: 'root'
})
export class AppointmentService {

  constructor(private http: HttpClient) { }

  getAllAppointments(): Observable<IAppointmentDoctorName[]> {
    let temp = this.http.get<IAppointmentDoctorName[]>("http://localhost:26189/api/Polyclinic/GetAppointmentList");
    return temp;
  }

  cancelAnAppointment(appNo: number): Observable<number> {
    let httpOptions = { headers: new HttpHeaders({ 'Content-Type': 'application/json' }) };
    let temp = this.http.delete<number>("http://localhost:26189/api/Polyclinic/CancelAppointment?appointmentNo=" + appNo, httpOptions);
    return temp;
  }
}
