import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { LoginComponent } from '../login/login.component';
import { RegisterComponent } from '../register/register.component';
import { HttpClientModule } from '@angular/common/http';
import { AuthLayoutComponent } from './auth-layout.component';
//import { AuthLayoutRoutingModule } from './auth-layout-routing.module';
import { RouterModule } from '@angular/router';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
@NgModule({
  imports: [
    CommonModule,
    //AuthLayoutRoutingModule,
    FormsModule,
    ReactiveFormsModule,
    HttpClientModule,
    NgbModule,
    RouterModule
  ],
  declarations: [
    LoginComponent,
    RegisterComponent
  ],
  providers: [],
  bootstrap: [AuthLayoutComponent]
})
export class AuthLayoutModule { }
