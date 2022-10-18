import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Subscription } from 'rxjs';
import { AuthService } from '../auth-layout/auth.service';
import { TokenStorageService } from '../auth-layout/token-storage.service';
import Swal from 'sweetalert2';
import { Router } from '@angular/router';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit {

  loginFormGroup: FormGroup = this.fb.group({
    email: ["", Validators.required],
    password: ["", Validators.required]
  });
 
  sub!: Subscription;
  errorMessage = '';
  isLoggedIn = false;
  isLoginFailed = false;
  roles: string[] = [];

  constructor(
    private authService: AuthService,
    private fb: FormBuilder,
    private tokenStorage: TokenStorageService,
    private router: Router
  ) {}

  ngOnInit(): void {
    if (this.tokenStorage.getToken()) {
      this.isLoggedIn = true;
      this.roles = this.tokenStorage.getUser().roles;
    }
  }

  Authenticate(){
    let formValue = this.loginFormGroup.value;
    const params: any = {
      email: formValue.email,
      password: formValue.password
    }
    this.authService.login(params).subscribe({
      next: data => {
        this.tokenStorage.saveToken(data.token);
        this.tokenStorage.saveUser(data);
        Swal.fire({
          title: 'Logged In!',
          text: '',
          icon: 'success'
        })
        this.isLoginFailed = false;
        this.isLoggedIn = true;
        this.roles = this.tokenStorage.getUser().roles;
        this.router.navigate(['/tasks']);
      },
      error: err => {
        this.errorMessage = err.error.message;
        this.isLoginFailed = true;
      }
    })
  }

  
  reloadPage(): void {
    window.location.reload();
  }
}

