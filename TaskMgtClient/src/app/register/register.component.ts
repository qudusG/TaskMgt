import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Subscription } from 'rxjs';
import { AuthService } from '../auth-layout/auth.service';
import Swal from 'sweetalert2';
@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.scss']
})
export class RegisterComponent implements OnInit {

 formGroupReg: FormGroup = this.fb.group({
    name: ["", Validators.required],
    email: ["", Validators.required],
    password: ["", Validators.required]
  });
  
  errorMessage = '';
  constructor(
    private authService: AuthService,
    private fb: FormBuilder
  ) {}

  ngOnInit(): void {
  }

  Register(){
    let formValue = this.formGroupReg.value;
    const params: any = {
      email: formValue.email,
      name: formValue.name,
      password: formValue.password,
    }
    this.authService.register(params).subscribe({
      next: data => {
        Swal.fire({
          title: 'User created successfully!',
          text: '',
          icon: 'success'
        })
      },
      error: err => {
        this.errorMessage = err.error.message;
      }
    })
  }


}
