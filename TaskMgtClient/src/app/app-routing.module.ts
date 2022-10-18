import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AppLayoutComponent } from './app-layout/app-layout.component';
import { AuthLayoutComponent } from './auth-layout/auth-layout.component';
import { LoginComponent } from './login/login.component';
import { RegisterComponent } from './register/register.component';
const routes: Routes = [
  {
    path: '',
    redirectTo: 'tasks',
    pathMatch: 'full',
  }, {
    path: '',
    component: AppLayoutComponent,
    children: [
      {
        path: '',
        loadChildren: () => import('./app-layout/app-layout.module').then(m => m.AppLayoutModule)
      }
    ]
  },{
    path: 'login',
    component: LoginComponent,
    children: [
      {
        path: '',
        loadChildren: () => import('./auth-layout/auth-layout.module').then(m => m.AuthLayoutModule)
      }
    ]
  },
  {
    path: 'register',
    component: RegisterComponent,
    children: [
      {
        path: '',
        loadChildren: () => import('./auth-layout/auth-layout.module').then(m => m.AuthLayoutModule)
      }
    ]
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
