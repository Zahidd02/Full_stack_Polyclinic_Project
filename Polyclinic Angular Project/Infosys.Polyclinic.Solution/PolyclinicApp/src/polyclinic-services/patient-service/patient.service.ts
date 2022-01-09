import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { IPatient } from '../../app/polyclinic-interfaces/patient';

@Injectable({
  providedIn: 'root'
})
export class PatientService {

  patient?: IPatient[];
  constructor(private http: HttpClient) { }

  getPatientDetails(): Observable<IPatient[]> {
    let temp = this.http.get<IPatient[]>("http://localhost:26189/api/Polyclinic/GetPatientList");
    return temp;
  }

  updatePatientAge(patientId: string, newAge: number): Observable<boolean> {
    let x = "?patientId=" + patientId + "&newAge=" + newAge;
    let temp = this.http.put<boolean>("http://localhost:26189/api/Polyclinic/UpdatePatientAge" + x,"");
    return temp;
  }
}
