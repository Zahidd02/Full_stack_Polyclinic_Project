

import { Component, OnInit } from '@angular/core';
import { NgForm } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { PatientService } from '../../polyclinic-services/patient-service/patient.service';

@Component({
  selector: 'app-update-age',
  templateUrl: './update-age.component.html',
  styleUrls: ['./update-age.component.css']
})
export class UpdateAgeComponent implements OnInit {
  status?: boolean;
  patientId: string = "";
  patientName?: string;
  constructor(private navRouter: Router, private router: ActivatedRoute, private _patientService: PatientService) { }

  ngOnInit(): void {
    this.patientId = this.router.snapshot.params['patientId'];
    this.patientName = this.router.snapshot.params['patientName'];
  }

  updatePatientAge(age: number) {
    this._patientService.updatePatientAge(this.patientId, age).subscribe(
      responseUpdateAge => {
        this.status = responseUpdateAge;
      }
    );
    this.navRouter.navigate(['/viewPatients']);
  }

}
