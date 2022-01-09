import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { IDoctor } from '../../app/polyclinic-interfaces/doctor';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class DoctorService {

  doctor?: IDoctor[];
  constructor(private http: HttpClient) { }

  getDoctors(): Observable<IDoctor[]> {
    let temp = this.http.get<IDoctor[]>("http://localhost:26189/api/Polyclinic/GetDoctorList");
    return temp;
  }
}
