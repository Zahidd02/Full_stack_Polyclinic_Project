import { Component, OnInit } from '@angular/core';
import { DoctorService } from '../../polyclinic-services/doctor-service/doctor.service';
import { IDoctor } from '../polyclinic-interfaces/doctor';

@Component({
  selector: 'app-view-doctors',
  templateUrl: './view-doctors.component.html',
  styleUrls: ['./view-doctors.component.css']
})
export class ViewDoctorsComponent implements OnInit {

  doctors?: IDoctor[];
  filteredDoctor?: IDoctor[];
  constructor(private _doctorService: DoctorService) {
  }

  ngOnInit(): void {
    this.getDoctors();
  }

  getDoctors() {
    this._doctorService.getDoctors().subscribe(
      responseDoctorData => {
        this.doctors = responseDoctorData;
        this.filteredDoctor = responseDoctorData;
      }
    );
  }

  getDoctorsBySpecialization(special: string) {
    this.filteredDoctor = this.doctors?.filter(x => x.specialization.toLowerCase().indexOf(special.toLowerCase()) >= 0);
  }
}
