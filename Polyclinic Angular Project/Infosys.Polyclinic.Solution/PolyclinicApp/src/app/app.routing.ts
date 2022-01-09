import { RouterModule, Routes } from '@angular/router';
import { ModuleWithProviders } from '@angular/core';
import { ViewDoctorsComponent } from './view-doctors/view-doctors.component';
import { ViewPatientsComponent } from './view-patients/view-patients.component';
import { ViewAppointmentsComponent } from './view-appointments/view-appointments.component';
import { UpdateAgeComponent } from './update-age/update-age.component';

const routes: Routes = [
  { path: '', component: ViewDoctorsComponent },
  { path: 'home', component: ViewDoctorsComponent },
  { path: 'viewPatients', component: ViewPatientsComponent },
  { path: 'viewAppointments', component: ViewAppointmentsComponent },
  { path: 'updateAge/:patientId/:patientName', component: UpdateAgeComponent }

];

export const routing: ModuleWithProviders<RouterModule> = RouterModule.forRoot(routes);

