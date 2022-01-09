import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { PatientService } from '../../polyclinic-services/patient-service/patient.service';
import { IPatient } from '../polyclinic-interfaces/patient';

@Component({
  selector: 'app-view-patients',
  templateUrl: './view-patients.component.html',
  styleUrls: ['./view-patients.component.css']
})
export class ViewPatientsComponent implements OnInit {

  patients?: IPatient[];
  filteredPatients?: IPatient[];
  constructor(private _patientService: PatientService, private router: Router) { }

  ngOnInit(): void {
    this.getPatientDetails();
  }

  getPatientDetails() {
    this._patientService.getPatientDetails().subscribe(
      responsePatientData => {
        this.patients = responsePatientData;
        this.filteredPatients = responsePatientData;
      }
    );
  }

  searchPatientByName(name: string) {
    this.filteredPatients = this.patients?.filter(x => x.patientName.toLowerCase().indexOf(name.toLowerCase()) >= 0);
  }

  updatePatientAge(patient: IPatient) {
    this.router.navigate(['/updateAge', patient.patientId, patient.patientName]);
  }
}
