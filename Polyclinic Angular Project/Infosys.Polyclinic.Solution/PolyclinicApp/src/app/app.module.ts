import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppComponent } from './app.component';
import { ViewDoctorsComponent } from './view-doctors/view-doctors.component';
import { HttpClientModule } from '@angular/common/http';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { ViewPatientsComponent } from './view-patients/view-patients.component';
import { routing } from './app.routing';
import { ViewAppointmentsComponent } from './view-appointments/view-appointments.component';
import { UpdateAgeComponent } from './update-age/update-age.component';

@NgModule({
  declarations: [
    AppComponent,
    ViewDoctorsComponent,
    ViewPatientsComponent,
    ViewAppointmentsComponent,
    UpdateAgeComponent
  ],
  imports: [
    BrowserModule,
    HttpClientModule,
    FormsModule,
    ReactiveFormsModule,
    routing
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
